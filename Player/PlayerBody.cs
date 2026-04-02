using DragonXVI;
using Godot;

namespace LabBoldRun.Player;

/// <summary>
/// Main character body of the player.
/// </summary>
[GlobalClass, Tool]
public partial class PlayerBody : StrippedCharacterBody2DCS
{
    public const float JumpStrength = 300f;    

    public override void _Ready()
    {
        CollisionLayer = Collision.PlayerBody;
        CollisionMask = Collision.BodyWalls;

        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        //var dt = (float)delta;

        MoveAndSlide();
    }
}
