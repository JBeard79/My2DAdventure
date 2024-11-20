package dev.jonbeard.Tile;

import dev.jonbeard.GamePanel;
import dev.jonbeard.UtilityTools;

import javax.imageio.ImageIO;
import java.awt.*;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Objects;

public class TileManager {
    public Tile[] tiles;
    public int[][] mapTileNum;
    GamePanel gp;

    public TileManager(GamePanel gp) {
        this.gp = gp;
        tiles = new Tile[50];
        mapTileNum = new int[GamePanel.MAX_WORLD_COLUMN][GamePanel.MAX_WORLD_ROW];
        getTileImage();
        loadMap("/maps/worldV2.txt");
    }

    public void getTileImage() {
        // 0-9 placeholders
        setup(0, "grass00", false);
        setup(1, "grass00", false);
        setup(2, "grass00", false);
        setup(3, "grass00", false);
        setup(4, "grass00", false);
        setup(5, "grass00", false);
        setup(6, "grass00", false);
        setup(7, "grass00", false);
        setup(8, "grass00", false);
        setup(9, "grass00", false);
        // Real deal
        setup(10, "grass00", false);
        setup(11, "grass01", false);
        setup(12, "water00", true);
        setup(13, "water01", true);
        setup(14, "water02", true);
        setup(15, "water03", true);
        setup(16, "water04", true);
        setup(17, "water05", true);
        setup(18, "water06", true);
        setup(19, "water07", true);
        setup(20, "water08", true);
        setup(21, "water09", true);
        setup(22, "water10", true);
        setup(23, "water11", true);
        setup(24, "water12", true);
        setup(25, "water13", true);
        setup(26, "road00", false);
        setup(27, "road01", false);
        setup(28, "road02", false);
        setup(29, "road03", false);
        setup(30, "road04", false);
        setup(31, "road05", false);
        setup(32, "road06", false);
        setup(33, "road07", false);
        setup(34, "road08", false);
        setup(35, "road09", false);
        setup(36, "road10", false);
        setup(37, "road11", false);
        setup(38, "road12", false);
        setup(39, "earth", false);
        setup(40, "wall", true);
        setup(41, "tree", true);
    }

    public void loadMap(String filePath) {
        try {
            InputStream is = getClass().getResourceAsStream(filePath);
            BufferedReader br = new BufferedReader(new InputStreamReader(is));

            int column = 0;
            int row = 0;

            while (column < GamePanel.MAX_WORLD_COLUMN && row < GamePanel.MAX_WORLD_ROW) {
                String line = br.readLine();

                while (column < GamePanel.MAX_WORLD_COLUMN) {

                    String[] numbers = line.split(" ");
                    int num = Integer.parseInt(numbers[column]);
                    mapTileNum[column][row] = num;
                    column++;
                }
                if (column == GamePanel.MAX_WORLD_COLUMN) {
                    column = 0;
                    row++;
                }
            }
            br.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void setup(int index, String imageName, boolean collision) {
        UtilityTools utilityTools = new UtilityTools();
        try {
            tiles[index] = new Tile();
            tiles[index].image = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream("/textures/Tiles/" + imageName + ".png")));
            tiles[index].image = utilityTools.scaleImage(tiles[index].image, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);
            tiles[index].collision = collision;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void draw(Graphics2D g2) {
        int worldColumn = 0;
        int worldRow = 0;

        while (worldColumn < GamePanel.MAX_WORLD_COLUMN && worldRow < GamePanel.MAX_WORLD_ROW) {
            int tileNum = mapTileNum[worldColumn][worldRow];
            int worldX = worldColumn * GamePanel.TILE_SIZE;
            int worldY = worldRow * GamePanel.TILE_SIZE;
            int screenX = worldX - gp.player.worldX + gp.player.screenX;
            int screenY = worldY - gp.player.worldY + gp.player.screenY;

            if (worldX + GamePanel.TILE_SIZE > gp.player.worldX - gp.player.screenX &&
                    worldX - GamePanel.TILE_SIZE < gp.player.worldX + gp.player.screenX &&
                    worldY + GamePanel.TILE_SIZE > gp.player.worldY - gp.player.screenY &&
                    worldY - GamePanel.TILE_SIZE < gp.player.worldY + gp.player.screenY) {
                g2.drawImage(tiles[tileNum].image, screenX, screenY, null);
            }
            worldColumn++;

            if (worldColumn == GamePanel.MAX_WORLD_COLUMN) {
                worldColumn = 0;
                worldRow++;
            }
        }
    }
}
