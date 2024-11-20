package dev.jonbeard.Items;

import dev.jonbeard.GamePanel;

import javax.imageio.ImageIO;
import java.io.IOException;
import java.util.Objects;

public class Key extends Item {
    GamePanel gp;

    public Key(GamePanel gp) {
        this.gp = gp;
        name = "Key";
        try {
            image = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream("/textures/objects/key.png")));
            image = utilityTools.scaleImage(image, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
