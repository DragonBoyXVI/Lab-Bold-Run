extends PanelContainer;
class_name ScorePanel;

@export var name_label: Label;
@export var score_label: Label;
@export var date_label: Label;

func set_data( score_data: Dictionary ) -> void:
	
	if (score_data.is_empty()):
		
		name_label.text = "";
		score_label.text = "";
		date_label.text = "";
	else:
		
		name_label.text = tr( "key_name" ) % score_data["name"];
		score_label.text = tr( "key_score" ) % score_data["score"];
		date_label.text = tr( "key_date" ) % score_data["date"];
