extends Label

func _process(_delta):
	text = "damage sustained: " + str($"..".character_damage)
