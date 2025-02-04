using Raylib_cs;
using Rectangle = System.Drawing.Rectangle;

namespace My2DAdventure.World.Maps;

public class OverWorld(Texture2D textures, int mapMaxCol, int mapMaxRow, int tileSize, string mapName)
    : Map(textures, mapMaxCol, mapMaxRow, tileSize, mapName)
{
    public override Event[]? EventTiles { get; init; } =
    [
        new Event("You have fallen into a pit!",
            new Rectangle(27 * tileSize, 16 * tileSize, tileSize, tileSize), 2, true, false),
        new Event("You drink the water.  You are healed",
            new Rectangle(23 * tileSize, 12 * tileSize, tileSize, tileSize), 2, false, false)
    ];
}