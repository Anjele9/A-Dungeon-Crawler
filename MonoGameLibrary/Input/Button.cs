

namespace MonoGameLibrary.Input;
public abstract class Button
{
    public Texture2D Texture;
    public string Text;
    public Color NormalColor;
    public Color HoverColor;
    public Color PressColor;
    public bool _isHovering {get; protected set;}
    public bool _isPressed {get; protected set; }

    public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);
    public abstract void Update(MouseState mouse);

}


public class CircularButton : Button
{
    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
    }
    public override void Update(MouseState mouse)
    {
    }
} 

public class RectangleButton : Button
{
    public Rectangle Bounds;

    public RectangleButton(Rectangle bounds, Texture2D Texture, string text = "")
    {
        Bounds = bounds;
        this.Texture = Texture;
        Text = text;
    }
    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        Color drawColor = NormalColor;
        if (_isPressed) drawColor = PressColor;
        else if (_isHovering) drawColor = HoverColor;
        spriteBatch.Draw(Texture, Bounds, drawColor);
    }
    public override void Update(MouseState mouse)
    {
        Point mousePos = mouse.Position;
        _isHovering = Bounds.Contains(mousePos);
        if (_isHovering && mouse.LeftButton == ButtonState.Pressed) _isPressed = true;
    }
}