using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public partial class PlayerFlying : PlayerState
{
    public static readonly StringName StateName = "PlayerFlying";

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        var newVelocity = Player.Velocity;
        if (Input.IsActionPressed(InputNames.Jump))
        {
            newVelocity.Y -= PlayerBody.BoostStrength * dt * ((newVelocity.Y > 0f) ? 2 : 1);
        }
        else
        {
            newVelocity.Y += GlobalVars.GetIntsance().Gravity * dt * ((newVelocity.Y < 0f) ? 2 : 1);
        }
        newVelocity.Y = float.Min( 600f, float.Abs(newVelocity.Y) ) * float.Sign(newVelocity.Y);
        Player.Velocity = newVelocity;
        
        if (Player.IsOnFloor())
        {
            EmitRequestStateChange(PlayerGrounded.StateName);
        }
    }
}