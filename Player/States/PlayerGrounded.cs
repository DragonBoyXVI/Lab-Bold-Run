using Godot;

namespace LabBoldRun.Player;

[GlobalClass, Tool]
public partial class PlayerGrounded : PlayerState
{
    public static readonly StringName StateName = "PlayerGrounded";

    public override void _EnterState()
    {
        base._EnterState();
        Player.Velocity = Vector2.Down;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        
        if (@event.IsActionPressed(InputNames.Jump))
        {
            Player.Velocity = Vector2.Up * PlayerBody.JumpStrength;
            EmitRequestStateChange(PlayerJump.StateName);

            GetWindow().SetInputAsHandled();
            return;
        }
    }
}