using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour {

	[Header("Movement")]
	public float pixelToUnit = 40f;
	public float maxVelocity = 10f; //pixels/seconds
	public Vector3 moveSpeed = Vector3.zero; // (0, 0, 0)

	[Header("Animation")]
	public bool isFacingLeft = false;
	public bool isRunning = false;
	public bool isSquat = false;
	public Animator animator;

	[Header("Components")]
	public Rigidbody2D rigidbody2D ;
	public SpriteRenderer spriterenderer;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		UpdateAnimatorParameters ();
		HandleHorizontalMovement ();
		HandleVerticalMovement ();
		MoveCharacterController ();

	}

	void UpdateAnimatorParameters(){
		if (Input.GetAxis ("Vertical") <= 0) {
			animator.SetBool ("isRunning", isRunning);
		} else {
			animator.SetBool ("isRunning", false);
		}
		animator.SetBool ("isSquat", isSquat);
	}

	void HandleHorizontalMovement(){


		if(!isSquat){
			moveSpeed.x = Input.GetAxis("Horizontal") * (maxVelocity / pixelToUnit);
		}

		if (Input.GetAxis ("Horizontal") < 0 && !isFacingLeft) {
			//Change Mario face left
			isFacingLeft = true;
		} else if (Input.GetAxis ("Horizontal") > 0 && isFacingLeft) {
			//Change Mario face right
			isFacingLeft = false;
		}


		if (Input.GetAxis ("Vertical") < 0 && !isFacingLeft) {
			//Squat Mario 
			isSquat = true;
			isFacingLeft = false;

		}

		if (Input.GetAxis ("Vertical") < 0 && isFacingLeft) {
			//Squat Mario 
			isSquat = true;
			isFacingLeft = true;
		}

		if (Input.GetAxis ("Vertical") >= 0) {
			//Squat Mario 
			isSquat = false;
		}

		if (Input.GetAxis ("Vertical") > 0 && isFacingLeft && rigidbody2D.velocity.y <= 2) {
			//Jump Mario 
			Debug.Log(moveSpeed.y);
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, moveSpeed.y+2);
			animator.SetTrigger("Jump");
			isFacingLeft = true;
		}

		if (Input.GetAxis ("Vertical") > 0 && !isFacingLeft && rigidbody2D.velocity.y <= 2) {
			//Jump Mario 
			Debug.Log(moveSpeed.y);
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, moveSpeed.y+2);
			animator.SetTrigger("Jump");
			isFacingLeft = false;
		}


		if (Mathf.Abs(moveSpeed.x) > 0) {
			isRunning = true;
		} else {
			isRunning = false;
		}
			
		spriterenderer.flipX = isFacingLeft;

	}

	void HandleVerticalMovement(){

	}

	void MoveCharacterController(){

	//	rigidbody2D.velocity = moveSpeed;

		if (!isSquat) {
			rigidbody2D.velocity = new Vector2 (moveSpeed.x, rigidbody2D.velocity.y);
		}

	}

}
