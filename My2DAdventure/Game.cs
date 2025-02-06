using System.Numerics;
using My2DAdventure.Entities;
using My2DAdventure.UI.Screens;
using My2DAdventure.World;
using My2DAdventure.World.Maps;
using Raylib_cs;

namespace My2DAdventure;

public enum GameState
{
    LogoScreen,
    TitleScreen,
    GameOn,
    DialogWindow,
    Pause,
    Quit
}

public class Game
{
    private readonly Map _map;
    private readonly Texture2D _textures = Raylib.LoadTexture("textures.png");
    public readonly LogoScreen LogoScreen = new();
    public readonly TitleScreen TitleScreen = new();

    public Game(Config config)
    {
        Config = config;
        Player = new Player(_textures, Config.TileSize, new Vector2(23 * Config.TileSize, 21 * Config.TileSize));
        _map = new OverWorld(_textures, Config.MaxMapCol, Config.MaxMapRow, config.TileSize, "worldV2.txt");
        Camera = Camera with { Zoom = 1 };
        Camera = Camera with { Offset = config.CameraOrigin };
    }

    public Player Player { get; }

    private List<Music> Music { get; } = [Raylib.LoadMusicStream("BlueBoyAdventure.mp3")];

    private List<Sound> SoundEffects { get; } =
    [
        Raylib.LoadSound("coin.wav"), Raylib.LoadSound("dooropen.wav"), Raylib.LoadSound("powerup.wav"),
        Raylib.LoadSound("fanfare.wav")
    ];

    public GameState GameState { get; private set; } = GameState.TitleScreen;
    public Camera2D Camera { get; set; }

    private Config Config { get; }

    public void CheckState()
    {
        switch (GameState)
        {
            case GameState.LogoScreen:
                if (LogoScreen.IsDone) GameState = GameState.TitleScreen;
                break;
            case GameState.TitleScreen:
                if (TitleScreen is { IsDone: true, CaretPosition: 0 }) GameState = GameState.GameOn;
                if (TitleScreen is { IsDone: true, CaretPosition: 2 }) GameState = GameState.Quit;
                break;
            case GameState.GameOn:
                if (Raylib.IsKeyPressed(KeyboardKey.P)) GameState = GameState.Pause;
                break;
            case GameState.DialogWindow:
                break;
            case GameState.Pause:
                if (Raylib.IsKeyPressed(KeyboardKey.P)) GameState = GameState.GameOn;
                break;
            case GameState.Quit:
                break;
            default:
                Console.WriteLine("Bad user...bad!"); break;
        }
    }

    public void Draw2DCore()
    {
        Raylib.BeginMode2D(Camera);
        _map.Draw(Camera);
        Player.Draw();
        Raylib.EndMode2D();
    }

    public void UnloadGame()
    {
        foreach (var music in Music) Raylib.UnloadMusicStream(music);
        foreach (var sound in SoundEffects) Raylib.UnloadSound(sound);
        Raylib.UnloadTexture(_textures);
        Raylib.CloseAudioDevice();
    }

    public void CheckTileCollision(Entity entity)
    {
        var leftWorldX = (int)(entity.WorldPosition.X + entity.CollisionArea.X);
        var rightWorldX = (int)(entity.WorldPosition.X + entity.CollisionArea.X + entity.CollisionArea.Width);
        var topWorldY = (int)(entity.WorldPosition.Y + entity.CollisionArea.Y);
        var bottomWorldY = (int)(entity.WorldPosition.Y + entity.CollisionArea.Y + entity.CollisionArea.Height);

        var leftCol = leftWorldX / entity.TileSize;
        var rightCol = rightWorldX / entity.TileSize;
        var topRow = topWorldY / entity.TileSize;
        var bottomRow = bottomWorldY / entity.TileSize;

        int tileNum1 = 0, tileNum2 = 0;

        switch (entity.Direction)
        {
            case "up":
                // --- ---
                // 	  X
                //
                topRow = (topWorldY - entity.Speed) / entity.TileSize;
                tileNum1 = _map.TileMap[topRow, leftCol].TileNumber;
                tileNum2 = _map.TileMap[topRow, rightCol].TileNumber;
                break;
            case "down":
                //
                // 	  X
                // ___ ___
                bottomRow = (bottomWorldY + entity.Speed) / entity.TileSize;
                tileNum1 = _map.TileMap[bottomRow, leftCol].TileNumber;
                tileNum2 = _map.TileMap[bottomRow, rightCol].TileNumber;
                break;
            case "left":
                // |
                //    X
                // |
                leftCol = (leftWorldX - entity.Speed) / entity.TileSize;
                tileNum1 = _map.TileMap[topRow, leftCol].TileNumber;
                tileNum2 = _map.TileMap[bottomRow, leftCol].TileNumber;
                break;
            case "right":
                //		 |
                //    X
                // 		 |
                rightCol = (rightWorldX + entity.Speed) / entity.TileSize;
                tileNum1 = _map.TileMap[topRow, rightCol].TileNumber;
                tileNum2 = _map.TileMap[bottomRow, rightCol].TileNumber;
                break;
        }

        if (Map.CollisionTiles.Contains(tileNum1) || Map.CollisionTiles.Contains(tileNum2)) entity.CollisionOn = true;
    }
}