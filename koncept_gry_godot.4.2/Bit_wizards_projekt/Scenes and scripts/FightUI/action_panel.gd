extends Panel

var payload

@onready var action_panel = $"."
@onready var target_panel = $"../Target panel"
@onready var modifier_panel = $"../Modifier panel"

signal ready_to_send()

func _ready():
	for spell in PlayerData.spell_array:
		var button = Action_button.new()
		button.spell_blueprint = spell
		$GridContainer.add_child(button)
		button.connect("action_input_complete",serek)
		button.action_panel = action_panel
		button.target_panel = target_panel
		button.modifier_panel = modifier_panel
		
func get_user_input():
	visible = true
	await ready_to_send
	return payload
	
func serek(action):
	payload = action
	ready_to_send.emit()
