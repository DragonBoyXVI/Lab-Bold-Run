using System;
using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun.Player;

/// <summary>
/// Object that increments speed and score, independant of the players state.
/// </summary>
[GlobalClass, Tool]
public partial class Pedometer : Node
{
    private static GlobalVars globalVars;

    const float SpeedGain = 0.125f;
    const float SpeedLoss = 2f;

    private enum State
    {
        Stopped,
        Running,
        Stopping,
    }

    private State CurState = State.Stopped;

    private Timer ScoreTimer;

    public void Start() => CurState = State.Running;
    public void Stop() => CurState = State.Stopping;

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        globalVars ??= GlobalVars.GetIntsance();
        //temp
        Start();

        ScoreTimer = new Timer()
        {
            ProcessCallback = Timer.TimerProcessCallback.Physics,
            WaitTime = 1f,
            Autostart = true,
            OneShot = false,
        };
        ScoreTimer.Timeout += () => globalVars.Score++;
        AddChild(ScoreTimer);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var dt = (float)delta;

        switch (CurState)
        {
            case State.Stopped: break;

            case State.Running:
                globalVars.WorldSpeed += SpeedGain * dt;
                break;
            
            case State.Stopping:
                globalVars.WorldSpeed = float.Max( 0f, globalVars.WorldSpeed - (SpeedLoss * dt) );
                break;

            default: throw new Exception($"Invalid pedometer state! {CurState}");
        }
    }
}