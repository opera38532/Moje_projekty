extends Node2D

@onready var rng = RandomNumberGenerator.new()
@onready var placeholder = $TextureRect

@export var bit_amount = 4
var value_ar = Array()
var display_ar = Array()
var offset = 0

var bit = load("res://Scenes and scripts/Binary display/bit_disp.tscn")

func _ready():
	generate()

func _process(_delta):
	update_display()

func update_display():
	for i in range(len(value_ar)):
		if value_ar[i]:
			display_ar[i].play("1")
		else:
			display_ar[i].play("0")

func generate():
	placeholder.queue_free()
	for i in range(bit_amount):
		value_ar.append(random_bool())
		var instance = bit.instantiate()
		add_child(instance)
		instance.position.x = offset
		display_ar.append(instance)
		offset += -20
	
func random_bool():
	if rng.randi_range(0,1) == 1:
		return true
	else:
		return false
	
func set_hp(value):
	var temp = value
	for i in range(len(value_ar)):
		var x = len(value_ar)-1-i
		if temp >= pow(2,x):
			value_ar[x] = true
			temp = temp - pow(2,x)
		else:
			value_ar[x] = false
func get_bit_amount():
	var amount = 0
	for i in range(len(value_ar)):
		if value_ar[i] == true:
			amount += 1
	return amount
	
func get_dec_hp():
	var amount = 0
	for i in range(len(value_ar)):
		if value_ar[i] == true:
			amount += pow(2,i)
	return amount
	
func get_dec_missing_hp():
	var amount = 0
	for i in range(len(value_ar)):
		if value_ar[i] == false:
			amount += pow(2,i)
	return amount

func bit_flip(i):
	value_ar[i] = !value_ar[i]

func bit_shift(direction):
	var length = len(value_ar)
	var last = length-1
	if direction == "Right":
		for i in range(last):
			value_ar[i] = value_ar[i+1]
		value_ar[last] = false
	elif direction == "Left":
		for i in range(last):
			var x = last-i
			value_ar[x] = value_ar[x-1]
		value_ar[0] = false
	else:
		print("bit shift invalid direction")
	
func bit_rotate(direction):
	var length = len(value_ar)
	var last = length-1
	var rotate_cache
	if direction == "Right":
		rotate_cache = value_ar[0]
		for i in range(last):
			value_ar[i] = value_ar[i+1]
		value_ar[last] = rotate_cache
	elif direction == "Left":
		rotate_cache = value_ar[last]
		for i in range(last):
			var x = last-i
			value_ar[x] = value_ar[x-1]
		value_ar[0] = rotate_cache
	else:
		print("bit rotate invalid direction")
	
func bit_inverse(_serek):
	for i in range(len(value_ar)):
		value_ar[i] = !value_ar[i]

func bit_reverse(_serek):
	value_ar.reverse()

func increment(_serek):
	var temp = get_dec_hp()
	temp += 1
	if temp > pow(2,bit_amount)-1:
		set_hp(0)
	else:
		set_hp(temp)

func decrement(_serek):
	var temp = get_dec_hp()
	temp += -1
	if temp < 0:
		set_hp(0)
	else:
		set_hp(temp)
