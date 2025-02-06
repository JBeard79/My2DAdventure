using System.Numerics;
using My2DAdventure.Maps;
using Raylib_cs;

namespace My2DAdventure.World;

public abstract class Map(Texture2D textures, int mapMaxCol, int mapMaxRow, int tileSize, string mapName)
{
    public Tile[,] TileMap { get; } = LoadTileMap(mapMaxCol, mapMaxRow, mapName);

    public abstract Event[]? EventTiles { get; init; }

    public static List<int> CollisionTiles { get; } =
        [12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 40, 41];

    private Rectangle[] SrcRects { get; } =
    {
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // placeholder
        new(619, 290, 48, 48), // grass00
        new(193, 459, 48, 48), // grass01
        new(555, 481, 48, 48), // water00
        new(603, 482, 48, 48), // water01
        new(651, 482, 48, 48), // water02
        new(287, 491, 48, 48), // water03
        new(186, 548, 48, 48), // water04
        new(186, 596, 48, 48), // water05
        new(241, 539, 48, 48), // water06
        new(234, 587, 48, 48), // water07
        new(282, 587, 48, 48), // water08
        new(253, 635, 48, 48), // water09
        new(335, 526, 48, 48), // water10
        new(383, 526, 48, 48), // water11
        new(374, 574, 48, 48), // water12
        new(431, 527, 48, 48), // water13
        new(651, 338, 48, 48), // road00
        new(370, 382, 48, 48), // road01
        new(459, 383, 48, 48), // road02
        new(507, 385, 48, 48), // road03
        new(555, 385, 48, 48), // road04
        new(603, 386, 48, 48), // road05
        new(651, 386, 48, 48), // road06
        new(370, 430, 48, 48), // road07
        new(459, 431, 48, 48), // road08
        new(507, 433, 48, 48), // road09
        new(555, 433, 48, 48), // road10
        new(603, 434, 48, 48), // road11
        new(651, 434, 48, 48), // road12
        new(523, 289, 48, 48), // earth
        new(507, 481, 48, 48), // wall
        new(459, 479, 48, 48) // tree
    };

    private static Tile[,] LoadTileMap(int mapMaxCol, int mapMaxRow, string mapName)
    {
        var map = new Tile[mapMaxCol, mapMaxRow];
        var input = File.ReadAllText(mapName);
        int i = 0, j = 0;
        foreach (var row in input.Split('\n'))
        {
            foreach (var col in row.Trim().Split(' '))
            {
                var tileNum = int.Parse(col);

                if (CollisionTiles.Contains(tileNum))
                    map[i, j] = new Tile(tileNum, true);
                else
                    map[i, j] = new Tile(tileNum, false);

                j++;
            }

            i++;
            j = 0;
        }

        return map;
    }

    public void Draw(Camera2D camera)
    {
        var cameraStart = Raylib.GetScreenToWorld2D(new Vector2(), camera);
        var cameraEnd =
            Raylib.GetScreenToWorld2D(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), camera);

        for (var row = (int)cameraStart.Y / tileSize; row < (int)cameraEnd.Y / tileSize + 1; row++)
        {
            for (var col = (int)cameraStart.X / tileSize; col < (int)cameraEnd.X / tileSize + 1; col++)
            {
                // Figure out how to get mapMaxCol/Row to work here.  Only works with const.
                if (row is < 0 or >= 50 || col is < 0 or >= 50)
                    continue;

                var tilePosition = new Vector2(col * tileSize, row * tileSize);
                var destRect = new Rectangle(tilePosition.X, tilePosition.Y, tileSize, tileSize);
                var tileNum = TileMap[row, col].TileNumber;
                Raylib.DrawTexturePro(textures, SrcRects[tileNum], destRect, new Vector2(), 0, Color.White);
            }
        }
    }
}