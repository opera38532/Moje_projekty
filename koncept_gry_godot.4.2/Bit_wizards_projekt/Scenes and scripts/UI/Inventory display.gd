extends Control

@onready var inventory = PlayerData.inventory.inventory_array
@onready var entry_array = [$VBoxContainer/TextureRect]


func _ready():
	PlayerData.inventory.connect("inventory_changed", redraw_inventory_display)
	redraw_inventory_display()

func redraw_inventory_display():
	for instance in entry_array:
		instance.queue_free()
	entry_array = []
	for entry in inventory:
		var instance = load("res://Scenes and scripts/UI/inventory_display_entry.tscn")
		instance = instance.instantiate()
		if FileAccess.file_exists(entry.contents.get_icon_path()):
			instance.texture = load(entry.contents.get_icon_path())
		instance.get_child(0).text = "x"+str(entry.amount)
		$VBoxContainer.add_child(instance)
		entry_array.append(instance)
