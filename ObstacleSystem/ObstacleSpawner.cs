using System;
using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.ObstacleSystem;

[GlobalClass]
public partial class ObstacleSpawner : Node2D
{
    public static long ObstacleDifficultyScore = 0;
    private static GlobalVars globalVars;

    [Export]
    public PackedScene[] ObstacleScenes = [];

    private Timer SpawnTimer;

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        globalVars ??= GlobalVars.GetIntsance();

        SpawnTimer = new Timer()
        {
            Autostart = false,
            OneShot = false,
            WaitTime = .5f,//1.25f,
            ProcessCallback = Timer.TimerProcessCallback.Physics,
        };
        SpawnTimer.Connect(Timer.SignalName.Timeout, Callable.From(OnSpawnTimerTimeout), (uint)ConnectFlags.Deferred);
        AddChild(SpawnTimer, false, InternalMode.Front);

        var radio = LBRRadio.GetIntsance();
        radio.Connect(LBRRadio.SignalName.GameStarted, Callable.From(OnRadioGameStarted), (uint)ConnectFlags.Deferred);
        radio.Connect(LBRRadio.SignalName.GameEnded, Callable.From(OnRadioGameEnded));
        radio.Connect(LBRRadio.SignalName.ObstacleAdded, new Callable(this, nameof(OnRadioObstacleAdded)));
        radio.Connect(LBRRadio.SignalName.ObstacleRemoved, new Callable(this, nameof(OnRadioObstacleRemoved)));
    }

    private void OnSpawnTimerTimeout()
    {
        //var List = new List<ObstacleDef>( AvaliableDefs );
        //List.Re

        if (ObstacleDifficultyScore > (globalVars.Score / 25))
        {
            return;
        }

        var i = (int)(GD.Randi() % ObstacleScenes.Length);
        GD.Print(i);
        var ObScene = ObstacleScenes[ i ];
        var Scene = ObScene.Instantiate<Node2D>();
        AddSibling(Scene);
    }
    private static void OnRadioObstacleAdded(long diffScore)
    {
        ObstacleDifficultyScore += diffScore;
    }

    private static void OnRadioObstacleRemoved(long diffScore)
    {
        ObstacleDifficultyScore = long.Max(0, ObstacleDifficultyScore - diffScore);
    }

    private void OnRadioGameStarted()
    {
        ObstacleDifficultyScore = 0;
        SpawnTimer.Start();
    }
    private void OnRadioGameEnded()
    {
        SpawnTimer.Stop();
    }
}