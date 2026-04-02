using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace DragonXVI;

/// <summary>
/// An Area2D with some properties stripped, so they can be
/// activated in code instead.
/// Deactivated properties are normally set to an "off" state.
/// </summary>
[GlobalClass,Tool]
public abstract partial class StrippedArea2DCS : Area2D, IStrippedProperties
{
    static StrippedArea2DCS()
	{
		strippedPropertyList = GetStrippedProperties();
	}
	public StrippedArea2DCS()
	{
		Monitoring = false;
		Monitorable = false;
		CollisionLayer = 0;
		CollisionMask = 0;
		InputPickable = false;

		if ( Engine.IsEditorHint())
		{
			ChildEnteredTree += _OnChildEnteredTree;
		}
	}

	public override void _ValidateProperty(Dictionary property)
	{
		base._ValidateProperty(property);
		if (strippedPropertyList.Contains((string)property[PropertyDetail.Name]))
		{
			property[PropertyDetail.Usage] = (long)PropertyUsageFlags.None;
		}
	}

	public virtual void _OnChildEnteredTree(Node node)
	{
		if ( node is CollisionShape2D d)
		{
			_ShapeEnteredTree(d);
		}
	}
	/// <summary>
	/// Shortcut for when a [CollisionShape] enters the tree in the editor.
	/// I like to use this to automatically change the debug color.
	/// </summary>
	/// <param name="shape">The new [CollisionShape].</param>
	public virtual void _ShapeEnteredTree(CollisionShape2D shape) {  }

    public static List<string> GetStrippedProperties() => [
            Area2D.PropertyName.Monitoring,
            Area2D.PropertyName.Monitorable,
            CollisionObject2D.PropertyName.CollisionLayer,
            CollisionObject2D.PropertyName.CollisionMask,
            CollisionObject2D.PropertyName.InputPickable,
        ];
	private static readonly List<string> strippedPropertyList;
}
