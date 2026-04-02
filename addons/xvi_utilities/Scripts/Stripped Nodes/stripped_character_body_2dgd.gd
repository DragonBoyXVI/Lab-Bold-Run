@abstract
@tool
extends CharacterBody2D;
class_name StrippedCharacterBody2DGD;
## A [CharacterBody2D] with some properties disabled, so that they can be enabled
## in code instead.
##
## NOTE: Some disabled properties are set to whatever is its OFF state,
## so you have to explicitly turn them back on. Others are left as their
## initial state (eg motion_mode is GROUNDED).
## NOTE: Call super() in _init()!!!;


func _init() -> void:
	
	collision_layer = 0;
	collision_mask = 0;

func _validate_property( property: Dictionary ) -> void:
	const DISABLED: PackedStringArray = [
		"motion_mode",
		"collision_layer",
		"collision_mask",
		"z_index",
	];
	
	if ( DISABLED.has( property[ Property.NAME ] ) ):
		property[ Property.USAGE ] = PROPERTY_USAGE_NONE;
