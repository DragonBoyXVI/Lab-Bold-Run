using DragonXVI;
using Godot;

namespace LabBoldRun.Autoloads;

public partial class PauseManager : Node, IAutoload<PauseManager>
{
    private static PauseManager instance;
    public static PauseManager GetIntsance() => instance;

    [Signal]
    public delegate void GamePausedEventHandler(bool isPaused);
    public static void EmitGamePaused(bool isPaused) => instance.EmitSignal(SignalName.GamePaused, isPaused);

    public void SwitchPause()
    {
        var tree = GetTree();
        tree.Paused = !tree.Paused;
        EmitGamePaused(tree.Paused);
    }

    public override void _Ready()
    {
        base._Ready();
        instance ??= this;
        ProcessMode = ProcessModeEnum.Always;
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsEcho()) return;


        if (@event.IsActionPressed(InputNames.Pause))
        {
           SwitchPause();
            GetWindow().SetInputAsHandled();
            return;
        }
    }
}