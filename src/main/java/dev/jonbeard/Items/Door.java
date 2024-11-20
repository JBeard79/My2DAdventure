package dev.jonbeard.Items;

import dev.jonbeard.GamePanel;

import javax.imageio.ImageIO;
import java.io.IOException;
import java.util.Objects;

public class Door extends Item {
    GamePanel gp;

    public Door(GamePanel gp) {
        this.gp = gp;
        name = "Door";
        collisionOn = true;
        try {
            image = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream("/textures/objects/door.png")));
            image = utilityTools.scaleImage(image, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

