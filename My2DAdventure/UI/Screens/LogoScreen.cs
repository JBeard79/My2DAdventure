using Raylib_cs;

namespace My2DAdventure.UI.Screens;

public class LogoScreen
{
    private readonly int _logoPositionX = Raylib.GetScreenWidth() / 2 - 128;
    private readonly int _logoPositionY = Raylib.GetScreenHeight() / 2 - 128;
    private float _alpha = 1.0F;
    private int _bottomSideRecWidth = 16;
    private int _framesCounter;
    private int _leftSideRecHeight = 16;
    private int _lettersCount;
    private int _rightSideRecHeight = 16;
    private int _state;
    private int _topSideRecWidth = 16;

    public bool IsDone { get; private set; }

    public void Update()
    {
        switch (_state)
        {
            case 0:
                _framesCounter++;

                if (_framesCounter == 120)
                {
                    _state = 1;
                    _framesCounter = 0; // Reset counter... will be used later...
                }

                break;
            case 1:
                _topSideRecWidth += 4;
                _leftSideRecHeight += 4;

                if (_topSideRecWidth == 256) _state = 2;
                break;
            case 2:
                _bottomSideRecWidth += 4;
                _rightSideRecHeight += 4;

                if (_bottomSideRecWidth == 256) _state = 3;
                break;
            case 3:
                _framesCounter++;

                if (_framesCounter % 12 == 0)
                {
                    // Every 12 frames, one more letter!
                    if (_lettersCount <= 10) _lettersCount++;

                    _framesCounter = 0;
                }

                if (_lettersCount >= 10)
                {
                    // When all letters have appeared, just fade out everything
                    _alpha -= 0.02F;

                    if (_alpha <= 0.0F)
                    {
                        _alpha = 0.0F;
                        _state = 4;
                    }
                }

                break;
            case 4:
                IsDone = true;
                break;
        }
    }

    public void Draw()
    {
        switch (_state)
        {
            case 0:
                if (_framesCounter / 15 % 2 == 0)
                    Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, 16, Color.Black);
                break;
            case 1:
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRecWidth, 16, Color.Black);
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, _leftSideRecHeight, Color.Black);
                break;
            case 2:
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRecWidth, 16, Color.Black);
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, _leftSideRecHeight, Color.Black);
                Raylib.DrawRectangle(_logoPositionX + 240, _logoPositionY, 16, _rightSideRecHeight, Color.Black);
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 240, _bottomSideRecWidth, 16, Color.Black);
                break;
            case 3:
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRecWidth, 16,
                    Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 16, 16, _leftSideRecHeight - 32,
                    Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(_logoPositionX + 240, _logoPositionY + 16, 16, _rightSideRecHeight - 32,
                    Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 240, _bottomSideRecWidth, 16,
                    Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(Raylib.GetScreenWidth() / 2 - 112, Raylib.GetScreenHeight() / 2 - 112, 224, 224,
                    Raylib.Fade(Color.RayWhite, _alpha));
                const string title = "raylib     ";
                Raylib.DrawText(title[.._lettersCount]
                    , Raylib.GetScreenWidth() / 2 - 44, Raylib.GetScreenHeight() / 2 + 48, 50,
                    Raylib.Fade(Color.Black, _alpha));
                break;
        }
    }
}