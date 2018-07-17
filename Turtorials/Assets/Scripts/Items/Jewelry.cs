using UnityEngine;

public class Jewelry : BuffItem {
	private JewelrySlat _slat;
	
	public Jewelry(){
		_slat = JewelrySlat.PocketItem;
	}
	public Jewelry(JewelrySlat slat){
		_slat = slat;
	}
	public JewelrySlat Slat {
		get{return _slat; }
		set{_slat = value; }
	}
}
public enum JewelrySlat {
	EarRings,
	Necklaces,
	Bracelets,
	Rings,
	Pock,
	PocketItem
}