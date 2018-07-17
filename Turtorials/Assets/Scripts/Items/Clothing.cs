using UnityEngine;

public class Clothing : BuffItem {
	private ArmorSlat _slat;
	
	public Clothing() {
		_slat = ArmorSlat.Head;
	}
	public Clothing(ArmorSlat slat){
		_slat = slat;
	}
	public ArmorSlat Slat{
		get {return _slat; }
		set {_slat = value; }
	}
}
public enum ArmorSlat {
	Head,
	Shoulders,
	UpperBody,
	Torso,
	Legs,
	Hands,
	Feet,
	Back
}
