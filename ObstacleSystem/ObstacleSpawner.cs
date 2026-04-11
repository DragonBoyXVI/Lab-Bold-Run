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
            WaitTime = 0.5,
            ProcessCallback = Timer.TimerProcessCallback.Physics,
        };
        SpawnTimer.Timeout += OnSpawnTimerTimeout;
        AddChild(SpawnTimer, false, InternalMode.Front);

        LBRRadio.GetIntsance().GameStarted += OnRadioGameStarted;
        LBRRadio.GetIntsance().GameEnded += OnRadioGameEnded;
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
        var ObScene = ObstacleScenes[ i ];
        var Scene = ObScene.Instantiate<Node2D>();
        AddSibling(Scene);
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