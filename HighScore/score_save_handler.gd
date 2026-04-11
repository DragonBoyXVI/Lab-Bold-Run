@tool
@abstract
extends Object;
class_name ScoreFileHandler;

const SAVE_DIR := "user://Scores";

const SCORE_FILE_CONTENTS := "{
	\"name\": {0},
	\"score\": {1},
	\"date\": {2}
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
