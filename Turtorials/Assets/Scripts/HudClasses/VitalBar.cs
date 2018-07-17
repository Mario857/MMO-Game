using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	public bool _isPlayerHealthBar;
	
	private int _maxBarLength;
	private int _curBarLength;
	private GUITexture _display;
	void Awake(){
			_display = gameObject.GetComponent<GUITexture>();	
	}
	// Use this for initialization
	void Start () {
	 //   _isPlayerHealthBar = true;
		
		_maxBarLength = (int)(_display.pixelInset.width);
		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnEnable() {
		if(_isPlayerHealthBar){
		Messenger<int , int>.AddListener("player health update", OnChangeHealthBarSize);
		}else{
		ToggleDisplay(false);
		Messenger<int , int>.AddListener("mob health update", OnChangeHealthBarSize);	
		Messenger<bool>.AddListener("show mob vitalbars",ToggleDisplay);
		}
	}
	public void OnDisable() {
		if(_isPlayerHealthBar){
		     Messenger<int , int>.RemoveListener("player health update", OnChangeHealthBarSize);
		}else{
		    Messenger<int , int>.RemoveListener("mob health update", OnChangeHealthBarSize);
		    Messenger<bool>.RemoveListener("show mob vitalbars",ToggleDisplay);
		}
	}
	public void OnChangeHealthBarSize(int curHealth , int maxHealth){
		//Debug.Log("We heard an event: curhealth = " + curHealth + " maxHealth = " + maxHealth);
	    
		_curBarLength = (int)(curHealth /(float)(maxHealth) * _maxBarLength);
		//_display.pixelInset = new Rect(_display.pixelInset.x,_display.pixelInset.y,_curBarLength,_display.pixelInset.height);
		_display.pixelInset = CalculatePosition();
		
	}
	public void SetPlayerHealthBar(bool b){
		_isPlayerHealthBar = b;
	}
	private Rect CalculatePosition(){
		float yPos = _display.pixelInset.y / 2 - 10;
		
		if(!_isPlayerHealthBar){
			float xPos = (_maxBarLength - _curBarLength) - (_maxBarLength / 4 + 10);
			return new Rect(xPos,yPos,_curBarLength,_display.pixelInset.height);
		}
		return new Rect(_display.pixelInset.x,yPos,_curBarLength,_display.pixelInset.height);
		
	}
	private void ToggleDisplay(bool show){
		_display.enabled = show;
	}
}
