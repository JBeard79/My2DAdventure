package dev.jonbeard.Items;

import dev.jonbeard.GamePanel;
import dev.jonbeard.UtilityTools;

import java.awt.*;
import java.awt.image.BufferedImage;


public class Item {
    public BufferedImage image;
    public BufferedImage image2;
    public BufferedImage image3;
    public String name;
    public boolean collisionOn = false;
    public int worldX, worldY;
    public Rectangle solidArea = new Rectangle(0, 0, 48, 48);
    public int solidAreaDefaultX = 0;
    public int solidAreaDefaultY = 0;
    UtilityTools utilityTools = new UtilityTools();

    public void draw(Graphics2D g2, GamePanel gp) {
        int screenX = worldX - gp.player.worldX + gp.player.screenX;
        int screenY = worldY - gp.player.worldY + gp.player.screenY;

        if (worldX + GamePanel.TILE_SIZE > gp.player.worldX - gp.player.screenX &&
                worldX - GamePanel.TILE_SIZE < gp.player.worldX + gp.player.screenX &&
                worldY + GamePanel.TILE_SIZE > gp.player.worldY - gp.player.screenY &&
                worldY - GamePanel.TILE_SIZE < gp.player.worldY + gp.player.screenY) {
            g2.drawImage(image, screenX, screenY, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE, null);
        }
    }
}
