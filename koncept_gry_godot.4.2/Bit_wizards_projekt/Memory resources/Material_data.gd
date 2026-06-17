extends Resource

enum material_variant {Fiber,Wood,Stone,Ore,Gem}

var roughfiber = Material_type.new()
var fiber_array = [roughfiber]

var deadwood = Material_type.new()
var greenwood = Material_type.new()
var wood_array = [deadwood,greenwood]

var brittlestone = Material_type.new()
var stone_array = [brittlestone]

func _init():
	roughfiber.variant = material_variant.Fiber
	roughfiber.tier = 1
	roughfiber.display_name = "Rough fiber"
	roughfiber.name_id = "roughfiber"
	
	
	deadwood.variant = material_variant.Wood
	deadwood.tier = 1
	deadwood.display_name = "Dead wood"
	deadwood.name_id = "deadwood"
	
	greenwood.variant = material_variant.Wood
	greenwood.tier = 2
	greenwood.display_name = "Green wood"
	greenwood.name_id = "greenwood"
