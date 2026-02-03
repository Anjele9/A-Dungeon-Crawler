using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.Graphics;

public class AnimatedSprite : Sprite
{
    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation;

    public Animation Animation
    {
        get => _animation;
        set
        {
            _animation = value;
            Region = _animation.Frames[0]; // Note the region is moved between frames, the slime isnt redrawn. 
            }
    }
    public AnimatedSprite() { }
    public AnimatedSprite(Animation animation)
    {
        Animation = animation;
    }
    public override void Update(GameTime gameTime) //Everytime the animation updates, check if the timer has elapsed past the delay, move it ahead if it has
    {
        _elapsed += gameTime.ElapsedGameTime;
        if (_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;
            if (_currentFrame >= _animation.Frames.Count)
            {
                _currentFrame = 0;
            }
            Region = _animation.Frames[_currentFrame];
        }
    }
}