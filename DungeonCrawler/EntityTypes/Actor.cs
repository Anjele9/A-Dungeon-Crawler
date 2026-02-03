
namespace DungeonCrawler.EntityTypes;

public abstract class Actor : Entity
{
    public float Speed { get; protected set; }
    public int HP { get; protected set; }
    public override void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, Position);
    }
    public void Move(Vector2 movement)
    {
        this.Position += movement;
    }
    public void ChangeDirection(Direction direction)
    {
        if (Sprite is DirectionalAnimatedSprite DirectionalSprite)
        {
            DirectionalSprite.CurrentDirection = direction;
        }
    }
}