extends Control

@onready var play_button: Button = %PlayButton;
@onready var high_score_button: Button = %HighScoresButton;
@onready var quit_button: Button = %QuitButton;

func _ready() -> void:
	show();
	
	play_button.pressed.connect( _on_play_button_pressed );
	high_score_button.pressed.connect( _on_high_score_button_pressed );
	quit_button.pressed.connect( _on_quit_button_pressed );
	
	LbrRadio.MainMenuRequested.connect( _on_radio_main_menu_requested );

func _on_play_button_pressed() -> void:
	LbrRadio.EmitGameStarted();
	XVIControlAnimation.close_window(self);

func _on_high_score_button_pressed() -> void:
	
	LbrRadio.EmitScoreScreenRequested();
	XVIControlAnimation.close_window(self);

func _on_quit_button_pressed() -> void:
	get_tree().quit();

func _on_radio_main_menu_requested() -> void:
	XVIControlAnimation.open_window(self);
