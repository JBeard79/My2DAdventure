package dev.jonbeard.Entity;

import dev.jonbeard.GamePanel;
import dev.jonbeard.Input.KeyHandler;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Player extends Entity {
    public final int screenX;
    public final int screenY;
    KeyHandler keyH;

    public Player(GamePanel gp, KeyHandler keyH) {
        super(gp);
        this.keyH = keyH;
        screenX = GamePanel.SCREEN_WIDTH / 2 - (GamePanel.TILE_SIZE / 2);
        screenY = GamePanel.SCREEN_HEIGHT / 2 - (GamePanel.TILE_SIZE / 2);
        solidArea = new Rectangle(8, 16, 32, 32);
        solidAreaDefaultX = solidArea.x;
        solidAreaDefaultY = solidArea.y;
        setDefaultValues();
        getPlayerImage();
    }

    public void setDefaultValues() {
        worldX = GamePanel.TILE_SIZE * 23;
        worldY = GamePanel.TILE_SIZE * 21;
        speed = 4;
        direction = "down";
        maxLife = 6;
        life = maxLife;
    }

    public void getPlayerImage() {
        String filePath = "/textures/player/walk/boy_";
        up1 = setup(filePath + "up_1.png");
        up2 = setup(filePath + "up_2.png");
        down1 = setup(filePath + "down_1.png");
        down2 = setup(filePath + "down_2.png");
        left1 = setup(filePath + "left_1.png");
        left2 = setup(filePath + "left_2.png");
        right1 = setup(filePath + "right_1.png");
        right2 = setup(filePath + "right_2.png");
    }

    public void update() {
        if (keyH.upPressed) {
            direction = "up";
        } else if (keyH.downPressed) {
            direction = "down";
        } else if (keyH.leftPressed) {
            direction = "left";
        } else if (keyH.rightPressed) {
            direction = "right";
        }

        // COLLISION
        collisionOn = false;
        gp.collisionChecker.checkTile(this);
        int objectIndex = gp.collisionChecker.checkObject(this, true);
        pickUpObject(objectIndex);
        int npcIndex = gp.collisionChecker.checkEntity(this, gp.npcs);
        interactNPC(npcIndex);

        if (keyH.upPressed || keyH.downPressed || keyH.leftPressed || keyH.rightPressed) {
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
        }

        spriteCounter++;
        if (spriteCounter > 10) {
            if (spriteNumber == 1) spriteNumber = 2;
            else if (spriteNumber == 2) spriteNumber = 1;
            spriteCounter = 0;
        }
    }

    private void interactNPC(int npcIndex) {
        if (npcIndex != 999) {
            if (gp.keyH.enterPressed) {
                gp.gameState = gp.dialogState;
                gp.npcs[npcIndex].speak();
            }
        }
        gp.keyH.enterPressed = false;
    }

    public void pickUpObject(int index) {
        // empty for now
    }

    public void draw(Graphics2D g2) {
        BufferedImage image = null;

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
        g2.drawImage(image, screenX, screenY, null);
    }
}
