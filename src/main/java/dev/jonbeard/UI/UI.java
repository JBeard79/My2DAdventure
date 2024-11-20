package dev.jonbeard.UI;

import dev.jonbeard.GamePanel;
import dev.jonbeard.Items.Heart;
import dev.jonbeard.Items.Item;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.io.InputStream;

public class UI {
    public boolean messageOn = false;
    public String message = "";
    public String currentDialogue = "";
    public int commandNum = 0;
    Graphics2D g2;
    GamePanel gp;
    Font pixelated_elegance;
    BufferedImage heart_full, heart_half, heart_blank;


    public UI(GamePanel gp) {
        this.gp = gp;
        try {
            InputStream is = getClass().getResourceAsStream("/fonts/pixelated_elegance.ttf");
            assert is != null;
            pixelated_elegance = Font.createFont(Font.TRUETYPE_FONT, is);
        } catch (FontFormatException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        Item heart = new Heart(gp);
        heart_full = heart.image;
        heart_half = heart.image2;
        heart_blank = heart.image3;
    }

    public void showMessage(String message) {
        this.message = message;
        messageOn = true;
    }

    public void draw(Graphics2D g2) {
        this.g2 = g2;
        g2.setFont(pixelated_elegance);
        g2.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
        g2.setColor(Color.white);

        if (gp.gameState == GamePanel.TITLE_STATE) {
            drawTitleScreen();
        }
        if (gp.gameState == GamePanel.PLAY_STATE) {
            drawPlayerLife();
        }

        if (gp.gameState == GamePanel.PAUSE_STATE) {
            drawPlayerLife();
            drawPauseScreen();
        }

        if (gp.gameState == gp.dialogState) {
            drawPlayerLife();
            drawDialogScreen();
        }

    }

    private void drawPlayerLife() {
        int x = GamePanel.TILE_SIZE / 2;
        int y = GamePanel.TILE_SIZE / 2;
        int i = 0;
        while (i < gp.player.maxLife / 2) {
            g2.drawImage(heart_blank, x, y, null);
            i++;
            x += GamePanel.TILE_SIZE;
        }

        x = GamePanel.TILE_SIZE / 2;
        y = GamePanel.TILE_SIZE / 2;
        i = 0;

        while (i < gp.player.life) {
            g2.drawImage(heart_half, x, y, null);
            i++;
            if (i < gp.player.life) {
                g2.drawImage(heart_full, x, y, null);
            }
            i++;
            x += GamePanel.TILE_SIZE;
        }
    }

    private void drawTitleScreen() {
        g2.setColor(new Color(0, 0, 0));
        g2.fillRect(0, 0, GamePanel.SCREEN_WIDTH, GamePanel.SCREEN_HEIGHT);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD, 50F));

        String text = "Blue Boy Adventure";
        int x = getXForCenteredText(text);
        int y = GamePanel.TILE_SIZE * 3;

        g2.setColor(Color.gray);
        g2.drawString(text, x + 5, y + 5);
        g2.setColor(Color.white);
        g2.drawString(text, x, y);

        x = GamePanel.SCREEN_WIDTH / 2 - (GamePanel.TILE_SIZE * 2) / 2;
        y += GamePanel.TILE_SIZE * 2;
        g2.drawImage(gp.player.down1, x, y, GamePanel.TILE_SIZE * 2, GamePanel.TILE_SIZE * 2, null);

        g2.setFont(g2.getFont().deriveFont(Font.BOLD, 35f));

        text = "NEW GAME";
        x = getXForCenteredText(text);
        y += GamePanel.TILE_SIZE * 3.5;
        g2.drawString(text, x, y);
        if (commandNum == 0) {
            g2.drawString(">", x - GamePanel.TILE_SIZE, y);
        }

        text = "LOAD GAME";
        x = getXForCenteredText(text);
        y += GamePanel.TILE_SIZE;
        g2.drawString(text, x, y);
        if (commandNum == 1) {
            g2.drawString(">", x - GamePanel.TILE_SIZE, y);
        }


        text = "QUIT";
        x = getXForCenteredText(text);
        y += GamePanel.TILE_SIZE;
        g2.drawString(text, x, y);
        if (commandNum == 2) {
            g2.drawString(">", x - GamePanel.TILE_SIZE, y);
        }
    }

    private void drawDialogScreen() {
        // WINDOW
        int x = GamePanel.TILE_SIZE * 2;
        int y = GamePanel.TILE_SIZE / 2;
        int width = GamePanel.SCREEN_WIDTH - (GamePanel.TILE_SIZE * 4);
        int height = GamePanel.TILE_SIZE * 4;
        drawSubWindow(x, y, width, height);
        g2.setFont(g2.getFont().deriveFont(25f));
        x += GamePanel.TILE_SIZE;
        y += GamePanel.TILE_SIZE;

        for (String line : currentDialogue.split("\n")) {
            g2.drawString(line, x, y);
            y += 40;
        }
    }

    public void drawSubWindow(int x, int y, int width, int height) {
        Color c = new Color(0, 0, 0, 210);
        g2.setColor(c);
        g2.fillRoundRect(x, y, width, height, 35, 35);

        c = new Color(255, 255, 255);
        g2.setColor(c);
        g2.setStroke(new BasicStroke(5));
        g2.drawRoundRect(x + 5, y + 5, width - 10, height - 10, 25, 25);
    }

    public void drawPauseScreen() {
        g2.setFont(g2.getFont().deriveFont(Font.PLAIN, 80F));
        String text = "PAUSED";
        int x = getXForCenteredText(text);
        int y = getYForCenteredText(text);

        g2.drawString(text, x, y);
    }

    public int getXForCenteredText(String text) {
        int length = (int) g2.getFontMetrics().getStringBounds(text, g2).getWidth();
        return GamePanel.SCREEN_WIDTH / 2 - length / 2;
    }

    public int getYForCenteredText(String text) {
        int height = (int) g2.getFontMetrics().getStringBounds(text, g2).getHeight();
        return GamePanel.SCREEN_HEIGHT / 2 + height / 2 - 20;
    }
}
