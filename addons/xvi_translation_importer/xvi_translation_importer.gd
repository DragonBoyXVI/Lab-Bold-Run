@tool
extends EditorPlugin


const TRANS_IMPORTER_SCRIPT := "res://addons/xvi_translation_importer/Scripts/translation_importer.gd"
const TRANS_IMPORTER_NAME := "TranslationImporter"


func _enter_tree() -> void:
	
	add_autoload_singleton( TRANS_IMPORTER_NAME, TRANS_IMPORTER_SCRIPT )

func _exit_tree() -> void:
	
	remove_autoload_singleton( TRANS_IMPORTER_NAME )
