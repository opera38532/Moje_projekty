extends Object
class_name Action_blueprint

var display_name: String
#action name displayed
var incantation: String
#callable of a function the action represents
var global_possible:bool
#is global targeting possible?
var modifier_type: String
#what modifier will the action use,
# "Position" and "Direction" or "None" are now in scope
var max_amount: int

func set_values_in_order(name: String,callable: String,global: bool,modtype: String,amount: int):
	display_name = name
	incantation = callable
	global_possible = global
	modifier_type = modtype
	max_amount = amount
