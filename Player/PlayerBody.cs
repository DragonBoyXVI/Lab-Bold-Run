using DragonXVI;
using Godot;
using LabBoldRun.Collision;
using LabBoldRun.Autoloads;

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

        Hitbox.TookDamage += OnHitboxTookDamage;
        AttackBox.DisableDeferred();

        //yk what sure it works.
        LBRRadio.GetIntsance().Connect(LBRRadio.SignalName.GameEnded, Callable.From(QueueFree));
        //LBRRadio.GetIntsance().GameEnded += OnGameEnded;
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        //var dt = (float)delta;

        MoveAndSlide();
    }
    //public override void _Notification(int what)
    //{
    //    //Yes, in c# you do need to manually disconnect from CUSTOM signals.
    //    //unless you use Connect() bc ??????
    //    base._Notification(what);
    //    if (what == NotificationPredelete)
    //    {
    //        LBRRadio.GetIntsance().GameEnded -= OnGameEnded;
    //    }
    //}

    private void OnHitboxTookDamage()
    {
        StateMachine.ChangeState(States.PlayerDead.StateName);
        Hitbox.DisableDeferred();
    }

    private void OnGameEnded() => QueueFree();
}
