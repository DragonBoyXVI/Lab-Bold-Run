extends Node2D
# this node persists for the whole game.

const PLAYER_SCENE: PackedScene = preload( "res://Player/player_body.tscn" );

const TRANSLATIONS_RES_PATH := "res://Translations";
const TRANSLATIONS_USER_PATH := "user://Translations";
const DEFAULT_TRANSLATION := "en";

func _ready() -> void:
	
	LbrRadio.GameStarted.connect( _on_radio_game_started );
	
	# loads translations
	TranslationImporter.parse_file_for_dict(TRANSLATIONS_RES_PATH + "/en.json");
	TranslationServer.set_locale(DEFAULT_TRANSLATION);

func _on_radio_game_started() -> void:
	
	var player: CharacterBody2D = PLAYER_SCENE.instantiate();
	add_child(player);
	player.position = %PlayerSpawnSpot.global_position;
	player.reset_physics_interpolation();
	
	GlobalVars.Score = 0;
	GlobalVars.WorldSpeed = 1.0;
