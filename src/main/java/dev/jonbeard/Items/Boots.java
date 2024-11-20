package dev.jonbeard.Items;

import dev.jonbeard.GamePanel;

import javax.imageio.ImageIO;
import java.io.IOException;
import java.util.Objects;

public class Boots extends Item {
    GamePanel gp;

    public Boots(GamePanel gp) {
        name = "Boots";
        try {
            image = ImageIO.read(Objects.requireNonNull(getClass().getResourceAsStream("/textures/objects/boots.png")));
            image = utilityTools.scaleImage(image, GamePanel.TILE_SIZE, GamePanel.TILE_SIZE);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}