extends Node2D

const PLAYER_SCENE: PackedScene = preload( "res://Player/player_body.tscn" );

func _ready() -> void:
	
	LbrRadio.GameStarted.connect( _on_radio_game_started );

func _on_radio_game_started() -> void:
	
	var player: CharacterBody2D = PLAYER_SCENE.instantiate();
	add_child(player);
	player.position = %PlayerSpawnSpot.global_position;
	player.reset_physics_interpolation();
