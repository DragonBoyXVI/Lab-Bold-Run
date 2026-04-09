using DragonXVI;
using Godot;

namespace LabBoldRun.Collision;

[GlobalClass, Tool]
public partial class Hurtbox2D : StrippedArea2DCS
{
    [Export]
    public bool IsPlayers = false;

    public override void _Ready()
    {
        base._Ready();

        CollisionMask = CollisionLayers.DamageLayer;
        Monitoring = true;

        AreaEntered += OnAreaEntered;
    }

    public override void _ShapeEnteredTree(CollisionShape2D shape)
    {
        shape.DebugColor = new Color(Colors.Red, 0.65f);
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is Hitbox2D hitbox)
        {
            if ( hitbox.IsPlayers != IsPlayers )
            {
                hitbox.EmitTookDamage();
            }
        }
    }
}