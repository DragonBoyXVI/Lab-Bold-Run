using System.Collections.Generic;
using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun;

public partial class BackGround : Parallax2D
{
    private static GlobalVars globalVars;

    private const int ScorePerBackground = 5;

    private readonly List<TileMapLayer> TileMaps = [];
    private int BackgroundIndex = 0;
    private int NextScoreReq = 0;

    public override void _Ready()
    {
        base._Ready();

        globalVars ??= GlobalVars.GetIntsance();

        foreach (Node child in GetChildren())
        {
            if (child is TileMapLayer tileMap)
            {
                TileMaps.Add(tileMap);
                tileMap.Hide();
            }
        }

        TileMaps[0].Show();
        NextScoreReq = ScorePerBackground;

        SetProcess(false);

        LBRRadio.GetIntsance().Connect(LBRRadio.SignalName.GameStarted, Callable.From(OnGameStart));
        LBRRadio.GetIntsance().Connect(LBRRadio.SignalName.GameEnded, Callable.From(OnGameEnd));
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        var dt = (float)delta;

        var offset = ScreenOffset;
        offset.X += GlobalVars.XSpeed * globalVars.WorldSpeed * dt;
        ScreenOffset = offset;

        if (globalVars.Score > NextScoreReq)
        {
            if (BackgroundIndex < TileMaps.Count - 1)
            {
                TileMaps[BackgroundIndex].Hide();
                BackgroundIndex++;
                TileMaps[BackgroundIndex].Show();
                NextScoreReq = ScorePerBackground * BackgroundIndex;
            }
        }
    }

    private void OnGameStart()
    {
        SetProcess(true);
    }
    private void OnGameEnd()
    {
        SetProcess(false);
    }
}
