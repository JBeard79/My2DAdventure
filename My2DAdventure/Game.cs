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
    private readonly Config _config;
    private readonly Map _map;
    private readonly Texture2D _textures = Raylib.LoadTexture("textures.png");
    public readonly LogoScreen LogoScreen = new();
    public readonly TitleScreen TitleScreen = new();

    public Game(Config config)
    {
        _config = config;
        _map = new OverWorld(_textures, _config.MaxMapCol, _config.MaxMapRow, config.TileSize, "worldV2.txt");
    }

    private List<Music> Music { get; } = [Raylib.LoadMusicStream("BlueBoyAdventure.mp3")];

    private List<Sound> SoundEffects { get; } =
    [
        Raylib.LoadSound("coin.wav"), Raylib.LoadSound("dooropen.wav"), Raylib.LoadSound("powerup.wav"),
        Raylib.LoadSound("fanfare.wav")
    ];

    public GameState GameState { get; private set; } = GameState.TitleScreen;
    public Camera2D Camera { get; private set; } = new();

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
        Raylib.ClearBackground(Color.White);

        _map.Draw();
    }

    public void UnloadGame()
    {
        foreach (var music in Music) Raylib.UnloadMusicStream(music);
        foreach (var sound in SoundEffects) Raylib.UnloadSound(sound);
        Raylib.UnloadTexture(_textures);
        Raylib.CloseAudioDevice();
    }
}