extends Control

@onready var score_label: Label = %ScoreLabel;

func _ready() -> void:
	hide();
	
	LbrRadio.GameStarted.connect( _on_radio_game_started );

func _process( _delta: float ) -> void:
	score_label.text = str( GlobalVars.Score );

func _on_radio_game_started() -> void:
	XVIControlAnimation.open_window(self);
