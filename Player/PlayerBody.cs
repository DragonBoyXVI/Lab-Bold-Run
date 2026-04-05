using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;
using LabBoldRun.Collision;
using LabBoldRun.Player.States;

namespace LabBoldRun.Player;

/// <summary>
/// Main character body of the player.
/// </summary>
[GlobalClass, Tool]
public partial class PlayerBody : StrippedCharacterBody2DCS
{
    public const float JumpStrength = 600f;
    public const float BoostStrength = 800f;

    [Export]
    public Hitbox2D Hitbox;
    [Export]
    public CSStateMachine StateMachine;

    public bool IsWinning = true;

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

        Hitbox.TookDamage += OnHitboxTookDamage;
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        var gv = GlobalVars.GetIntsance();
        if (IsWinning)
        {
            gv.WorldSpeed += 0.125f * dt;
        }
        else
        {
            gv.WorldSpeed = float.Max(0f, gv.WorldSpeed - (5f * dt));
            if (Mathf.IsZeroApprox(gv.WorldSpeed))
            {
                GetTree().Quit();
                GD.Print($"Game Over! Score: {gv.Score}");
            }
        }
        gv.Score += gv.WorldSpeed * dt;

        MoveAndSlide();
    }

    private void OnHitboxTookDamage()
    {
        GD.Print("OUCH!");
        StateMachine.ChangeState(PlayerDead.StateName);
    }
}
