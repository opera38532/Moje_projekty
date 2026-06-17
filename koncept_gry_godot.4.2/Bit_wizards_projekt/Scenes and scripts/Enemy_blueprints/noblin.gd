extends Node2D

@onready var hp_module = $binary_display_central

@export var type: int
var enemy_type
var rng = RandomNumberGenerator.new()

@onready var animation = $AnimatedSprite2D

func _ready():
	type = rng.randi_range(0,2)
	match type:
		0:
			enemy_type = "archer"
		1:
			enemy_type = "berserk"
		2:
			enemy_type = "normal"
	animation.play(enemy_type)


func take_action():
	match enemy_type:
		"archer":
			print(enemy_type,archer_action())
			return archer_action()
		"berserk":
			print(enemy_type,berserk_action())
			return berserk_action()
		"normal":
			print(enemy_type,normal_action())
			return normal_action()


func archer_action():
	return hp_module.get_bit_amount()

func berserk_action():
	return floor(hp_module.get_dec_missing_hp()/2)
	
func normal_action():
	return 1
