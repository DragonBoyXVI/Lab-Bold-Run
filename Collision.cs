using Godot;

namespace LabBoldRun;

public class Collision
{
    static Collision()
    {
        // set project settings names.
        const string PathTemp = "layer_names/2d_physics/layer_";
        ProjectSettings.SetSetting(PathTemp + 1, "Body Wall");
        ProjectSettings.SetSetting(PathTemp + 2, "Player Body");
        ProjectSettings.SetSetting(PathTemp + 3, "Damage Area");
        ProjectSettings.SetSetting(PathTemp + 4, "Collectable Area");
    }
    public const uint BodyWalls = 1<<0;
    public const uint PlayerBody = 1<<1;
    public const uint DamagableArea = 1<<2;
    public const uint CollectableArea = 1<<3;
}