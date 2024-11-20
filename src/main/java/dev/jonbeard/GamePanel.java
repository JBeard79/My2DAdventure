package dev.jonbeard;

import dev.jonbeard.Asset.AssetManager;
import dev.jonbeard.Collision.CollisionChecker;
import dev.jonbeard.Entity.Entity;
import dev.jonbeard.Entity.Player;
import dev.jonbeard.Input.KeyHandler;
import dev.jonbeard.Items.Item;
import dev.jonbeard.Sound.SoundManager;
import dev.jonbeard.Tile.TileManager;
import dev.jonbeard.UI.UI;

import javax.swing.*;
import java.awt.*;

public class GamePanel extends JPanel implements Runnable {
    public static final int MAX_WORLD_COLUMN = 50;
    public static final int MAX_WORLD_ROW = 50;
    public static final int PLAY_STATE = 1;
    public static final int PAUSE_STATE = 2;
    public static final int MAX_SCREEN_COL = 16;
    public static final int MAX_SCREEN_ROW = 12;
    public static final int TITLE_STATE = 3;
    // SCREEN SETTINGS
    static final int ORIGINAL_TILE_SIZE = 16;
    static final int SCALE = 3;
    public static final int TILE_SIZE = ORIGINAL_TILE_SIZE * SCALE;
    public static final int SCREEN_WIDTH = TILE_SIZE * MAX_SCREEN_COL;  // 768 pixels
    public static final int SCREEN_HEIGHT = TILE_SIZE * MAX_SCREEN_ROW; // 576 pixels
    public TileManager tileManager = new TileManager(this);
    public CollisionChecker collisionChecker = new CollisionChecker(this);
    public SoundManager music = new SoundManager();
    public SoundManager soundEffect = new SoundManager();
    public UI userInterface = new UI(this);
    public Item[] items = new Item[10];
    public Entity[] npcs = new Entity[10];
    public int gameState;
    public int dialogState;
    public AssetManager assetManager = new AssetManager(this);
    public KeyHandler keyH = new KeyHandler(this);
    public Player player = new Player(this, keyH);
    int fps = 60;
    private Thread gameThread;

    public GamePanel() {
        this.setPreferredSize(new Dimension(SCREEN_WIDTH, SCREEN_HEIGHT));
        this.setBackground(Color.black);
        this.setDoubleBuffered(true);
        this.addKeyListener(keyH);
        this.setFocusable(true);
    }

    public void startGameThread() {
        gameThread = new Thread(this);
        gameThread.start();
    }

    public void setupGame() {
        assetManager.setObject();
        assetManager.setNPC();
        gameState = TITLE_STATE;
        //playMusic(0);
    }

    @Override
    public void run() {
        double drawInterval = (double) 1000000000 / fps;
        double delta = 0;
        long lastTime = System.nanoTime();
        long currentTime;
        long timer = 0;
        int drawCount = 0;

        while (gameThread != null && gameThread.isAlive()) {
            currentTime = System.nanoTime();
            delta += (currentTime - lastTime) / drawInterval;
            timer += (currentTime - lastTime);
            lastTime = currentTime;

            if (delta >= 1) {
                update();
                repaint();
                delta--;
                drawCount++;
            }
            if (timer >= 1000000000) {
                System.out.println("FPS: " + drawCount);
                drawCount = 0;
                timer = 0;
            }
        }
    }

    public void update() {
        if (gameState == PLAY_STATE) {
            player.update();
            for (Entity npc : npcs) {
                if (npc != null) {
                    npc.update();
                }
            }
        }
        if (gameState == PAUSE_STATE) {
            //nothing for now
        }
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);

        Graphics2D g2 = (Graphics2D) g;


        if (gameState == TITLE_STATE) {
            userInterface.draw(g2);
        } else {
            tileManager.draw(g2);
            for (Item item : items) {
                if (item != null) {
                    item.draw(g2, this);
                }
            }

            for (int i = 0; i < npcs.length; i++) {
                if (npcs[i] != null) {
                    npcs[i].draw(g2);
                }
            }
            player.draw(g2);
            userInterface.draw(g2);
        }

        g2.dispose();
    }

    public void playMusic(int index) {
        music.setFile(index);
        music.play();
        music.loop();
    }

    public void stopMusic() {
        music.stop();
    }

    public void playSoundEffect(int index) {
        soundEffect.setFile(index);
        soundEffect.play();
    }
}
