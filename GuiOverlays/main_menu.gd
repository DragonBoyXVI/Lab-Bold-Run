extends Control

@onready var play_button: Button = %PlayButton;
@onready var high_score_button: Button = %HighScoresButton;
@onready var quit_button: Button = %QuitButton;

func _ready() -> void:
	show();
	
	play_button.pressed.connect( _on_play_button_pressed );
	high_score_button.pressed.connect( _on_high_score_button_pressed );
	quit_button.pressed.connect( _on_quit_button_pressed );

func _on_play_button_pressed() -> void:
	LbrRadio.EmitGameStarted();
	XVIControlAnimation.close_window(self);

func _on_high_score_button_pressed() -> void:
	print("IMPLEMENT ME!!!!");

func _on_quit_button_pressed() -> void:
	get_tree().quit();
