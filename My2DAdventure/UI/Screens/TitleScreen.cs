using System.Numerics;
using Raylib_cs;

namespace My2DAdventure.UI.Screens;

public class TitleScreen
{
    public int CaretPosition { get; private set; }
    public bool IsDone { get; private set; }

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

        if (Raylib.IsKeyPressed(KeyboardKey.Enter)) IsDone = true;
    }

    private void DrawCaret()
    {
        const int defaultYPos = 355;
        const string caret = "->";
        switch (CaretPosition)
        {
            case 0:
                Raylib.DrawText(caret, 200, defaultYPos, 30, Color.White);
                break;
            case 1:
                Raylib.DrawText(caret, 200, defaultYPos + 50, 30, Color.White);
                break;
            case 2:
                Raylib.DrawText(caret, 200, defaultYPos + 100, 30, Color.White);
                break;
        }
    }

    private void DrawBorder()
    {
        const int padding = 10;
        var screenWidth = Raylib.GetScreenWidth();
        var screenHeight = Raylib.GetScreenHeight();
        Raylib.DrawLine(padding, padding, padding, screenHeight - padding, Color.White);
        Raylib.DrawLine(padding, padding, screenWidth - padding, padding, Color.White);
        Raylib.DrawLine(screenWidth - padding, padding, screenWidth - padding, screenHeight - padding, Color.White);
        Raylib.DrawLine(padding, screenHeight - padding, screenWidth - padding, screenHeight - padding, Color.White);
    }

    private void DrawTitleText()
    {
        const string titleText = "My2DAdventure - Raylib";
        var titleSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), titleText, 50, 1);
        var titleLocation = new Vector2((float)Raylib.GetScreenWidth() / 2 - titleSize.X / 2, 75);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), titleText, titleLocation, 50, 1, Color.White);
    }

    private void DrawTitleMenu()
    {
        var screenMiddle = Raylib.GetScreenWidth() / 2;
        const int defaultYPos = 345;
        var menuLabels = new List<string> { "New Game", "Load Game", "Quit" };
        var menuSizes = new List<Vector2>
        {
            Raylib.MeasureTextEx(Raylib.GetFontDefault(), menuLabels[0], 50, 1),
            Raylib.MeasureTextEx(Raylib.GetFontDefault(), menuLabels[1], 50, 1),
            Raylib.MeasureTextEx(Raylib.GetFontDefault(), menuLabels[2], 50, 1)
        };
        for (int i = 0, padding = 0; i < 3; i++, padding += 50)
        {
            var labelLocation = new Vector2(screenMiddle - menuSizes[i].X / 2, defaultYPos + padding);
            Raylib.DrawTextEx(Raylib.GetFontDefault(), menuLabels[i], labelLocation, 50, 1, Color.White);
        }
    }

    public void Draw()
    {
        DrawTitleMenu();
        DrawTitleText();
        DrawBorder();
        DrawCaret();
    }
}