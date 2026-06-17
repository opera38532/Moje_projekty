extends Object
class_name Game_object

var class_identifier: String
var display_name: String
var name_id: String


func get_icon_path():
	var path = "res://Assets/Item icons/"+class_identifier+"/"+name_id+".png"
	return path
