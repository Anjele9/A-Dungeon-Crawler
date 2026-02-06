namespace DungeonCrawler.Widgets;

public class PauseButton : RectangleButton
{
    public bool IsPaused { get; private set; }
    private MouseState _previousMouseState;

    public PauseButton(Rectangle bounds, Texture2D texture, string text = "Pause") 
        : base(bounds, texture, text)
    {
        IsPaused = false;
        _previousMouseState = Mouse.GetState();
        NormalColor = Color.Gray;
        HoverColor = Color.DarkGray;
        PressColor = Color.Black;
    }

    public override void Update(MouseState mouse)
    {
        base.Update(mouse);
        
        // Detect click (when button was just pressed this frame)
        if (_isHovering && 
            _previousMouseState.LeftButton == ButtonState.Released && 
            mouse.LeftButton == ButtonState.Pressed)
        {
            IsPaused = !IsPaused;
        }
        
        _previousMouseState = mouse;
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        // Draw the button background
        Color drawColor = NormalColor;
        if (_isPressed) drawColor = PressColor;
        else if (_isHovering) drawColor = HoverColor;
        spriteBatch.Draw(Texture, Bounds, drawColor);
        
        // Draw the text centered
        Vector2 textSize = font.MeasureString(Text);
        Vector2 textPosition = new Vector2(
            Bounds.X + (Bounds.Width - textSize.X) / 2f,
            Bounds.Y + (Bounds.Height - textSize.Y) / 2f
        );
        spriteBatch.DrawString(font, Text, textPosition, Color.White);
    }
}
