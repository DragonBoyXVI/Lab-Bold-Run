using Godot;

namespace LabBoldRun.Collision;

[Tool]
public static class CollisionLayers
{
    static CollisionLayers()
    {
        GD.Print("Set Collision Names!");
        // set project settings names.
        const string PathTemp = "layer_names/2d_physics/layer_";
        ProjectSettings.SetSetting(PathTemp + "1", "Body Wall");
        ProjectSettings.SetSetting(PathTemp + "2", "Player Body");
        ProjectSettings.SetSetting(PathTemp + "3", "Damage Layer");
        ProjectSettings.SetSetting(PathTemp + "4", "Collectable Layer");
    }
    /// <summary>
    /// Walls that block bodies, like the player
    /// </summary>
    public static readonly uint BodyWalls = 1<<0;
    /// <summary>
    /// The players character body.
    /// Could be useful for triggers outside the player.
    /// </summary>
    public static readonly uint PlayerBody = 1<<1;
    /// <summary>
    /// Interaction layer for damage systems.
    /// </summary>
    public static readonly uint DamageLayer = 1<<2;
    /// <summary>
    /// Interaction layer for collectables.
    /// </summary>
    public static readonly uint Collectable = 1<<3;
}