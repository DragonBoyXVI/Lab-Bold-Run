using Godot;

namespace LabBoldRun.Player;

[GlobalClass]
public partial class PlayerBody : CharacterBody2D
{
    private const float UppiesSpeed = 300f;
    private const float Gravity = 600f;
    private readonly StringName UpInput = "ui_up";

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;


        var newVelocty = Velocity;
        if (Input.IsActionPressed(UpInput))
        {
            newVelocty.Y -= UppiesSpeed * dt;
            Velocity = newVelocty;
        }
        else
        {
            if (IsOnFloor())
            {
                Velocity = Vector2.Zero;
            }
            else
            {
                newVelocty.Y += Gravity * dt;
                Velocity = newVelocty;
            }
        }
        MoveAndSlide();
    }
}
