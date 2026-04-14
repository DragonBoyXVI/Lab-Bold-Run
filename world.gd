extends Node2D
# this node persists for the whole game.

const PLAYER_SCENE: PackedScene = preload( "res://Player/player_body.tscn" );

const TRANSLATIONS_RES_PATH := "res://Translations";
const TRANSLATIONS_USER_PATH := "user://Translations";
const DEFAULT_TRANSLATION := "en";

@onready var fps_label: Label= %FPSLabel;
const DRAW_FPS := true;

func _ready() -> void:
	fps_label.visible = DRAW_FPS;
	
	LbrRadio.GameStarted.connect( _on_radio_game_started );
	
	# loads translations
	TranslationImporter.parse_file_for_dict(TRANSLATIONS_RES_PATH + "/en.json");
	var trangener := TRANSLATIONS_USER_PATH + "/en.json";
	if (FileAccess.file_exists(trangener)):
		TranslationImporter.parse_file_for_dict(trangener);
	TranslationServer.set_locale(DEFAULT_TRANSLATION);

func _process(_delta: float) -> void:
	if DRAW_FPS:
		fps_label.text = str(Engine.get_frames_per_second());

func _on_radio_game_started() -> void:
	
	var player: CharacterBody2D = PLAYER_SCENE.instantiate();
	add_child(player);
	player.position = %PlayerSpawnSpot.global_position;
	player.reset_physics_interpolation();
	
	GlobalVars.Score = 0;
	GlobalVars.WorldSpeed = 1.0;
