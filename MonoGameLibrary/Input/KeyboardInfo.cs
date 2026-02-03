using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.Input;

public class KeyboardInfo
{
    public KeyboardState currentState { get; private set; }
    public KeyboardState previousState { get; private set; }

    public KeyboardInfo()
    {
        previousState = new KeyboardState();
        currentState = Keyboard.GetState();
    }
    public void Update()
    {
        previousState = currentState;
        currentState = Keyboard.GetState();
    }
    public bool isKeyDown(Keys key)
    {
        if (currentState.IsKeyDown(key))
        {
            return true;
        }
        return false;
    }
    public bool wasKeyPressed(Keys key)
    {
        if (currentState.IsKeyDown(key) && previousState.IsKeyUp(key))
            return true;
        return false;
    }
    public bool wasKeyDown(Keys key)
    {
        if (previousState.IsKeyUp(key) && previousState.IsKeyUp(key))
        {
            return true;
        }
        return false;
    }
    public bool wasKeyReleased(Keys key)
    {
        if (previousState.IsKeyDown(key) && currentState.IsKeyUp(key))
        {
            return true;
        }
        return false;
    }
    public bool anyKeyDown()
    {
        return currentState.GetPressedKeys().Length > 0;
    }
}