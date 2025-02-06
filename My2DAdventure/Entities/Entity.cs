using System.Numerics;
using Raylib_cs;

namespace My2DAdventure.Entities;

public enum EntityType
{
    Player,
    Npc,
    Monster
}

public abstract class Entity(Texture2D textures, int tileSize, Vector2 worldPosition)
{
    protected int SpriteCounter = 0;
    protected int SpriteNum = 1;
    public abstract int Id { get; init; }
    public int TileSize { get; } = tileSize;
    public string Direction { get; protected set; } = "down";
    public abstract Dictionary<string, Rectangle> SrcRects { get; }
    protected Texture2D Textures { get; } = textures;
    public Rectangle CollisionArea { get; } = new(8, 16, 32, 40);
    public Vector2 WorldPosition { get; protected set; } = worldPosition;
    public abstract int Health { get; protected set; }
    public abstract int MaxHealth { get; protected set; }
    public abstract int Speed { get; }
    public bool CollisionOn { get; protected internal set; }
    public abstract EntityType Type { get; init; }
    public abstract void Update();
    public abstract void Draw();
    public abstract void SetDirection();
}