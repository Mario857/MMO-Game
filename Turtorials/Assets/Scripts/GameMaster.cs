using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;
	public Camera mainCamera;
	public GameObject gameSettings;
	
	public float zOffset;
	public float yOffset;
	public float xRotOffset;
	
	public Vector3 _playerSpawnPointPos; 
	
	private GameObject _pc;
	private PlayerCharacter _pcScript;
	// Use this for initialization
	void Start () {
		//_playerSpawnPointPos = new Vector3(240,6,116);
		
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
		if(go == null){
			
			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
		    
			go.transform.position = _playerSpawnPointPos;
		}
		_pc = Instantiate(playerCharacter,go.transform.position, Quaternion.identity)as GameObject;
	    _pc.name = "pc";
		//tur 44
		_pcScript = _pc.GetComponent<PlayerCharacter>();
		
		zOffset = -2.5f;
		yOffset = 2.5f;	
		xRotOffset = 22.5f;
		
		mainCamera.transform.position =  new Vector3(_pc.transform.position.x, _pc.transform.position.y + yOffset,_pc.transform.position.z + zOffset);
		mainCamera.transform.Rotate(xRotOffset,0,0);
	    
		LoadCharacter();
	}
	public void LoadCharacter(){
	GameObject gs = GameObject.Find("__GameSettings");	
		if(gs == null){
			GameObject gs1 = Instantiate(gameSettings,Vector3.zero,Quaternion.identity)as GameObject;
		    gs1.name ="__GameSettings";
		}
		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			
	    gsScript.LoadCharacterData();
	}
}
