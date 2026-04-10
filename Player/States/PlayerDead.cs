using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public partial class PlayerDead : PlayerState
{
    public static readonly StringName StateName = "PlayerDead";
    private static GlobalVars globalVars;

    [Export]
    private Node2D Model;
    [Export]
    private Pedometer ThePedometer;

    private Vector2 PreviousVelocity = Vector2.Zero;

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            return;
        }

        globalVars ??= GlobalVars.GetIntsance();
    }
    public override void _EnterState()
    {
        base._EnterState();

        ThePedometer.Stop();
        Player.Velocity = Player.Velocity + new Vector2( 0f, -200f );
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        var dt = (float)delta;

        const float rotationSpeed = float.Tau;
        Model.Rotate( rotationSpeed * dt * globalVars.WorldSpeed );
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;


        if (Player.IsOnFloor())
        {
            globalVars.WorldSpeed *= 0.8f;
            if (PreviousVelocity.LengthSquared() > 1f) 
                Player.Velocity = PreviousVelocity * new Vector2(1f, -0.8f) * float.Min(1f, globalVars.WorldSpeed);
        }
        else
        {
            var newVelocity = Player.Velocity;
            newVelocity.Y += globalVars.Gravity * dt;
            Player.Velocity = newVelocity;
        }
        PreviousVelocity = Player.Velocity;

        if ( Player.Velocity.IsZeroApprox() )
        {
            LBRRadio.EmitGameEnded();
        }
    }
}