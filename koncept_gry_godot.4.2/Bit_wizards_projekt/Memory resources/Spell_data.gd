extends Resource

var bit_flip = Action_blueprint.new()
var bit_shift = Action_blueprint.new()
var bit_rotate = Action_blueprint.new()
var bit_inverse = Action_blueprint.new()
var bit_reverse = Action_blueprint.new()
var increment = Action_blueprint.new()
var decrement = Action_blueprint.new()
var spell_array = [bit_flip,bit_shift,bit_rotate,bit_inverse,bit_reverse,increment,decrement]

func _init():
	bit_flip.set_values_in_order("Bit flip","bit_flip",true,"Position",1)
	bit_shift.set_values_in_order("Bit shift","bit_shift",true,"Direction",1)
	bit_rotate.set_values_in_order("Bit rotate","bit_rotate",true,"Direction",1)
	bit_inverse.set_values_in_order("Bit inverse","bit_inverse",true,"None",1)
	bit_reverse.set_values_in_order("Bit reverse","bit_reverse",true,"None",1)
	increment.set_values_in_order("Increment","increment",true,"None",1)
	decrement.set_values_in_order("Decrement","decrement",true,"None",1)
