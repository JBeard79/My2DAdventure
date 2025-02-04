using System.Numerics;

namespace My2DAdventure;

public struct Config(
    Vector2 cameraOrigin,
    int tileSize,
    int defaultScreenWidth,
    int defaultScreenHeight,
    int maxMapCol,
    int maxMapRow)
{
    public Vector2 CameraOrigin { get; init; } = cameraOrigin;
    public int TileSize { get; init; } = tileSize;
    public int DefaultScreenWidth { get; init; } = defaultScreenWidth;
    public int DefaultScreenHeight { get; init; } = defaultScreenHeight;
    public int MaxMapCol { get; init; } = maxMapCol;
    public int MaxMapRow { get; init; } = maxMapRow;
}