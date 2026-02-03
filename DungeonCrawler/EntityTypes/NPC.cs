
namespace DungeonCrawler.EntityTypes;

public class NPC : Actor{
    public NPC(Sprite Sprite, Circle Bounds, Vector2 Position, float Speed)
    {
        this.Sprite = Sprite;
        this.Bounds = Bounds;
        this.Position = Position;
        this.Speed = Speed;
        IsCollidable = true;
    }
}