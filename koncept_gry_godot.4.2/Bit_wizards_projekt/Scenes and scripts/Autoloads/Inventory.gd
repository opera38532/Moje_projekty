extends Node

var inventory_array = []
var amount_limit = 15
var entry_limit = 3

signal inventory_changed()

func add_to_inventory(object: Game_object,add_amount: int):
	var match_attempt = match_entry(object)
	if match_attempt == null:
		return add_new_entry(object,add_amount)
	else:
		return add_to_entry(match_attempt, add_amount)




func match_entry(object: Game_object):
	for entry in inventory_array:
		if object.name_id == entry.contents.name_id:
			return entry
	return null

func add_to_entry(entry: Inventory_entry,add_amount: int):
	if !check_for_amount_limit(add_amount):
		return false
	else:
		entry.amount += add_amount
		inventory_changed.emit()
		return true

func add_new_entry(object: Game_object,add_amount: int):
#kocham podejmować gameplayowe decyzjyzje których potem będę żałował
#unikalne itemy nie liczą się do entry_limit
	var entry_limit_respected
	var amount_limit_respected
	if object.class_identifier == "Material":
		entry_limit_respected = check_for_entry_limit()
	else:
		entry_limit_respected = true
	amount_limit_respected = check_for_amount_limit(add_amount)
	
	if amount_limit_respected and entry_limit_respected:
		var new_entry = Inventory_entry.new()
		new_entry.contents = object
		new_entry.amount = add_amount
		inventory_array.append(new_entry)
		inventory_changed.emit()
		return true
	else:
		return false

func check_for_entry_limit():
	var limit_counter = 0
	for entry in inventory_array:
		if entry.contents.class_identifier == "Material":
			limit_counter += 1
	if limit_counter<entry_limit:
		return true
	elif limit_counter== entry_limit:
		return false
	elif limit_counter > entry_limit:
		push_warning("Inventory.gd Inventory entry overflow")
		#to sie kurwa nigdy nie powinno stac ale zobaczymy
		return false

func check_for_amount_limit(add_amount: int):
	var limit_counter = 0
	for entry in inventory_array:
		limit_counter += entry.amount
	limit_counter += add_amount
	if limit_counter<amount_limit:
		return true
	elif limit_counter== amount_limit:
		return false
	elif limit_counter > amount_limit:
		push_warning("Inventory.gd Inventory amount overflow")
		#to sie kurwa nigdy nie powinno stac ale zobaczymy part2
		return false

func clear_inventory():
	for entry in inventory_array:
		entry.free()
	inventory_array = []
