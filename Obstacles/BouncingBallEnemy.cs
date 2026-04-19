using Godot;
using DragonXVI;
using LabBoldRun.Collision;
using LabBoldRun.Autoloads;
using LabBoldRun.ObstacleSystem;

namespace LabBoldRun.Obstacles;

[GlobalClass, Tool]
public partial class BouncingBallEnemy : StrippedCharacterBody2DCS
{
    enum State
    {
        
    }

    private static readonly GlobalVars globalVars = GlobalVars.GetIntsance();

    const float SpeedMultMin = 0.8f;
    const float SpeedMultMax = 3f;
    //i am very intellegenve
    private static readonly double AngleVariance = double.DegreesToRadians(25f);

    private static readonly Vector2 BallSize = new(48f * 2f, 48f * 2f);
    private static readonly Rect2 SafeArea = new(-BallSize, PlayArea.Size + (BallSize * 2f));

    [Export]
    private Hitbox2D Hitbox;
    [Export]
    private Hurtbox2D Hurtbox;

    //private float SpeedMult;


    public override void _Ready()
    {
        base._Ready();
        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        CollisionMask = CollisionLayers.BodyWalls;


        Position = ( SafeArea.Size * new Vector2(1f, 0.5f) ) - BallSize;
        Position += Vector2.Left;
        ResetPhysicsInterpolation();

        var SpeedMult = float.Clamp( globalVars.WorldSpeed,
            SpeedMultMin,
            SpeedMultMax
        );  

        var startingVelocity = Vector2.Left;
        startingVelocity = startingVelocity.Rotated( (float)GD.Randfn(0, AngleVariance) );
        startingVelocity *= SpeedMult * GlobalVars.XSpeed;
        Velocity = startingVelocity;

        var radio = LBRRadio.GetIntsance();
        radio.Connect(LBRRadio.SignalName.GameStarted, Callable.From(OnRadioGameStarted));
        radio.Connect(LBRRadio.SignalName.GameEnded, Callable.From(OnRadioGameEnded));
        Hitbox.Connect(Hitbox2D.SignalName.TookDamage, Callable.From(OnHitboxTookDamage));
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        if (!SafeArea.HasPoint(Position))
        {
            QueueFree();
            return;
        }

        var newVelocity = Velocity;
        newVelocity.Y += globalVars.Gravity * dt;
        Velocity = newVelocity;

        MoveAndSlide();
        if (IsOnFloor() || IsOnCeiling())
        {
            Velocity = newVelocity * new Vector2(1f, -0.8f);
        }
    }

    private void OnRadioGameStarted() => QueueFree();
    private static readonly NodePath ScalePath = (string)Node2D.PropertyName.Scale;
    private void OnRadioGameEnded()
    {
        var tween = CreateTween();
        var dur = GD.RandRange(0.625, 1);
        tween.TweenProperty(this, ScalePath, Vector2.Zero, dur);
        tween.TweenCallback(Callable.From(QueueFree));
    }
    private void OnHitboxTookDamage()
    {
        globalVars.Score++;
        Hitbox.DisableDeferred();
        Hurtbox.IsPlayers = true;

        var DirFromPlayer = globalVars.PlayerNode.Position.DirectionTo(Position);
        var newVelocity = Velocity * DirFromPlayer * 2f;
        newVelocity.X = float.Max(GlobalVars.XSpeed, float.Abs(newVelocity.X));

        Velocity = newVelocity;
        Modulate = Colors.BlueViolet;
    }
}
