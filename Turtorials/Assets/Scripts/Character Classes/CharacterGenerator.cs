using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 350;
	private const int MIN_STARTING_ATRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int pointsLeft;
	
	public GUISkin mySkin;
	
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate(playerPrefab,Vector3.zero , Quaternion.identity)as GameObject;
		pc.name = "pc";
		
	   // _toon = new PlayerCharacter();
	   //_toon.Awake();
	   _toon = pc.GetComponent<PlayerCharacter>();	
		pointsLeft = STARTING_POINTS;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
	    _toon.GetPrimaryAttribute(cnt).BaseValue = MIN_STARTING_ATRIBUTE_VALUE;
		pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATRIBUTE_VALUE);
		}
		_toon.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
	  
	}
	void OnGUI(){
		GUI.skin = mySkin;
		DisplayName();
		DisplayAttributes();
		DisplayPointsLeft();
		
		DisplayVitals();
		DisplaySkills();
		
		if(_toon.Name == "" || pointsLeft >0)
			DisplayCreateLabel();
		else
		DisplayCreateButton();	
	}
	private void DisplayName(){
		GUI.Label(new Rect(10, 10, 50,25), "Name");
		_toon.Name = GUI.TextArea(new Rect(65,10,100,25), _toon.Name);
	}
	
	private void DisplayAttributes(){
			for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			GUI.Label(new Rect(10,40 +(cnt * 25),100,25),"  "+((AttributeName)cnt).ToString());
		    GUI.Label(new Rect(115,40 +(cnt * 25),30,25), "  "+_toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
		    if(GUI.Button(new Rect (150 , 40+(cnt * 25),25,25),"-")){
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATRIBUTE_VALUE){
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
			if(GUI.Button(new Rect (180,40+(cnt * 25),25,25),"+")){
				
					if(pointsLeft > 0){
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				    }
				}
			
		}
	}
	
	private void DisplayVitals(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			GUI.Label(new Rect(10,40 +((cnt + 7) * 25),100,25), "  "+((VitalName)cnt).ToString());
		    GUI.Label(new Rect(115,40 +((cnt + 7)*25),30,25), "  "+_toon.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}
	private void DisplaySkills(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
			GUI.Label(new Rect(250,40 + (cnt * 25),100,25),((SkillName)cnt).ToString());
		    GUI.Label(new Rect(355,40 + (cnt * 25),30,25), "  "+_toon.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}
	private void DisplayPointsLeft(){
		GUI.Label(new Rect(250,10,100,25),"Points Left: "+pointsLeft.ToString());
	}
	private void DisplayCreateLabel(){
		GUI.Label(new Rect(Screen.width/2 - 420,240,80,20),"Create...","Button");
	}
	private void DisplayCreateButton(){
		if(GUI.Button(new Rect(Screen.width/2 - 420,240,80,20), "Create")){
			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
		    UpdateCurVitalValues();
			gsScript.SaveCharacterData();
			Application.LoadLevel("Level1");
		}
	}
	private void UpdateCurVitalValues() {
		for(int cnt = 0; cnt <Enum.GetValues(typeof(VitalName)).Length; cnt++){
			_toon.GetVital(cnt).CurValue = _toon.GetVital(cnt).AdjustedBaseValue;
		}
	}
}
