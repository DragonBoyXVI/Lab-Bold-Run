using Godot;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public partial class PlayerGroundAttack : PlayerAttack
{
    public static readonly StringName StateName = "PlayerGroundAttack";

    protected override void OnAttackTimerTimeout()
    {
        base.OnAttackTimerTimeout();
        EmitRequestStateChange(PlayerGrounded.StateName);
    }
}