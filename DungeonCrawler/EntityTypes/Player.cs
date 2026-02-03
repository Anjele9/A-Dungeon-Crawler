
namespace DungeonCrawler.EntityTypes;

public class Player : Actor
{
    public Player(Sprite Sprite, Circle Bounds, Vector2 Position, float Speed)
    {
        this.Sprite = Sprite;
        this.Bounds = Bounds;
        this.Position = Position;
        this.Speed = Speed;
    }
    public void MoveInput(KeyboardState keyboard, bool Run)
    {
        float movespeed = Run ? 2f * Speed : Speed;
        float dx = 0f;
        float dy = 0f;
        if (keyboard.IsKeyDown(Keys.A)) dx -= 1;
        if (keyboard.IsKeyDown(Keys.D)) dx += 1;
        if (keyboard.IsKeyDown(Keys.S)) dy += 1;
        if (keyboard.IsKeyDown(Keys.W)) dy -= 1;
        if (dx != 0 || dy != 0)
        {
            Vector2 movement = new Vector2(dx, dy);
            movement.Normalize();
            Move(movement * movespeed);
        }
        if (dx == 0 && dy == 0) ChangeDirection(Direction.Idle);
        else if (dx > 0) ChangeDirection(Direction.Right);
        else if (dx < 0) ChangeDirection(Direction.Left);
        else if (dy > 0) ChangeDirection(Direction.Down);
        else if (dy < 0) ChangeDirection(Direction.Up);
    }

}
