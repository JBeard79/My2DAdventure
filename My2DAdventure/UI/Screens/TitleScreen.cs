using System.Numerics;
using Raylib_cs;

namespace My2DAdventure.UI.Screens;

public class TitleScreen
{
    public int CaretPosition { get; private set; }
    public bool IsDone { get; private set; }
    private List<string> MenuLabels { get; } = ["New Game", "Load Game", "Quit"];

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            if (CaretPosition == 2)
                CaretPosition = 0;
            else
                CaretPosition++;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            if (CaretPosition == 0)
                CaretPosition = 2;
            else
                CaretPosition--;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Enter) || Raylib.IsKeyPressed(KeyboardKey.KpEnter)) IsDone = true;
    }

    private void DrawCaret(int screenHeight)
    {
        const int fontSize = 30;
        const int posX = 250;
        var defaultYPos = screenHeight - screenHeight / 3 + fontSize / 2;
        const string caret = "->";
        switch (CaretPosition)
        {
            case 0:
                Raylib.DrawText(caret, posX, defaultYPos, fontSize, Color.White);
                break;
            case 1:
                Raylib.DrawText(caret, posX, defaultYPos + 50, fontSize, Color.White);
                break;
            case 2:
                Raylib.DrawText(caret, posX, defaultYPos + 100, fontSize, Color.White);
                break;
        }
    }

    private static void DrawBorder(int screenWidth, int screenHeight)
    {
        const int padding = 10;
        Raylib.DrawLine(padding, padding, padding, screenHeight - padding, Color.White);
        Raylib.DrawLine(padding, padding, screenWidth - padding, padding, Color.White);
        Raylib.DrawLine(screenWidth - padding, padding, screenWidth - padding, screenHeight - padding, Color.White);
        Raylib.DrawLine(padding, screenHeight - padding, screenWidth - padding, screenHeight - padding, Color.White);
    }

    private static void DrawTitleText(int screenWidth, int screenHeight)
    {
        const string titleText = "My2DAdventure - Raylib";
        const int fontSize = 50;
        var titleSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), titleText, fontSize, 1);
        var titleLocation =
            new Vector2((float)screenWidth / 2 - titleSize.X / 2,
                (float)screenHeight / 5 - (float)fontSize / 2);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), titleText, titleLocation, fontSize, 1, Color.White);
    }

    private void DrawTitleMenu(int screenWidth, int screenHeight)
    {
        var screenMiddle = screenWidth / 2;
        var defaultYPos = screenHeight - screenHeight / 3;

        for (int i = 0, padding = 0; i < 3; i++, padding += 50)
        {
            var menuItemSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), MenuLabels[i], 50, 1);
            var labelLocation = new Vector2(screenMiddle - menuItemSize.X / 2, defaultYPos + padding);
            Raylib.DrawTextEx(Raylib.GetFontDefault(), MenuLabels[i], labelLocation, 50, 1, Color.White);
        }
    }

    public void Draw()
    {
        var screenWidth = Raylib.GetScreenWidth();
        var screenHeight = Raylib.GetScreenHeight();
        DrawTitleMenu(screenWidth, screenHeight);
        DrawTitleText(screenWidth, screenHeight);
        DrawBorder(screenWidth, screenHeight);
        DrawCaret(screenHeight);
    }
}