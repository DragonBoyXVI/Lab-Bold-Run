using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;
using LabBoldRun.Collision;

namespace LabBoldRun.Player;

/// <summary>
/// Main character body of the player.
/// </summary>
[GlobalClass, Tool]
public partial class PlayerBody : StrippedCharacterBody2DCS
{
    public const float JumpStrength = 600f;
    public const float BoostStrength = 800f;

    private static GlobalVars globalVars;

    [Export]
    public Hitbox2D Hitbox;
    [Export]
    public Hurtbox2D AttackBox;
    [Export]
    public CSStateMachine StateMachine;


    public override void _Ready()
    {
        CollisionLayer = CollisionLayers.PlayerBody;
        CollisionMask = CollisionLayers.BodyWalls;
        AddToGroup( GroupNames.PlayerNode );

        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        globalVars ??= GlobalVars.GetIntsance();

        Hitbox.TookDamage += OnHitboxTookDamage;
        AttackBox.DisableDeferred();
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        //var dt = (float)delta;

        MoveAndSlide();
    }

    private void OnHitboxTookDamage()
    {
        GD.Print("OUCH!");
        StateMachine.ChangeState(States.PlayerDead.StateName);
        Hitbox.DisableDeferred();
    }
}
