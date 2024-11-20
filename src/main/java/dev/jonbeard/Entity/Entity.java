package dev.jonbeard.Entity;


import dev.jonbeard.GamePanel;
import dev.jonbeard.UtilityTools;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.Objects;

public class Entity {
    public int worldX, worldY;
    public int speed;

    public BufferedImage up1, up2, down1, down2, left1, left2, right1, right2;
    public String direction;
    public int spriteCounter = 0;
    public int spriteNumber = 1;
    public Rectangle solidArea = new Rectangle(0, 0, 48, 48);
    public int solidAreaDefaultX, solidAreaDefaultY;
    public boolean collisionOn = false;
    public int actionLockCounter = 0;
    public int maxLife;
    public int life;
    GamePanel gp;
    String[] dialogues = new String[20];
    int dialogIndex = 0;

    public Entity(GamePanel gp) {
        this.gp = gp;
    }


    public BufferedImage setup(String imageName) {
        UtilityTools utilityTools = new UtilityTools();
        BufferedImage image = null;

        try {
            image = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream(imageName)));
            image = utilityTools.scaleImage(image, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return image;
    }

    public void draw(Graphics2D g2) {
        BufferedImage image = null;
        int screenX = worldX - gp.player.worldX + gp.player.screenX;
        int screenY = worldY - gp.player.worldY + gp.player.screenY;

        if (worldX + GamePanel.TILE_SIZE > gp.player.worldX - gp.player.screenX &&
                worldX - GamePanel.TILE_SIZE < gp.player.worldX + gp.player.screenX &&
                worldY + GamePanel.TILE_SIZE > gp.player.worldY - gp.player.screenY &&
                worldY - GamePanel.TILE_SIZE < gp.player.worldY + gp.player.screenY) {
            switch (direction) {
                case "up":
                    if (spriteNumber == 1) {
                        image = up1;
                    }
                    if (spriteNumber == 2) {
                        image = up2;
                    }
                    break;
                case "down":
                    if (spriteNumber == 1) {
                        image = down1;
                    }
                    if (spriteNumber == 2) {
                        image = down2;
                    }
                    break;
                case "left":
                    if (spriteNumber == 1) {
                        image = left1;
                    }
                    if (spriteNumber == 2) {
                        image = left2;
                    }
                    break;
                case "right":
                    if (spriteNumber == 1) {
                        image = right1;
                    }
                    if (spriteNumber == 2) {
                        image = right2;
                    }
            }
            g2.drawImage(image, screenX, screenY, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE, null);
        }
    }

    public void update() {
        setAction();
        collisionOn = false;
        gp.collisionChecker.checkTile(this);
        gp.collisionChecker.checkObject(this, false);
        gp.collisionChecker.checkPlayer(this);

        if (!collisionOn) {
            switch (direction) {
                case "up":
                    worldY -= speed;
                    break;
                case "down":
                    worldY += speed;
                    break;
                case "left":
                    worldX -= speed;
                    break;
                case "right":
                    worldX += speed;
                    break;
            }
        }

        spriteCounter++;
        if (spriteCounter > 10) {
            if (spriteNumber == 1) spriteNumber = 2;
            else if (spriteNumber == 2) spriteNumber = 1;
            spriteCounter = 0;
        }
    }

    public void setAction() {
    }

    public void speak() {
        if (dialogues[dialogIndex] == null) {
            dialogIndex = 0;
        }
        gp.userInterface.currentDialogue = dialogues[dialogIndex];
        dialogIndex++;

        switch (gp.player.direction) {
            case "up":
                direction = "down";
                break;
            case "down":
                direction = "up";
                break;
            case "left":
                direction = "right";
                break;
            case "right":
                direction = "left";
                break;
        }
    }

}
