using System.Collections.Generic;
using DragonXVI;
using Godot;

namespace LabBoldRun.Player.States;

[GlobalClass, Tool]
public abstract partial class PlayerState : CSState
{
    [Export]
    public PlayerBody Player
    {
        get => _player;
        set
        {
            _player = value;
            UpdateConfigurationWarnings();
        }
    }
    private PlayerBody _player;

    public override string[] _GetConfigurationWarnings()
    {
        List<string> warnings = [];

        if (Player is null) warnings.Add("Assign the player stoopid \n(ill learn to do this better eventually BUT LATER)");

        return [.. warnings];
    }
    public override void _Ready()
    {
        base._Ready();
        if (Engine.IsEditorHint())
        {
            XVIUtil.DisableNodeProcesses(this);
            return;
        }
    }
}