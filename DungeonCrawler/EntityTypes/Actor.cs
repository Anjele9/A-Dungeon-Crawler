
namespace DungeonCrawler.EntityTypes;

public abstract class Actor : Entity
{
    public float Speed { get; protected set; }
    public bool canMove {get; protected set;}
    public int HP { get; protected set; }
    public override void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, Position);
    }
    public void Move(Vector2 movement, List<Entity> entities, Rectangle roomBounds)
    {
        if (CanMove(movement, entities, roomBounds))
        {
            this.Position += movement;
            
            // Check for collisions after moving (for passable entities)
            var collisions = CheckCollisions(entities);
            foreach (var entity in collisions)
            {
                OnCollision(entity);
            }
        }
    }
    public virtual void OnCollision(Entity entity)
    {
        // Override this in derived classes for unique collision behavior
    }
    public void ChangeDirection(Direction direction)
    {
        if (Sprite is DirectionalAnimatedSprite DirectionalSprite)
        {
            DirectionalSprite.CurrentDirection = direction;
        }
    }
}