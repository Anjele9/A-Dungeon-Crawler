
namespace DungeonCrawler.EntityTypes;

public abstract class Entity
{
    public Sprite Sprite { get; protected set; }
    public Vector2 _position { get; protected set; }
    public bool IsImmune { get; protected set; }
    public bool IsCollidable { get; protected set; }
    public Vector2 Scale { get; protected set; } = Vector2.One;
    public Circle Bounds;
    public Vector2 Position
    {
        get => _position;
        protected set
        {
            _position = value;

            // Keep bounds centered or aligned
            Bounds.X = (int)value.X;
            Bounds.Y = (int)value.Y;
        }
    }
    public virtual void Update(GameTime gameTime)
    {
        if (Sprite is AnimatedSprite or DirectionalAnimatedSprite)
            Sprite.Update(gameTime);
        return;
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Sprite?.Draw(spriteBatch, Position);
    }
}