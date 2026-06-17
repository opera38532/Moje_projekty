extends CharacterBody2D
var direction
var aimdirection
var speed = 100
var newangl = 90

@onready var model = $Node3D/Cube
@onready var raycast = $RayCast2D
@onready var attack_cooldown_timer = $Timer
@onready var marker = $RayCast2D/Marker
var attack_cooldown = false

func _physics_process(_delta):
	get_movement_and_aim_input()
	attack()
	move_and_slide()
	
func attack():
	if Input.is_action_pressed("interact") and raycast.is_colliding() and !attack_cooldown:
		var collider = raycast.get_collider().get_parent()
		if collider.is_in_group("Destructible"):
			collider.damage()
			attack_cooldown = true
			attack_cooldown_timer.start(PlayerData.attack_speed)
		

func get_movement_and_aim_input():
	velocity = Vector2.ZERO
	direction = Input.get_vector("left","right","up","down")
	aimdirection = Input.get_vector("aimleft","aimright","aimup","aimdown")
	
	velocity = direction*PlayerData.walking_speed
	if direction != Vector2(0,0) and !Input.is_action_pressed("bitflip") and aimdirection.length()<=0:
		newangl = round(rad_to_deg(direction.angle()))
		marker.visible = false
	elif Input.is_action_pressed("bitflip"):
		newangl = round(rad_to_deg(get_local_mouse_position().angle()))
		marker.visible = true
	elif aimdirection.length()>0:
		newangl = round(rad_to_deg(aimdirection.angle()))
		marker.visible = true
	model.rotation_degrees.y = -newangl
	raycast.rotation_degrees = newangl


func _on_timer_timeout():
	attack_cooldown = false
