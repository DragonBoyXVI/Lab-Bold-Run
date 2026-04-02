using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.Player;

[GlobalClass, Tool]
public partial class PlayerJump : PlayerState
{
    public static readonly StringName StateName = "PlayerJump";
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        Vector2 gravity = Vector2.Down * GlobalVars.GetIntsance().Gravity * dt;
        var newVelocity = Player.Velocity + gravity;
        Player.Velocity = newVelocity;

        if (Player.IsOnFloor())
        {
            EmitRequestStateChange(PlayerGrounded.StateName);
        }
    }
}