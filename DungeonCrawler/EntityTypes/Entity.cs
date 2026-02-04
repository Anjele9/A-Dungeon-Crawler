
namespace DungeonCrawler.EntityTypes;

public abstract class Entity
{
    public Sprite Sprite { get; protected set; }
    public Vector2 _position { get; protected set; }
    public bool IsImmune { get; protected set; }
    public bool isPassable {get; protected set;}
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
    public void DrawBounds(SpriteBatch spriteBatch, Texture2D pixel, Color color)
    {
        Rectangle bounds = new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            (int)Sprite.Width,
            (int)Sprite.Height
        );
        spriteBatch.Draw(pixel, bounds, color * 0.5f);
    }
    public List<Entity> CheckCollisions(List<Entity> entities)
    {
        List<Entity> collisions = new List<Entity>();
        foreach (var entity in entities)
        {
            if (entity == this || !entity.IsCollidable)
                continue;

            if (Bounds.Intersects(entity.Bounds))
            {
                collisions.Add(entity);
            }
        }
        return collisions;
    }
    public bool CanMove(Vector2 movement, List<Entity> entities, Rectangle roomBounds)
    {
        // Create a temporary circle at the new position
        Circle futureBounds = new Circle(
            (int)(Position.X + movement.X),
            (int)(Position.Y + movement.Y),
            Bounds.Radius
        );

        // Check room bounds
        if (futureBounds.Left < roomBounds.Left ||
            futureBounds.Right > roomBounds.Right ||
            futureBounds.Top > roomBounds.Bottom ||
            futureBounds.Bottom < roomBounds.Top)
        {
            return false;
        }

        // Check collisions with non-passable entities
        foreach (var entity in entities)
        {
            if (entity == this || !entity.IsCollidable)
                continue;

            // Can't move through non-passable entities
            if (!entity.isPassable && futureBounds.Intersects(entity.Bounds))
                return false;
        }

        return true;
    }
}