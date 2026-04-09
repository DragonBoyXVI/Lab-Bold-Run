using DragonXVI;
using Godot;

namespace LabBoldRun.Autoloads;

/// <summary>
/// Global signal bus node.
/// All of these are signals rather than cs events for editor/gdscript access.
/// </summary>
public partial class LBRRadio : Node, IAutoload<LBRRadio>
{
    public static LBRRadio GetIntsance() => instance;
    private static LBRRadio instance;

    [Signal]
    public delegate void GameStartedEventHandler();
    public static void EmitGameStarted() => instance.EmitSignal(SignalName.GameStarted);
    [Signal]
    public delegate void GameEndedEventHandler();
    public static void EmitGameEnded() => instance.EmitSignal(SignalName.GameEnded);

    public override void _Ready()
    {
        base._Ready();

        instance = this;
    }
}