extends Node

var spell_data = load("res://Memory resources/Spell_data.tres")
var spell_array = spell_data.spell_array

@onready var inventory = $Inventory

var walking_speed = 100

var attack_speed = 0.5

var axe_power = 1
var pickaxe_power = 1
var sickle_power = 1
