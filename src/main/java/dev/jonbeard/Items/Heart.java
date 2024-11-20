package dev.jonbeard.Items;

import dev.jonbeard.GamePanel;

import javax.imageio.ImageIO;
import java.io.IOException;
import java.util.Objects;

public class Heart extends Item {
    GamePanel gp;

    public Heart(GamePanel gp) {
        this.gp = gp;
        name = "Heart";
        collisionOn = true;
        try {
            image = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream("/textures/objects/heart_full.png")));
            image = utilityTools.scaleImage(image, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);
            image2 = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream("/textures/objects/heart_half.png")));
            image2 = utilityTools.scaleImage(image2, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);
            image3 = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream("/textures/objects/heart_blank.png")));
            image3 = utilityTools.scaleImage(image3, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
