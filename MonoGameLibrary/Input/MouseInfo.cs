using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.Input;

public class MouseInfo
{
    public MouseState previousState { get; private set; }
    public MouseState currentState { get; private set; }

    public Point Position
    {
        get => currentState.Position;
        set => SetPosition(value.X, value.Y);
    }

    public int X
    {
        get => currentState.X;
        set => SetPosition(value, currentState.Y);
    }
    
    public int Y
    {
        get => currentState.Y;
        set => SetPosition(currentState.X, value);
    }
    public Point PositionDelta => currentState.Position - previousState.Position; //Difference between current position and last position
    public int XDelta => currentState.Position.X - previousState.Position.X;
    public int YDelta => currentState.Position.Y - previousState.Position.Y;

    public int Scroll => currentState.ScrollWheelValue;

    public int ScrollDelta => currentState.ScrollWheelValue - previousState.ScrollWheelValue; //Checks how far the user has scrolled.

    public MouseInfo()
    {
        previousState = new MouseState();
        currentState = Mouse.GetState();
    }
    public void Update()
    {
        previousState = currentState;
        currentState = Mouse.GetState();
    }
    public void SetPosition(int x, int y)
    {
        Mouse.SetPosition(X, Y);
        currentState = new MouseState(x, y, currentState.ScrollWheelValue,
        currentState.LeftButton,
        currentState.MiddleButton,
        currentState.RightButton,
        currentState.XButton1,
        currentState.XButton2);
    }
}