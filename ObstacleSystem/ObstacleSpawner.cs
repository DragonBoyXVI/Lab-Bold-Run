using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.ObstacleSystem;

[GlobalClass]
public partial class ObstacleSpawner : Node2D
{
    private static GlobalVars globalVars;

    [Export]
    public ObstacleDef[] AvaliableDefs = [];

    private Timer SpawnTimer;
    private int SpawnCount = 0;

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
        AddChild(SpawnTimer);

        LBRRadio.GetIntsance().GameStarted += () => SpawnTimer.Start();
        LBRRadio.GetIntsance().GameEnded += () => SpawnTimer.Stop();
    }

    private void OnSpawnTimerTimeout()
    {
        //var List = new List<ObstacleDef>( AvaliableDefs );
        //List.Re
        GD.Print(SpawnCount);

        if (SpawnCount > (globalVars.Score / 25))
        {
            return;
        }

        SpawnCount++;
        var i = (int)(GD.Randi() % AvaliableDefs.Length);
        var ObDef = AvaliableDefs[ i ];
        var Scene = ObDef.Scene.Instantiate<Node2D>();
        Scene.TreeExited += OnSpawnedObstacleExitTree;
        AddSibling(Scene);
    }

    private void OnSpawnedObstacleExitTree() => SpawnCount--;
}