extends Panel


@onready var enemy_array = $"..".fight_controller.enemy_array

var temp
var global_button
var enemy_button_array = []

signal return_value()

func _ready():
	global_button = VarReturn_button.new()
	$VBoxContainer.add_child(global_button)
	global_button.value = -1
	global_button.text = "Global"
	global_button.connect("var_pressed",serek)
	$VBoxContainer/HBoxContainer.free()
	var hbox = HBoxContainer.new()
	$VBoxContainer.add_child(hbox)
	for i in range(len(enemy_array)):
		var button = VarReturn_button.new()
		hbox.add_child(button)
		button.value = i
		button.text = "Enemy"+str(i+1)
		button.connect("var_pressed",serek)
		enemy_button_array.append(button)
		

func get_target(global_permission):
	global_button.visible = global_permission
	update_enemy_buttons()
	await return_value
	return temp

func serek(value):
	temp = value
	return_value.emit()

func update_enemy_buttons():
	var i = 0
	enemy_array = $"..".fight_controller.enemy_array
	for enemy in enemy_array:
		if enemy != null:
			enemy_button_array[i].visible = true
		else:
			enemy_button_array[i].visible = false
		i += 1
