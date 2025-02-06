using System.Numerics;
using Raylib_cs;

namespace My2DAdventure.Entities;

public class Player(Texture2D textures, int tileSize, Vector2 worldPosition) : Entity(textures, tileSize, worldPosition)
{
    private readonly bool _eventCollision = false;
    private bool _attacking;
    private bool _invulnerable;
    private int _invulnerableTimer;
    private bool _npcCollision;
    public override int Id { get; init; } = 9001;

    public override Dictionary<string, Rectangle> SrcRects { get; } = new()
    {
        { "up1", new Rectangle(617, 530, 46, 48) },
        { "up2", new Rectangle(560, 578, 46, 48) },
        { "down1", new Rectangle(525, 529, 46, 48) },
        { "down2", new Rectangle(468, 577, 46, 48) },
        { "left1", new Rectangle(650, 578, 44, 48) },
        { "left2", new Rectangle(514, 577, 46, 48) },
        { "right1", new Rectangle(390, 623, 44, 48) },
        { "right2", new Rectangle(570, 530, 46, 48) },
        { "attackUp1", new Rectangle(638, 241, 46, 49) },
        { "attackUp2", new Rectangle(195, 334, 44, 76) },
        { "attackDown1", new Rectangle(638, 192, 44, 49) },
        { "attackDown2", new Rectangle(145, 509, 41, 76) },
        { "attackLeft1", new Rectangle(678, 130, 35, 48) },
        { "attackLeft2", new Rectangle(296, 190, 68, 48) },
        { "attackRight1", new Rectangle(440, 286, 35, 48) },
        { "attackRight2", new Rectangle(364, 190, 68, 48) }
    };

    public override int Health { get; protected set; } = 6;
    public override int MaxHealth { get; protected set; } = 6;
    public override int Speed => 4;
    public override EntityType Type { get; init; } = EntityType.Player;

    public override void Update()
    {
        SetDirection();
        CheckInvulnerable();
        CheckAttack();

        if (!_attacking) UpdateAvatar();

        if (!CollisionOn && !_attacking) Move();

        CollisionOn = false;
        _npcCollision = false;
    }

    public override void SetDirection()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W)) Direction = "up";
        else if (Raylib.IsKeyDown(KeyboardKey.S)) Direction = "down";
        else if (Raylib.IsKeyDown(KeyboardKey.A)) Direction = "left";
        else if (Raylib.IsKeyDown(KeyboardKey.D)) Direction = "right";
    }

    private void CheckInvulnerable()
    {
        if (_invulnerable) _invulnerableTimer++;

        if (_invulnerableTimer != 120) return;
        _invulnerable = false;
        _invulnerableTimer = 0;
    }

    private void CheckAttack()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter) && !_attacking && !_npcCollision && !_eventCollision)
        {
            SpriteCounter = 0;
            SpriteNum = 1;
            _attacking = true;
        }

        if (!_attacking) return;

        SpriteCounter++;
        switch (SpriteCounter)
        {
            case <= 5:
                SpriteNum = 1;
                break;
            case <= 25 and > 5:
                SpriteNum = 2;
                break;
            case > 25:
                SpriteNum = 1;
                SpriteCounter = 0;
                _attacking = false;
                break;
        }
    }

    public void UpdateAvatar()
    {
        SpriteCounter++;
        if (SpriteCounter < 10) return;
        SpriteNum = SpriteNum switch
        {
            1 => 2,
            2 => 1,
            _ => SpriteNum
        };
        SpriteCounter = 0;
    }

    public void DrawAvatar()
    {
        var avatarSize = TileSize * 4;
        var screenPosition = new Rectangle((float)Raylib.GetScreenWidth() / 2 - (float)avatarSize / 2,
            (float)Raylib.GetScreenHeight() / 2 - (float)avatarSize / 2, avatarSize, avatarSize);

        Raylib.DrawTexturePro(Textures, SpriteNum == 1 ? SrcRects["down1"] : SrcRects["down2"], screenPosition,
            new Vector2(), 0, Color.White);
    }

    private void Move()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W)) WorldPosition = WorldPosition with { Y = WorldPosition.Y - Speed };
        else if (Raylib.IsKeyDown(KeyboardKey.S)) WorldPosition = WorldPosition with { Y = WorldPosition.Y + Speed };
        else if (Raylib.IsKeyDown(KeyboardKey.A)) WorldPosition = WorldPosition with { X = WorldPosition.X - Speed };
        else if (Raylib.IsKeyDown(KeyboardKey.D)) WorldPosition = WorldPosition with { X = WorldPosition.X + Speed };
    }

    public override void Draw()
    {
        var color = Color.RayWhite;

        if (_invulnerable)
        {
            color = new Color(255, 255, 255, 255);
            color.A = 190;
        }

        switch (Direction)
        {
            case "up":
                if (_attacking)
                    DrawAttack("attackUp", color);
                else
                    DrawRunning("up", color);

                break;
            case "down":
                if (_attacking)
                    DrawAttack("attackDown", color);
                else
                    DrawRunning("down", color);

                break;
            case "left":
                if (_attacking)
                    DrawAttack("attackLeft", color);
                else
                    DrawRunning("left", color);

                break;
            case "right":
                if (_attacking)
                    DrawAttack("attackRight", color);
                else
                    DrawRunning("right", color);

                break;
        }
    }

    private void DrawRunning(string src, Color color)
    {
        var srcRect = SpriteNum == 1 ? SrcRects[src + "1"] : SrcRects[src + "2"];
        var destRec = new Rectangle(WorldPosition.X, WorldPosition.Y, srcRect.Width, srcRect.Height);
        Raylib.DrawTexturePro(Textures, srcRect, destRec, new Vector2(), 0, color);
    }

    private void DrawAttack(string src, Color color)
    {
        var srcRect = SpriteNum == 1 ? SrcRects[src + "1"] : SrcRects[src + "2"];
        var destRec = new Rectangle(WorldPosition.X, WorldPosition.Y, srcRect.Width, srcRect.Height);

        if (SpriteNum == 2)
        {
            if (src.Contains("Left")) destRec.X -= (float)TileSize / 2;
            else if (src.Contains("Up")) destRec.Y -= (float)TileSize / 2;
        }

        Raylib.DrawTexturePro(Textures, srcRect, destRec, new Vector2(), 0, color);
    }
}