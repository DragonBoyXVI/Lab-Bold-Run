using DragonXVI;
using Godot;

namespace LabBoldRun.Autoloads;

public partial class GlobalVars : Node, IAutoload<GlobalVars>
{
    public static GlobalVars GetIntsance() => instance;
    private static GlobalVars instance;

    public override void _Ready()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            throw new System.Exception("Why are you making an instance of this autoload?????");
        }
    }

    /// <summary>
    /// The speed the game is traveling at.
    /// </summary>
    public float WorldSpeed = 1.0f;
    /// <summary>
    /// World gravity.
    /// </summary>
    public float Gravity = 960.0f;


    /// <summary>
    /// Current score.
    /// </summary>
    public long Score = 0;
    /// <summary>
    /// Distance ran.
    /// </summary>
    public double Distance = 0.0;

    
}