@icon( "res://addons/xvi_utilities/Assets/Script Icons/state_machine_simple.atlastex" )
extends RefCounted
class_name StateMachineLite
## A [RefCounted] based state machine.
##
## This is basically a wrapper for "match" based state machines.
## This also emits signals whenever its state is changed


## Emitted when you set the "state" value
signal state_entered( state: int )
## Emitted before the state value is changed
signal state_left( state: int )


var state: int = 0:
	set( new ):
		
		state_left.emit( state )
		state = new
		state_entered.emit( new )
