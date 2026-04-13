extends Control

const SCORE_PANEL_SCENE: PackedScene = preload("res://GuiOverlays/Saved Score Viewer/score_panel.tscn");

@onready var score_panel_container: BoxContainer = %ScorePanelContainer;


func _ready() -> void:
	
	hide();
	%MainMenuButton.pressed.connect( _on_main_menu_button_pressed );
	LbrRadio.ScoreScreenRequested.connect( _on_radio_score_screen_requested );


func clear_board() -> void:
	
	for child: Node in score_panel_container.get_children():
		child.queue_free();

func populate_board() -> void:
	
	var score_array := ScoreFileHandler.get_saved_scores_as_dict();
	for score: Dictionary in score_array:
		var score_panel: ScorePanel = SCORE_PANEL_SCENE.instantiate();
		score_panel_container.add_child( score_panel );
		score_panel.set_data( score );


func _on_radio_score_screen_requested() -> void:
	
	XVIControlAnimation.open_window(self);
	populate_board();

func _on_main_menu_button_pressed() -> void:
	
	clear_board();
	XVIControlAnimation.close_window(self);
	LbrRadio.EmitMainMenuRequested();
