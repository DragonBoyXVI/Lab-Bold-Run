@tool
@abstract
extends Object;
class_name ScoreFileHandler;

const SAVE_DIR := "user://Scores";

const SCORE_FILE_CONTENTS := "{
	\"name\": \"{0}\",
	\"score\": \"{1}\",
	\"date\": \"{2}\"
}"
const FILE_NAME := "score_%s.json";

static func save_score( name: String, score: int, date: String ) -> void:
	
	if (not DirAccess.dir_exists_absolute(SAVE_DIR)):
		var err := DirAccess.make_dir_recursive_absolute(SAVE_DIR);
		
		if (err != OK):
			push_error("Filed to make score dir: " + error_string(err));
			return;
	
	var dir := DirAccess.open(SAVE_DIR);
	var file_name := FILE_NAME % dir.get_files().size();
	var file := FileAccess.open(SAVE_DIR + "/" + file_name, FileAccess.WRITE);
	
	if (not file):
		var err = FileAccess.get_open_error();
		push_error("Failed to make a file to save a score in: " + error_string(err));
		return;
	
	var file_contents := SCORE_FILE_CONTENTS.format([name, score, date]);
	file.store_string(file_contents);
	file.close();

## returns the array already scored by highest score first
static func get_saved_scores_as_dict() -> Array[ Dictionary ]:
	
	if (not DirAccess.dir_exists_absolute( SAVE_DIR )):
		var err := DirAccess.make_dir_recursive_absolute( SAVE_DIR );
		
		if (err != OK):
			push_error("Could not make save dir: ", error_string(err));
			return [];
	
	var dir := DirAccess.open( SAVE_DIR );
	var file_names: PackedStringArray = dir.get_files();
	var dict_array: Array[Dictionary] = [];
	for file_name: String in file_names:
		if (file_name.get_extension().to_lower() != "json"):
			continue;
		
		var file_path: String = SAVE_DIR + "/" + file_name;
		var file := FileAccess.open(file_path, FileAccess.READ);
		
		if (not file):
			push_error("Could not open file: ", error_string( FileAccess.get_open_error() ));
			continue;
		
		var dict: Dictionary = JSON.parse_string( file.get_as_text() );
		
		if (not dict or dict.is_empty()):
			continue;
		
		dict_array.append(dict);
	
	dict_array.sort_custom( _dict_sort )
	return dict_array;


static func _dict_sort( a: Dictionary, b: Dictionary ) -> bool:
	
	if ( a["score"] == b["score"] ):
		return false;
	else:
		# type hints be damned
		return a["score"].to_int() > b["score"].to_int();
