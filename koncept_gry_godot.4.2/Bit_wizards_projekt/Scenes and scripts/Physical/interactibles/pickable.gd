extends CharacterBody2D

var contents: Game_object
var amount = 1
var rng = RandomNumberGenerator.new()
var speed = Vector2(0,0)

@onready var icon = $Sprite

func _ready():
	if FileAccess.file_exists(contents.get_icon_path()):
		icon.texture = load(contents.get_icon_path())
	
	velocity = speed.rotated(deg_to_rad(rng.randi_range(-180,180)))

func _physics_process(_delta):
	velocity = velocity*0.80
	move_and_slide()


func _on_area_2d_area_entered(area):
	if area.is_in_group("player_pickup_area"):
		var result = PlayerData.inventory.add_to_inventory(contents,amount)
		if result:
			queue_free()
		else:
			pass
