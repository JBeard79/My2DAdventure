using System.Numerics;

namespace My2DAdventure;

public struct Config(
    Vector2 cameraOrigin,
    int tileSize,
    int maxScreenCol,
    int maxScreenRow,
    int screenWidth,
    int screenHeight,
    int maxWorldCol,
    int maxWorldRow)
{
    public Vector2 CameraOrigin { get; init; } = cameraOrigin;
    public int TileSize { get; init; } = tileSize;
    public int MaxScreenCol { get; init; } = maxScreenCol;
    public int MaxScreenRow { get; init; } = maxScreenRow;
    public int ScreenWidth { get; init; } = screenWidth;
    public int ScreenHeight { get; init; } = screenHeight;
    public int MaxWorldCol { get; init; } = maxWorldCol;
    public int MaxWorldRow { get; init; } = maxWorldRow;
}