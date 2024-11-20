package dev.jonbeard.Asset;

import dev.jonbeard.Entity.OldMan;
import dev.jonbeard.GamePanel;

public class AssetManager {
    GamePanel gp;

    public AssetManager(GamePanel gp) {
        this.gp = gp;
    }

    public void setObject() {
        // empty for now
    }

    public void setNPC() {
        gp.npcs[0] = new OldMan(gp);
        gp.npcs[0].worldX = GamePanel.TILE_SIZE * 21;
        gp.npcs[0].worldY = GamePanel.TILE_SIZE * 21;
    }
}
