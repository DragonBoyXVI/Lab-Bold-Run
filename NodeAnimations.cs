using Godot;

namespace LabBoldRun;

public static class NodeAnimations
{
    static readonly NodePath RotationPath = "rotation";

    private static void DisapearAnimation(Node2D node)
    {
        

        var tween = node.CreateTween();
        tween.TweenProperty(node, RotationPath, double.DegreesToRadians(-45.0), 0.5);
        

        tween.SetParallel();
        tween.TweenProperty(node, new NodePath(Node2D.PropertyName.Rotation), double.Tau, 1.0);
    }
}