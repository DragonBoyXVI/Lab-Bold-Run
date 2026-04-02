@abstract
extends Object;
class_name XVIControlAnimation;
## Global class that animates control nodes.
##
## ditto

const _MTWEEN := &"CurrentTween"


## When enabled, certain animations will automatically set the conrol nodes
## pivot point to the center. Consider disabling this if it fucks up your UI.
static var auto_center_pivot: bool = true;


static func _kill_tween( control: Control ) -> void:
	if ( control.has_meta( _MTWEEN ) ):
		var tween: Tween = control.get_meta( _MTWEEN );
		if ( tween and tween.is_running() ):
			tween.kill();


## Plays an animation like that of a closing computer window.
## Hides the node once the animation is finished.
static func close_window( control: Control, duration: float = 0.125 ) -> Tween:
	_kill_tween( control );
	
	if ( auto_center_pivot ):
		control.pivot_offset_ratio = Vector2( 0.5, 0.5 );
	
	var tween := control.create_tween();
	control.set_meta( _MTWEEN, tween );
	tween.set_ignore_time_scale();
	tween.set_pause_mode( Tween.TWEEN_PAUSE_PROCESS );
	tween.set_parallel();
	
	tween.tween_property( control, ^"modulate", Color.TRANSPARENT, duration );
	tween.tween_property( control, ^"scale", Vector2( 0.8, 0.8 ), duration );
	tween.tween_callback( control.hide ).set_delay( duration );
	
	return tween;

## Plays an animation like that of opening a computer window.[br]
## The "stage_node" argument will set the node to the expected state of a closed window (transparent, reduced scale). 
static func open_window( control: Control, stage_node: bool = true, duration: float = 0.125 ) -> Tween:
	_kill_tween( control );
	
	if ( stage_node ):
		
		control.scale = Vector2( 0.8, 0.8 );
		control.modulate = Color.TRANSPARENT;
	
	if ( auto_center_pivot ):
		control.pivot_offset_ratio = Vector2( 0.5, 0.5 );
	
	var tween := control.create_tween();
	control.set_meta( _MTWEEN, tween );
	tween.set_ignore_time_scale();
	tween.set_pause_mode( Tween.TWEEN_PAUSE_PROCESS );
	tween.set_parallel();
	
	tween.tween_property( control, ^"modulate", Color.WHITE, duration );
	tween.tween_property( control, ^"scale", Vector2.ONE, duration );
	tween.tween_callback( control.show );
	
	return null;
