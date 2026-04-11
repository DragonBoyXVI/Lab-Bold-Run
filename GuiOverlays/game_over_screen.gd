extends Control

@onready var score_label: Label = %ScoreLabel;
@onready var menu_button: Button = %MenuButton;
@onready var score_button: Button = %ScoreButton;

func _ready() -> void:
	hide();
	
	LbrRadio.GameEnded.connect( _on_radio_game_ended );
	menu_button.pressed.connect( _on_menu_button_pressed );
	score_button.pressed.connect( _on_score_button_pressed );


func _on_menu_button_pressed() -> void:
	
	XVIControlAnimation.close_window(self);
	LbrRadio.EmitMainMenuRequested();

func _on_score_button_pressed() -> void:
	
	XVIControlAnimation.close_window(self);
	LbrRadio.EmitScoreSaveScreenRequested();

func _on_radio_game_ended() -> void:
	
	XVIControlAnimation.open_window(self);
	
	const key_string := "key_your_score";
	score_label.text = tr(key_string) % GlobalVars.Score;
