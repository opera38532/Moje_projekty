extends Button
class_name VarReturn_button

var value
signal var_pressed(value)

func _ready():
	connect("pressed",serek)
	
	
func serek():
	var_pressed.emit(value)
