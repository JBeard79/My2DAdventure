using System.Numerics;
using Raylib_cs;

namespace My2DAdventure;

internal static class Program
{
    private const string Title = "My2DAdventure - Raylib";
    private const int TileSize = 48;
    private const int MaxScreenCol = 16;
    private const int MaxScreenRow = 12;
    private const int ScreenWidth = MaxScreenCol * TileSize;
    private const int ScreenHeight = MaxScreenRow * TileSize;
    private const int MaxWorldCol = 50;
    private const int MaxWorldRow = 50;
    private const int TargetFps = 60;

    public static void Main()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, Title);
        Raylib.SetTargetFPS(TargetFps);
        Raylib.SetConfigFlags(ConfigFlags.VSyncHint | ConfigFlags.Msaa4xHint | ConfigFlags.HighDpiWindow);
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.InitAudioDevice();

        var game = new Game(new Config
        {
            CameraOrigin = new Vector2((float)Raylib.GetScreenWidth() / 2 - 24,
                (float)Raylib.GetScreenHeight() / 2 - 24),
            TileSize = TileSize,
            MaxScreenCol = MaxScreenCol,
            MaxScreenRow = MaxScreenRow,
            ScreenWidth = ScreenWidth,
            ScreenHeight = ScreenHeight,
            MaxWorldCol = MaxWorldCol,
            MaxWorldRow = MaxWorldRow
        });

        while (!Raylib.WindowShouldClose() && game.GameState != GameState.Quit)
        {
            // UPDATE
            switch (game.GameState)
            {
                case GameState.LogoScreen:
                    game.CheckState();
                    game.LogoScreen.Update();
                    break;
                case GameState.TitleScreen:
                    game.CheckState();
                    game.TitleScreen.Update();
                    break;
                case GameState.GameOn:
                    game.CheckState();
                    break;
                case GameState.DialogWindow:
                    game.CheckState();
                    break;
                case GameState.Pause:
                    game.CheckState();
                    break;
                case GameState.Quit:
                    break;
                default:
                    Console.WriteLine("Bad user....Bad!");
                    break;
            }

            // DRAW
            Raylib.BeginDrawing();

            switch (game.GameState)
            {
                case GameState.LogoScreen:
                    Raylib.ClearBackground(Color.RayWhite);
                    game.LogoScreen.Draw();
                    break;
                case GameState.TitleScreen:
                    Raylib.ClearBackground(Color.Black);
                    game.TitleScreen.Draw();
                    break;
                case GameState.GameOn:
                    Raylib.ClearBackground(Color.Pink);
                    break;
                case GameState.DialogWindow:
                    break;
                case GameState.Pause:
                    Raylib.ClearBackground(Color.Red);
                    break;
                case GameState.Quit:
                    break;
                default:
                    Console.WriteLine("Bad user....Bad!");
                    break;
            }

            Raylib.EndDrawing();
        }


        UnloadGame(game);

        Raylib.CloseWindow();
    }

    private static void UnloadGame(Game game)
    {
        Raylib.UnloadTexture(game.Textures);
        foreach (var music in game.Music) Raylib.UnloadMusicStream(music);

        foreach (var sound in game.SoundEffects) Raylib.UnloadSound(sound);
        Raylib.CloseAudioDevice();
    }
}