using System.Collections.Generic;
using DragonXVI;
using Godot;
using LabBoldRun.Autoloads;

namespace LabBoldRun;

public partial class BackGround : Parallax2D
{
    private static GlobalVars globalVars;

    private readonly List<Node2D> BGList = [];

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

        BGList[0].Modulate = Colors.White;
    }

    private void AddBG(Node2D bg)
    {
        BGList.Add(bg);
        bg.Modulate = Colors.Transparent;
    }

    private void SetBG(int index)
    {
        
    }
}
