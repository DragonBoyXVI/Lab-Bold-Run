using System.Diagnostics.CodeAnalysis;
using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;
using LabBoldRun.Collision;
using LabBoldRun.Player;

namespace LabBoldRun.ObstacleSystem;

[GlobalClass]
public partial class ObstacleBall : Node2D
{
    private static float Speed = 1f;

    [Export]
    private Hitbox2D Hitbox;

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        PlayerBody Player = GlobalVars.GetIntsance().PlayerNode;
        if (IsInstanceValid(Player))
            Position = Player.Position + (Vector2.Right * 1000);
        ResetPhysicsInterpolation();

        Hitbox.TookDamage += () => 
        {
            QueueFree();
            GlobalVars.GetIntsance().Score++;
        };

        var radio = LBRRadio.GetIntsance();
        radio.Connect(LBRRadio.SignalName.GameEnded, Callable.From(OnGameEnded));
        radio.Connect(LBRRadio.SignalName.GameStarted, Callable.From(OnGameStarted));
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        var newPosition = Position;
        newPosition.X -= GlobalVars.XSpeed * Speed * dt * GlobalVars.GetIntsance().WorldSpeed;
        Position = newPosition;
        if (newPosition.X < 0f)
        {
            QueueFree();
        }
    }

    private void OnGameStarted() => QueueFree();
    private void OnGameEnded()
    {
        var tween = CreateTween();
        tween.TweenProperty(this, new NodePath("modulate"), Colors.Transparent, 1);
        tween.TweenCallback(Callable.From(QueueFree));
    }
}