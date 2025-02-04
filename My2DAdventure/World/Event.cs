using System.Drawing;

namespace My2DAdventure.World;

public class Event(string? notification, Rectangle worldPosition, int power, bool damageEvent, bool triggered)
{
    public string? Notification { get; init; } = notification;
    public Rectangle WorldPosition { get; init; } = worldPosition;
    public int Power { get; init; } = power;
    public bool DamageEvent { get; init; } = damageEvent;
    public bool Triggered { get; init; } = triggered;
}