using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public partial class PlayerAirAttack : PlayerAttack
{
    public static readonly StringName StateName = "PlayerAirAttack";

    private static GlobalVars globalVars;

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            return;
        }

        globalVars ??= GlobalVars.GetIntsance();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        var newVelocity = Player.Velocity;
        newVelocity.Y += globalVars.Gravity * dt;
        Player.Velocity = newVelocity;
    }

    protected override void OnAttackTimerTimeout()
    {
        base.OnAttackTimerTimeout();

        EmitRequestStateChange( Player.IsOnFloor() ? PlayerGrounded.StateName : PlayerFlying.StateName );
    }
}