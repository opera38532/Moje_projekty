extends Destructible
class_name Tree_type

@onready var damage_effect = load("res://Scenes and scripts/Physical/effects/tree_slash.tscn")
@export var damage_effect_offset = Vector2(-1,-14)
@onready var stump = $stump
@onready var crown = $crown
@onready var animation_player = $AnimationPlayer
@onready var tree_material = material_data.wood_array[tier-1]

var rng = RandomNumberGenerator.new()
var damage_effect_variant = 1
var cut_down = false
var falling_multiplier = 0.5

func _ready():
	super()
	connect("took_damage",on_damage)
	if cut_down:
		crown.visible = false

func _physics_process(_delta):
	pass

func on_damage():
	if PlayerData.axe_power>=tier:
		hp += -PlayerData.axe_power
	if not cut_down:
		play_effect()
	if hp<= 0:
		cut_down = true
		animation_player.play("falling down")

func play_effect():
	var instance = damage_effect.instantiate()
	instance.position = damage_effect_offset
	add_child(instance)
	instance.play(str(damage_effect_variant))
	damage_effect_variant += 1
	if damage_effect_variant > 4:
		damage_effect_variant = 1

func drop_pickable():
	for i in range(rng.randi_range(2,3)):
		var instance = load("res://Scenes and scripts/Physical/interactibles/pickable.tscn")
		instance = instance.instantiate()
		instance.contents = tree_material
		instance.position.x = rng.randi_range(-10,-50)
		add_child(instance)


func _on_animation_player_animation_finished(_anim_name):
	drop_pickable()
