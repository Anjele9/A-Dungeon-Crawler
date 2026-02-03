using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary;

public struct Circle : IEquatable<Circle>
{
    public int X;
    public int Y;

    public int Radius { get; set; }
    public readonly Point Location => new Point(X, Y);

    public readonly int Top => Y + Radius;
    public readonly int Bottom => Y - Radius;
    public readonly int Left => X - Radius;
    public readonly int Right => X + Radius;

    public Circle(int x, int y, int radius)
    {
        X = x;
        Y = y;
        Radius = radius;
    }
    public Circle(Point location, int radius)
    {
        X = location.X;
        Y = location.Y;
        Radius = radius;
    }

    public bool Intersects(Circle other)
    {
        int radiusSquared = (this.Radius + other.Radius) * (this.Radius + other.Radius);
        float distanceSquared = Vector2.DistanceSquared(this.Location.ToVector2(), other.Location.ToVector2());
        return distanceSquared < radiusSquared;
    }
    public readonly bool Equals(Circle other) => (other.X == this.X && other.Y == this.Y && other.Radius == this.Radius);
    public override readonly bool Equals(object other) => other is Circle && Equals(other);
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Radius);
    public static bool operator ==(Circle left, Circle right) => left.Equals(right);
    public static bool operator !=(Circle left, Circle right) => !left.Equals(right);
}