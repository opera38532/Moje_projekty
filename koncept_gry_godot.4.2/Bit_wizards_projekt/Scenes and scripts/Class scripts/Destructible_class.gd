extends Node2D
class_name Destructible

var material_data = load("res://Memory resources/Material_data.tres")

@export var hp: int
@export var tier: int 
signal took_damage()

func _ready():
	add_to_group("Destructible")

func damage():
	took_damage.emit()
