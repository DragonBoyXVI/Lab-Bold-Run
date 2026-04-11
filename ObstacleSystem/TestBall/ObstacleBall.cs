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
    private const int Difficulty = 1;

    private static readonly Vector2 Speed = new( 100f, 0f );

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

        Hitbox.TookDamage += () => 
        {
            QueueFree();
            GlobalVars.GetIntsance().Score++;
        };

        LBRRadio.GetIntsance().Connect(LBRRadio.SignalName.GameEnded, Callable.From(OnGameEnded));
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        Position -= Speed * dt * GlobalVars.GetIntsance().WorldSpeed;
        if (Position.X < 0f)
        {
            QueueFree();
        }
    }
    public override void _EnterTree()
    {
        base._EnterTree();

        ObstacleSpawner.ObstacleDifficultyScore += Difficulty;
    }
    public override void _ExitTree()
    {
        base._ExitTree();

        ObstacleSpawner.ObstacleDifficultyScore -= Difficulty;
    }
  
    private void OnGameEnded()
    {
        var tween = CreateTween();
        tween.TweenProperty(this, new NodePath("modulate"), Colors.Transparent, 1);
        tween.TweenCallback(Callable.From(QueueFree));
    }
}