using Godot;
using LabBoldRun.Collision;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public abstract partial class PlayerAttack : PlayerState
{
    [Export]
    private float AttackDuration = 1f;
    [Export]
    private Hurtbox2D AttackBox;
    [Export]
    private Hitbox2D Hitbox;
    [Export]
    private Node2D Model;

    private Timer AttackTimer;

    public override void _Ready()
    {
        base._Ready();
        if (Engine.IsEditorHint())
        {
            return;
        }

        AttackTimer = new()
        {
            WaitTime = AttackDuration,
            OneShot = true,
            Autostart = false,
            ProcessCallback = Timer.TimerProcessCallback.Physics,
        };
        AddChild(AttackTimer);
        AttackTimer.Timeout += OnAttackTimerTimeout;
    }

    private static readonly NodePath TweenAnimPath = "rotation";
    private Tween modelTween;
    public override void _EnterState()
    {
        base._EnterState();
        AttackBox.EnableDeferred();
        Hitbox.DisableDeferred();

        modelTween = CreateTween();
        modelTween.TweenProperty(Model, TweenAnimPath, double.Tau, AttackDuration);

        AttackTimer.Start();
    }
    public override void _LeaveState()
    {
        base._LeaveState();
        AttackBox.DisableDeferred();
        Hitbox.EnableDeferred();

        modelTween.Kill();
        modelTween = null;
        Model.Rotation = 0f;
    }

    protected virtual void OnAttackTimerTimeout()
    {
        
    }
}