using Godot;

namespace LabBoldRun.ObstacleSystem;

[GlobalClass]
public partial class ObstacleDef : Resource
{
    /// <summary>
    /// Higher difficulty scenes are spawned at later scores.
    /// </summary>
    [Export]
    public int Difficulty = 0;
    /// <summary>
    /// This loads the scene when the def is loaded, but game should be small enough for
    /// this to not be an issue
    /// </summary>
    [Export]
    public PackedScene Scene;
}