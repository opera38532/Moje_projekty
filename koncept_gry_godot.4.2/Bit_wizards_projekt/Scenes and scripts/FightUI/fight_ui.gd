extends Control

@onready var action_panel = $"Action panel"
@export var fight_controller: Node


func _on_button_pressed():
	fight_controller.reprepare_fight()
