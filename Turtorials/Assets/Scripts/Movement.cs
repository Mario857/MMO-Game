using UnityEngine;
using System.Collections;
[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour {
	public float RotateSpeed = 250.0f;
	public float runMultipler = 2;
	public float MoveSpeed = 5.0f;
	public float StrafeSpeed = 2.5f;
	public float Gravity = 10;
	private Transform _myTransform;
	private CharacterController _controller;
	
	public void Awake(){
		_myTransform = transform;
		_controller  = GetComponent<CharacterController>();
	}
	void Start () {
		animation.wrapMode = WrapMode.Loop;
	}
	void Update () {
		Turn();
		Walk();
		SetGravity();
		//Strafe();
	}
	private void Turn(){
		if(Mathf.Abs(Input.GetAxis("Rotate Player")) > 0){
			//Debug.Log("Rotate: " + Input.GetAxis("Rotate Player"));
			_myTransform.Rotate(0,Input.GetAxis("Rotate Player") * Time.deltaTime * RotateSpeed,0);
		}
	}
	private void Walk(){
		if(Mathf.Abs(Input.GetAxis("Move Forward")) > 0){
			//Debug.Log("Forward: " + Input.GetAxis("Move Forward"));
			if(Input.GetButton("Run")){
				animation.CrossFade("GoodWalk");
				_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward)* Input.GetAxis("Move Forward") * MoveSpeed * runMultipler);
			}
			else{
			_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward)* Input.GetAxis("Move Forward") * MoveSpeed);
			animation.CrossFade("GoodWalk");
			}
		}
		else{
			animation.CrossFade("GoodIdle");
		}
	}
	private void Strafe(){
		if(Mathf.Abs(Input.GetAxis("Strafe")) > 0){
			Debug.Log("Strafe: " + Input.GetAxis("Strafe"));
			_controller.SimpleMove(_myTransform.TransformDirection(Vector3.right)* Input.GetAxis("Strafe") * StrafeSpeed);
		}
	}
	private void SetGravity(){
		_controller.SimpleMove(_myTransform.TransformDirection(-Vector3.up)* Gravity);
	}
}
//96 Turtorial