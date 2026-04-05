using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;
using LabBoldRun.Player;

namespace LabBoldRun.ObstacleSystem;

[GlobalClass]
public partial class ObstacleBall : Node2D
{
    private static readonly Vector2 Speed = new( 100f, 0f );

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        PlayerBody Player = (PlayerBody)GetTree().GetNodesInGroup(GroupNames.PlayerNode)[0];
        Position = Player.Position + (Vector2.Right * 1000);
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
}