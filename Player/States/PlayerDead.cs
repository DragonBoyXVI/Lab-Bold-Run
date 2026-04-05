using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public partial class PlayerDead : PlayerState
{
    public static readonly StringName StateName = "PlayerDead";

    [Export]
    private Node2D Model;

    public override void _EnterState()
    {
        base._EnterState();
        Player.IsWinning = false;
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        var dt = (float)delta;

        const float rotationSpeed = float.Tau;
        Model.Rotate( rotationSpeed * dt * GlobalVars.GetIntsance().WorldSpeed );
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        var newVelocity = Player.Velocity;
        newVelocity.Y += GlobalVars.GetIntsance().Gravity * dt;
        Player.Velocity = newVelocity;
    }
}