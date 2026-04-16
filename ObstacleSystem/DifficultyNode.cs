using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.ObstacleSystem;

[GlobalClass, Tool]
public partial class DifficultyNode : Node
{
    [Export]
    public long Difficulty = 0;

    public override void _EnterTree()
    {
        base._EnterTree();
        if (!Engine.IsEditorHint())
            LBRRadio.EmitObstacleAdded(Difficulty);
    }
    public override void _ExitTree()
    {
        base._ExitTree();
        if (!Engine.IsEditorHint())
            LBRRadio.EmitObstacleRemoved(Difficulty);
    }
}