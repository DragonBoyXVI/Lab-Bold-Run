@icon( "res://addons/xvi_utilities/Assets/Script Icons/state_machine_node.atlastex" )
@tool
extends Node
class_name GDStateMachine
## A GDScript based state machine
##
## Root of a node based state machine, can have many [GDState] children
## that it manages.


## Emitted when a state is entered
signal state_entered( state: GDState )
## Emitted when a state is left
signal state_left( state: GDState )


## What state this starts on when readied
@export var initial_state: GDState:
	set( new ):
		initial_state = new;
		update_configuration_warnings();


var current_state: GDState
## Keep child states here
var _state_cache: Dictionary[ StringName, GDState ] = {}


func _ready() -> void:
	
	if ( Engine.is_editor_hint() ):
		XVIFuncs.disable_node_processes( self );
		return;
	
	var children := get_children()
	for child: Node in children:
		if ( child is GDState ):
			register_state( child )
	
	if ( initial_state ):
		change_state( initial_state.name )

func _get_configuration_warnings() -> PackedStringArray:
	var warnings := PackedStringArray();
	
	if ( not initial_state ):
		warnings.append( "No initial state set! Without one, this machine will not work unless set via some other means." );
	
	return warnings;


## Used to ready a state for usage in the ready func
func register_state( state: GDState ) -> void:
	
	if ( _state_cache.has( state.name ) ):
		push_error( "Attempting to add dupe state: ", state.name )
		return
	
	_state_cache[ state.name ] = state
	state._disable()
	state.state_change_requested.connect( _on_state_change_requested )

## Changes the current state
func change_state( state_name: StringName ) -> void:
	
	if ( not _state_cache.has( state_name ) ):
		push_error( "Trying to enter invalid state: ", state_name )
		return
	
	var new_state: GDState = _state_cache[ state_name ]
	
	if ( current_state ):
		if ( not current_state._can_switch_state( new_state ) ):
			return
		
		current_state._leave_state()
		current_state._disable()
		state_left.emit( current_state )
	
	current_state = new_state
	current_state._enable()
	current_state._enter_state()
	state_entered.emit( current_state )


func _on_state_change_requested( state: StringName ) -> void:
	change_state( state )
