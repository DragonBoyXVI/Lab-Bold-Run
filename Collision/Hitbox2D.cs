using DragonXVI;
using Godot;

namespace LabBoldRun.Collision;

[GlobalClass, Tool]
public partial class Hitbox2D : StrippedArea2DCS
{
    [Signal]
    public delegate void TookDamageEventHandler();
    public void EmitTookDamage() => EmitSignal(SignalName.TookDamage);

    [Export]
    public bool IsPlayers = false;

    public override void _Ready()
    {
        base._Ready();

        CollisionLayer = CollisionLayers.DamageLayer;
        Monitorable = true;
    }
}