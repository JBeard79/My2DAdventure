package dev.jonbeard.Entity;

import dev.jonbeard.GamePanel;

import java.util.Random;

public class OldMan extends Entity {
    public OldMan(GamePanel gp) {
        super(gp);
        direction = "down";
        speed = 1;
        getNPCImage();
        setDialogue();
    }

    public void getNPCImage() {
        String filePath = "/textures/npcs/oldman_";
        up1 = setup(filePath + "up_1.png");
        up2 = setup(filePath + "up_2.png");
        down1 = setup(filePath + "down_1.png");
        down2 = setup(filePath + "down_2.png");
        left1 = setup(filePath + "left_1.png");
        left2 = setup(filePath + "left_2.png");
        right1 = setup(filePath + "right_1.png");
        right2 = setup(filePath + "right_2.png");
    }

    public void setDialogue() {
        dialogues[0] = "Hello, lad.";
        dialogues[1] = "So you've come to this island\nto find the treasure?";
        dialogues[2] = "I used to be a great wizard\nbut now... I'm a bit to old for\ntaking an adventure. ";
        dialogues[3] = "Well, good luck to you.";

    }

    public void setAction() {
        Random random = new Random();
        actionLockCounter++;
        if (actionLockCounter == 120) {
            int i = random.nextInt(100) + 1;
            if (i <= 25) {
                direction = "up";
            }
            if (i > 25 && i <= 50) {
                direction = "down";
            }
            if (i > 50 && i <= 75) {
                direction = "left";
            }
            if (i > 75) {
                direction = "right";
            }
            actionLockCounter = 0;
        }

    }

    public void speak() {
        super.speak();
    }


}
