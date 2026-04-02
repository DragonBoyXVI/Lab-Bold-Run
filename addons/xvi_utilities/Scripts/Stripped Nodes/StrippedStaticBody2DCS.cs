using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace DragonXVI;

/// <summary>
/// A static body with some properties disabled so they can be enabled via code instead.
/// Disabled values are either off or left as defualt. (collison layer/mask is 0).
/// </summary>
[GlobalClass,Tool]
public abstract partial class StrippedStaticBody2DCS : StaticBody2D, IStrippedProperties
{
	static StrippedStaticBody2DCS()
	{
		strippedPropertyList = GetStrippedProperties();
	}
	public StrippedStaticBody2DCS()
	{
		CollisionLayer = 0;
		CollisionMask = 0;
		InputPickable = false;
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
		CollisionObject2D.PropertyName.CollisionLayer,
		CollisionObject2D.PropertyName.CollisionMask,
		CollisionObject2D.PropertyName.InputPickable,
	];
	private static readonly List<string> strippedPropertyList;
}