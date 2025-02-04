namespace My2DAdventure.Maps;

public class Tile(int tileNumber, bool collisionOn)
{
    public int TileNumber { get; init; } = tileNumber;
    public bool CollisionOn { get; init; } = collisionOn;
}