using System;
using System.Linq;
using DragonXVI;
using Godot;

namespace LabBoldRun.ObstacleSystem;

[GlobalClass]
public partial class ObstacleSpawner : Node2D
{
    [Export]
    public ObstacleDef[] AvaliableDefs = [];

    private Timer SpawnTimer;

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        SpawnTimer = new Timer()
        {
            Autostart = true,
            OneShot = false,
            WaitTime = 1.25,
            ProcessCallback = Timer.TimerProcessCallback.Physics,
        };
        SpawnTimer.Timeout += OnSpawnTimerTimeout;
        AddChild(SpawnTimer);
    }

    private void OnSpawnTimerTimeout()
    {
        var ObDef = AvaliableDefs[ (int)GD.Randi() % AvaliableDefs.Length ];
        var Scene = ObDef.Scene.Instantiate<Node2D>();
        AddSibling(Scene);
    }
}