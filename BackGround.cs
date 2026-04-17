using System.Collections.Generic;
using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun;

public partial class BackGround : Parallax2D
{
    private static GlobalVars globalVars;

    private readonly List<Node2D> BGList = [];
    private int CurrentBG = -1;

    public override void _Ready()
    {
        base._Ready();
        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }

        globalVars ??= GlobalVars.GetIntsance();

        foreach(Node child in GetChildren())
        {
            if (child is Node2D bg)
            {
                AddBG(bg);
            }
        }

        SetBG(0);

        LBRRadio.GetIntsance().Connect(LBRRadio.SignalName.GameStarted, Callable.From(OnGameStarted));
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        var dt = (float)delta;

        ScreenOffset += Vector2.Right * GlobalVars.XSpeed * dt * globalVars.WorldSpeed;

        const long ScorePerBG = 5;
        if (globalVars.Score / ScorePerBG > CurrentBG && CurrentBG < BGList.Count - 1)
        {
            SetBG(CurrentBG + 1);
        }
    }

    private void AddBG(Node2D bg)
    {
        BGList.Add(bg);
        bg.Modulate = Colors.Transparent;
        bg.Hide();
    }

    private static readonly NodePath NPModulate = (string)CanvasItem.PropertyName.Modulate;
    private void SetBG(int index)
    {
        if (index == CurrentBG) return;

        const double tweenTime = 0.5;

        var newBG = BGList[index];
        newBG.Show();
        var newTween = CreateTween();
        newTween.TweenProperty(newBG, NPModulate, Colors.White, tweenTime);

        if (CurrentBG >= 0)
        {
            var curBG = BGList[CurrentBG];
            var curTween = CreateTween();
            curTween.TweenProperty(curBG, NPModulate, Colors.Transparent, tweenTime);
            curTween.TweenCallback(Callable.From(curBG.Show));
        }
        CurrentBG = index;
    }

    private void OnGameStarted()
    {
        SetBG(0);
    }
}
