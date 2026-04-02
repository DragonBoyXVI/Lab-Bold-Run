using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public partial class PlayerJump : PlayerState
{
    public static readonly StringName StateName = "PlayerJump";

    private Timer FlyTimer;

    public override void _Ready()
    {
        base._Ready();
        if (Engine.IsEditorHint())
        {
            return;
        }

        FlyTimer = new Timer()
        {
            WaitTime = 0.5,
            OneShot = true,
            ProcessCallback = Timer.TimerProcessCallback.Physics,
        };
        FlyTimer.Timeout += OnFlyTimerTimeout;
        AddChild(FlyTimer);
    }
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
    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event.IsActionReleased(InputNames.Jump))
        {
            var newVelocity = Player.Velocity;
            newVelocity.Y = float.Max(newVelocity.Y, -50f);
            Player.Velocity = newVelocity;

            GetWindow().SetInputAsHandled();
            return;
        }
        if (@event.IsActionPressed(InputNames.Jump))
        {
            EmitRequestStateChange(PlayerFlying.StateName);

            GetWindow().SetInputAsHandled();
            return;
        }
    }

    public override void _EnterState()
    {
        base._EnterState();

        if (CanProcess())
            FlyTimer.Start();
    }

    private void OnFlyTimerTimeout()
    {
        if (Input.IsActionPressed(InputNames.Jump))
        {
            EmitRequestStateChange(PlayerFlying.StateName);
        }
    }
}