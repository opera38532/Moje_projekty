extends Panel

@onready var direction_container = $"Direction container"
@onready var position_container = $"Position container"

var maximum_bit_amount = 4
var temp

signal return_value()

func _ready():
	var right = VarReturn_button.new()
	var left = VarReturn_button.new()
	direction_container.add_child(right)
	direction_container.add_child(left)
	right.value = "Right"
	right.text = "Right"
	left.value = "Left"
	left.text = "Left"
	right.connect("var_pressed",serek)
	left.connect("var_pressed",serek)
	for i in range(maximum_bit_amount):
		var x = maximum_bit_amount-1-i
		var button = VarReturn_button.new()
		button.value = x
		button.text = " "+str(x)+" "
		button.connect("var_pressed",serek)
		position_container.add_child(button)
		
		
func get_modifier(modifier_type):
	if modifier_type == "Position":
		update_location_buttons()
		position_container.visible = true
		direction_container.visible = false
	elif modifier_type == "Direction":
		position_container.visible = false
		direction_container.visible = true
	else:
		push_error("invalid modifier type")
	await return_value
	return temp
	
func serek(value):
	temp = value
	return_value.emit()

func update_location_buttons():
	pass
