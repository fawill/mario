using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaManController : MonoBehaviour {

	[Header("Movement")]
	public float pixelToUnit = 40f;
	public float maxVelocity = 10f; //pixels/seconds
	public Vector3 moveSpeed = Vector3.zero; // (0, 0, 0)

	[Header("Animation")]
	public bool isFacingLeft = false;
	public bool isRunning = false;
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
		animator.SetBool ("isRunning", isRunning);
	}

	void HandleHorizontalMovement(){



		moveSpeed.x = Input.GetAxis("Horizontal") * (maxVelocity / pixelToUnit);

		if (Input.GetAxis ("Horizontal") < 0 && !isFacingLeft) {
			//Change MegaMan face left
			isFacingLeft = true;
		} else if (Input.GetAxis ("Horizontal") > 0 && isFacingLeft) {
			//Change MegaMan face right
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

		rigidbody2D.velocity = new Vector2 (moveSpeed.x, rigidbody2D.velocity.y);

	}

}
