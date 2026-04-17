extends Control

func _ready() -> void:
	hide();
	
	%ResumeButton.pressed.connect( _on_resume_button_pressed );
	%MenuButton.pressed.connect( _on_menu_button_pressed );
	%QuitButton.pressed.connect( _on_quit_button_pressed );
	
	PauseManager.GamePaused.connect( _on_pause_manager_game_paused ); 


func _on_resume_button_pressed() -> void:
	PauseManager.SwitchPause();

func _on_menu_button_pressed() -> void:
	PauseManager.SwitchPause();
	LbrRadio.EmitMainMenuRequested();

func _on_quit_button_pressed() -> void:
	get_tree().quit();


func _on_pause_manager_game_paused( is_paused: bool ) -> void:
	if is_paused:
		XVIControlAnimation.open_window(self);
	else:
		XVIControlAnimation.close_window(self);
