using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace DragonXVI;

/// <summary>
/// A character body with some properties disabled so they can be enabled via code instead.
/// Some values are simply left to their default state, while others are turned off (eg, MotionMode is grounded, and CollisonLayer/Mask is 0).
/// </summary>
[GlobalClass,Tool]
public abstract partial class StrippedCharacterBody2DCS : CharacterBody2D, IStrippedProperties
{
	static StrippedCharacterBody2DCS()
	{
		strippedPropertyList = GetStrippedProperties();
	}
	public StrippedCharacterBody2DCS()
	{
		CollisionLayer = 0;
		CollisionMask = 0;
	}

	public override void _ValidateProperty(Dictionary property)
	{
		base._ValidateProperty(property);

		if (strippedPropertyList.Contains( (string)property[PropertyDetail.Name] ))
		{
			property[PropertyDetail.Usage] = (long)PropertyUsageFlags.None;
		}
	}

    public static List<string> GetStrippedProperties() => [
		CharacterBody2D.PropertyName.MotionMode,
		CollisionObject2D.PropertyName.CollisionLayer,
		CollisionObject2D.PropertyName.CollisionMask,
	];
	private static readonly List<string> strippedPropertyList;
}
