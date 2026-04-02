@abstract
extends Object;
class_name XVIFuncs;
## Global class with some useful functions.
##
## Destription 2 waow.


## Disables all node processes that have a disable function.
## Such as process, physics process, input processes, etc.
## Useful for tool nodes, be sure to call this in _ready rather than _init.
static func disable_node_processes( node: Node ) -> void:
	
	node.set_process( false );
	node.set_physics_process( false );
	node.set_process_input( false );
	node.set_process_shortcut_input( false );
	node.set_process_unhandled_input( false );
	node.set_process_unhandled_key_input( false );