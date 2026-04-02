@abstract
@tool
extends Node2D;
class_name Drawing2D;
## Base class for drawing shapes
##
## Base class for nodes that draw some simple shapes to the screen.
## Provides some data related to how those are drawn.


const HANDLE_COLOR := Color.ORANGE;


## Flags for what to draw.
enum DrawFlag {
	## Draw outline
	OUTLINE = 1<<0,
	## Draw filled center
	CENTER = 1<<1,
	## Enables antialiasing
	ANIALIASING = 1<<2,
}


## How many pixels thick the outline is
@export var outline_thickness: float = 3.0:
	set( new ):
		
		outline_thickness = new;
		queue_redraw();
## color of the outline
@export var outline_color: Color = Color.BLACK:
	set( new ):
		
		outline_color = new
		queue_redraw()
## Color of the shape center
@export var center_color: Color = Color.WHITE:
	set( new ):
		
		center_color = new
		queue_redraw()
## Flags for what to draw.
var draw_flags: DrawFlag = DrawFlag.OUTLINE | DrawFlag.CENTER:
	set( new ):
		
		draw_flags = new
		queue_redraw()


func _get_property_list() -> Array[Dictionary]:
	var properties: Array[ Dictionary ] = [];
	
	properties.append( {
		Property.NAME: "draw_flags",
		Property.TYPE: TYPE_INT,
		Property.HINT: PROPERTY_HINT_FLAGS,
		Property.HINT_STRING: "Draw Outline,Draw Center,Antialiasing",
	} );
	
	return properties;
