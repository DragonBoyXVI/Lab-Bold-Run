extends Control

@onready var line_edit: LineEdit = %LineEdit;
@onready var main_menu_button: Button = %MainMenuButton;
@onready var save_button: Button = %SaveButton;

func _ready() -> void:
	hide();
	
	LbrRadio.ScoreSaveScreenRequested.connect( _on_radio_save_score_screen_requested );
	main_menu_button.pressed.connect( _on_main_menu_button_pressed );
	save_button.pressed.connect( _on_save_button_pressed );


func _on_main_menu_button_pressed() -> void:
	
	XVIControlAnimation.close_window(self);
	LbrRadio.EmitMainMenuRequested();

func _on_save_button_pressed() -> void:
	
	var time := Time.get_datetime_dict_from_system();
	var date_string := "{0}/{1}/{2}".format([time.year, time.month, time.day]);
	
	ScoreFileHandler.save_score(line_edit.text, GlobalVars.Score, date_string);
	
	line_edit.editable = false;
	save_button.disabled = true;
	save_button.text = tr( "key_saved" );

func _on_radio_save_score_screen_requested() -> void:
	XVIControlAnimation.open_window(self);
	
	line_edit.editable = true;
	line_edit.text = "";
	save_button.disabled = false;
	save_button.button_pressed = false;
	save_button.text = tr( "key_save" );
