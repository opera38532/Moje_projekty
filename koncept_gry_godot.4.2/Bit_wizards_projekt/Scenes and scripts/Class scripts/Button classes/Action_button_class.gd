extends Button
class_name Action_button

var spell_blueprint: Action_blueprint
var target_panel
var modifier_panel
var action_panel

var target
var modifier

var action = Action.new()

signal action_input_complete(action)

func _ready():
	action.type = spell_blueprint.incantation
	text = spell_blueprint.display_name
	connect("pressed",on_button_pressed)


func on_button_pressed():
	action_panel.visible = false
	target_panel.visible = true
	action.target = await target_panel.get_target(spell_blueprint.global_possible)
	target_panel.visible = false
	if spell_blueprint.modifier_type == "None":
		action.modifier = null
		modifier_panel.visible = false
	else:
		modifier_panel.visible = true
		action.modifier = await modifier_panel.get_modifier(spell_blueprint.modifier_type)
		modifier_panel.visible = false
	action_input_complete.emit(action)
