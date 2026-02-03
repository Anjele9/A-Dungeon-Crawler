using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Graphics;
public enum Direction
{
    Up, Down, Left, Right, Idle
}

public class DirectionalAnimatedSprite : AnimatedSprite
{
    private Dictionary<Direction, Animation> _directionAnimations = new Dictionary<Direction, Animation>();
    private Direction _currentDirection;
    public Direction CurrentDirection //Immediately changes the animation when a new direction is given if it's not the current direction
    {
        get => _currentDirection;
        set
        {
            if (_currentDirection != value && _directionAnimations.ContainsKey(value))
            {
                _currentDirection = value;
                Animation = _directionAnimations[value];
            }
        }   
    }
    public void SetDirectionAnimation(Direction direction, Animation animation) //Change the animation for the given direction, if its the first one then set it to current
    {
        _directionAnimations[direction] = animation;
        if (_directionAnimations.Count == 1){
        _currentDirection = direction;
        Animation = animation;
        }
    }
}


public class DirectionAnimationConfig
{
    public string Direction {get; set; }
    public string AnimationName {get; set;}
}

public class DirectionalAnimationConfig
{
    public string Name {get; set;} = "";
    public string AnimationName {get; set;} = "";
}

public class DirectionalSpriteConfig
{
    public string Name { get; set;} = "";
    public float ScaleX {get; set;} = 1f;
    public float ScaleY {get; set;} = 1f;
    public List<DirectionAnimationConfig> Animations {get; set;} = new();
}

public static class DirectionalSpriteConfigLoader
{
    public static DirectionalSpriteConfig LoadFromXml(string path, string spriteName)
    {
        //Throw exception if sprite name or path isn't found
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (spriteName == null) throw new ArgumentNullException(nameof(spriteName));
        if (!File.Exists(path)) throw new FileNotFoundException($"Directional sprite config file not found: {path}", path);


        var doc = XDocument.Load(path);
        var root = doc.Root ?? throw new InvalidDataException("TextureAtlas XML has no root element.");

        var directionalSpritesElement = root.Element("DirectionalSprites");
        if (directionalSpritesElement == null)
            throw new InvalidDataException("TextureAtlas XML is missing <DirectionalSprites> section.");

        // Find <DirectionalSprite name="spriteName">
        var spriteElement = directionalSpritesElement
            .Elements("DirectionalSprite")
            .FirstOrDefault(e => (string?)e.Attribute("name") == spriteName);

        if (spriteElement == null)
            throw new InvalidDataException(
                $"DirectionalSprite with name '{spriteName}' not found in {path}.");

        var config = new DirectionalSpriteConfig
        {
            Name = spriteName,
            ScaleX = 1f,
            ScaleY = 1f
        };

        // Optional <Scale x="2.0" y="2.0" />
        var scaleElement = spriteElement.Element("Scale");
        if (scaleElement != null)
        {
            var xAttr = (string?)scaleElement.Attribute("x");
            var yAttr = (string?)scaleElement.Attribute("y");

            if (!string.IsNullOrEmpty(xAttr))
                config.ScaleX = float.Parse(xAttr, CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(yAttr))
                config.ScaleY = float.Parse(yAttr, CultureInfo.InvariantCulture);
        }

        // Children can be either:
        //   <Animation direction="Idle" name="slime-animation" />
        // or
        //   <Direction name="Idle" animation="enemy-idle" />
        foreach (var child in spriteElement.Elements())
        {
            if (child.Name.LocalName == "Scale")
                continue; // already handled

            if (child.Name.LocalName != "Animation" && child.Name.LocalName != "Direction")
                continue; // ignore other elements if any

            // Try both styles
            var directionValue =
                (string?)child.Attribute("direction") ??
                (string?)child.Attribute("name");

            var animationNameValue =
                (string?)child.Attribute("name") ??
                (string?)child.Attribute("animation");

            if (string.IsNullOrWhiteSpace(directionValue) ||
                string.IsNullOrWhiteSpace(animationNameValue))
            {
                // Skip malformed entries
                continue;
            }

            config.Animations.Add(new DirectionAnimationConfig
            {
                Direction = directionValue,
                AnimationName = animationNameValue
            });
        }

        if (config.Animations.Count == 0)
        {
            throw new InvalidDataException(
                $"DirectionalSprite '{spriteName}' in {path} has no valid direction animations.");
        }

        return config;
    }
}
public static class DirectionalSpriteFactory
{
    public static DirectionalAnimatedSprite Create(
        DirectionalSpriteConfig config, TextureAtlas atlas)
    {
        var sprite = new DirectionalAnimatedSprite();

        foreach (var animCfg in config.Animations)
        {
            // Map string ("Idle", "Left", etc.) to your enum Direction
            var direction = Enum.Parse<Direction>(animCfg.Direction, ignoreCase: true);
            var animation = atlas.GetAnimation(animCfg.AnimationName);

            sprite.SetDirectionAnimation(direction, animation);
        }

        sprite.Scale = new Vector2(config.ScaleX, config.ScaleY);

        return sprite;
    }
}