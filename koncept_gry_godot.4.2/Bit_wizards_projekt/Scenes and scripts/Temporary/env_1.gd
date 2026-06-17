extends Node

var rng = RandomNumberGenerator.new()
var noblin = load("res://Scenes and scripts/Enemy_blueprints/noblin.tscn")
var fight_ui = load("res://Scenes and scripts/FightUI/fight_ui.tscn")
var fight_ui_instnace: Node
var enemy_array = []
var enemy_hp_memory_array = []
var fight_ongoing = true
var turn_ongoing = false

var character_damage = 0

func _ready():
	prepare_fight()
	var instance = fight_ui.instantiate()
	instance.fight_controller = $"."
	add_child(instance)
	fight_ui_instnace = instance

func _process(_delta):
	if fight_ongoing:
		$Label2.visible = false
		if turn_ongoing == false:
			turn_ongoing = true
			turn_process()
	else:
		$Label2.visible = true

func turn_process():
	print("Fight controller: new turn")
	var player_action = await fight_ui_instnace.action_panel.get_user_input()
	player_action_handler(player_action)
	look_for_deaths()
	for enemy in enemy_array:
		if enemy != null:
			character_damage += enemy.take_action()
	turn_ongoing = false


func prepare_fight():
	for i in range(4):
		var instance = noblin.instantiate()
		instance.position = Vector2(106*(i+1),150)
		add_child(instance)
		enemy_array.append(instance)
	for enemy in enemy_array:
		enemy_hp_memory_array.append(enemy.hp_module.get_dec_hp())
	fight_ongoing = true
	print("Fight controller: enemies generated")

func reprepare_fight():
	for enemy in enemy_array:
		if enemy != null:
			enemy.free()
	enemy_array = []
	enemy_hp_memory_array = []
	character_damage = 0
	prepare_fight()
	fight_ongoing = true


func player_action_handler(action):
	var incantation = action.type
	if action.target == -1:
		for enemy in enemy_array:
			if enemy != null:
				var callable = Callable(enemy.hp_module,incantation)
				callable.call(action.modifier)
	else:
		if enemy_array[action.target] != null:
			var callable = Callable(enemy_array[action.target].hp_module,incantation)
			callable.call(action.modifier)


func look_for_deaths():
	for enemy in enemy_array:
		if enemy != null:
			if enemy.hp_module.get_dec_hp() == 0:
				enemy.free()
				enemy = null
				fight_ongoing = false
	for enemy in enemy_array:
		if enemy != null:
			fight_ongoing = true
