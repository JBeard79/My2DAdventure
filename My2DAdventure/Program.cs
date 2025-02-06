using System.Numerics;
using Raylib_cs;

namespace My2DAdventure;

internal static class Program
{
    private const string Title = "My2DAdventure - Raylib";
    private const int TileSize = 48;
    private const int DefaultScreenWidth = 1024;
    private const int DefaultScreenHeight = 768;
    private const int MaxMapCol = 50;
    private const int MaxMapRow = 50;
    private const int TargetFps = 60;

    public static void Main()
    {
        Raylib.InitWindow(DefaultScreenWidth, DefaultScreenHeight, Title);
        Raylib.SetTargetFPS(TargetFps);
        Raylib.SetConfigFlags(ConfigFlags.VSyncHint | ConfigFlags.Msaa4xHint | ConfigFlags.HighDpiWindow);
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.InitAudioDevice();

        var game = new Game(new Config
        {
            CameraOrigin = new Vector2((float)Raylib.GetScreenWidth() / 2 - 24,
                (float)Raylib.GetScreenHeight() / 2 - 24),
            TileSize = TileSize,
            DefaultScreenWidth = DefaultScreenWidth,
            DefaultScreenHeight = DefaultScreenHeight,
            MaxMapCol = MaxMapCol,
            MaxMapRow = MaxMapRow
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
                    game.Player.UpdateAvatar();
                    break;
                case GameState.GameOn:
                    game.CheckState();
                    game.Camera = game.Camera with { Target = game.Player.WorldPosition };
                    game.Player.Update();
                    game.CheckTileCollision(game.Player);
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
                    game.Player.DrawAvatar();
                    break;
                case GameState.GameOn:
                    Raylib.ClearBackground(Color.Black);
                    game.Draw2DCore();
                    break;
                case GameState.DialogWindow:
                    Raylib.ClearBackground(Color.Black);
                    game.Draw2DCore();
                    Console.WriteLine("Dialog");
                    break;
                case GameState.Pause:
                    Raylib.ClearBackground(Color.Black);
                    game.Draw2DCore();
                    Console.WriteLine("Pause");
                    break;
                case GameState.Quit:
                    break;
                default:
                    Console.WriteLine("Bad user....Bad!");
                    break;
            }

            Raylib.EndDrawing();
        }

        game.UnloadGame();
        Raylib.CloseWindow();
    }
}