using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
	public enum State{
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}
	
	public GameObject[] mobPrefabs;
	public GameObject[] spawnPoints;
	
	public State state;
	// Use this for initialization
	void Awake(){
		state = MobGenerator.State.Initialize;
	}
	
	IEnumerator Start () {
	     while(true){
			switch(state){
			case State.Initialize:
				Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;
			}
			yield return 0;
		}
	}
	private void Initialize(){
		Debug.Log("***We are in Initialize function***");
		if(!CheckForMobPrefabs())
			return;
		
		if(!CheckForSpawnPoints())
			return;
		
		state = MobGenerator.State.Setup;
	}
	
	private void Setup(){
		Debug.Log("***We are in Setup function***");
		
		state = MobGenerator.State.SpawnMob;
	}
	
	private void SpawnMob(){
		Debug.Log("Spawn Mob");
		
		GameObject[] gos = AvalibleSpawnPoints();
		
		for(int cnt = 0; cnt < gos.Length; cnt++){
			GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)],
				gos[cnt].transform.position, Quaternion.identity) 
				    as GameObject;
			go.transform.parent = gos[cnt].transform;
		}
		
		state = MobGenerator.State.Idle;
	}
	
	
	private bool CheckForMobPrefabs(){
		if(mobPrefabs.Length >0)
			return true;
		else
			return false;
	}
	private bool CheckForSpawnPoints(){
		if(spawnPoints.Length > 0)	
		    return true;
		else
			return false;
	}
	
	private GameObject[] AvalibleSpawnPoints(){
		List<GameObject> gas = new List<GameObject>();
		
		for(int cnt = 0;cnt < spawnPoints.Length; cnt++){
			if(spawnPoints[cnt].transform.childCount == 0){
				Debug.Log("***Spawn Point Avalible***");
				gas.Add(spawnPoints[cnt]);
			}
		}
		return gas.ToArray();
	}
}