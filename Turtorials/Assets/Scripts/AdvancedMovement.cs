using UnityEngine;
using System.Collections;
[RequireComponent (typeof(CharacterController))]
public class AdvancedMovement : MonoBehaviour {
	public enum State {
		Idle,
		Init,
		Setup,
		Run
	}
	public enum Turn{
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward {
		back = -1,
		none = 0,
		forward = 1
	}
	public float walkSpeed = 5;
	public float runMultiplier = 2;
	public float RotateSpeed = 250;
	public float Gravity = 20;
	public float airTime = 0;
	public float fallTime = .5f;
	public float jumpHeight = 8;
	public float jumpTime = 1.5f;
	public CollisionFlags _collisionFlags;
	private Vector3 _moveDirection;
	private Transform _myTransform;
	private CharacterController _controller;
	private bool _run;
	private bool _jump;
	
	private State _state;
	
	private Turn _turn;
	private Forward _forward;
	
	public void Awake(){
		_myTransform = transform;
		_controller  = GetComponent<CharacterController>();
		
		_state = AdvancedMovement.State.Init;
	}
	IEnumerator Start () {
		while(true){
			switch(_state){
			case State.Init:
				Init();
				break;
			case State.Setup:
				SetUp();
				break;
			case State.Run:
				ActionPicker();
				break;
			}
			yield return null;
		}
	}
	private void Init(){
		if(!GetComponent<CharacterController>()) return;
		if(!GetComponent<Animation>()) return;
		
		_state = AdvancedMovement.State.Setup;
	}
	private void SetUp(){	
		animation.Stop();
		
		animation.wrapMode = WrapMode.Loop;
		
		animation["Jump"].layer = 1;
		animation["Jump"].wrapMode = WrapMode.Once;
		
		animation.Play("GoodIdle");
		
		_moveDirection = Vector3.zero;
		_turn = AdvancedMovement.Turn.none;
		_forward = AdvancedMovement.Forward.none;
		_run = true;
		_jump = false;
		
		_state = AdvancedMovement.State.Run;
	}
	private void ActionPicker(){
		_myTransform.Rotate(0,(int)_turn * Time.deltaTime * RotateSpeed,0);
		if(_controller.isGrounded){
			airTime = 0;
			
			_moveDirection = new Vector3(0,0,(int)_forward);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			if(_forward != Forward.none){
				Walk();
				if(_run){
					_moveDirection *= runMultiplier;
					Run();
				}

			}
			else{
				Idle();
			}
			if(_jump){
				if(airTime < jumpTime){
					_moveDirection.y += jumpHeight;
					Jump();
					_jump = false;
				}
			}
		}
		else{
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0){
				airTime += Time.deltaTime;
				
				if(airTime >fallTime){
					Fall();
				}
			}
		}
		_moveDirection.y -= Gravity * Time.deltaTime;
		 _collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);
	}
	public void MoveMeForward(Forward z){
		_forward = z;
	}
	public void RotateMe(Turn y){
		_turn = y;
	}
	public void ToggleRun(){
		_run = !_run;
	}
	public void JumpUp() {
		_jump = true;
	}
	public void Idle(){
		animation.CrossFade("GoodIdle");
	}
	public void Walk(){
		animation.CrossFade("GoodWalk");
	}
	public void Run(){
		animation.CrossFade("GoodWalk");
	}
	public void Fall(){
		animation.CrossFade("CombatStance");
	}
	public void Jump(){
		animation.CrossFade("Jump");
	}
	//turtorial 104 
}
