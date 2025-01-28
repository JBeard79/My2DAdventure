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

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}