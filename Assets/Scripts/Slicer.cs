using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private CircleCollider2D myCollider;

	private Vector2 nextVelocity;

	public static Slicer theSlicer;
	private bool jumpFlag;
	private bool canSlice;
	private bool holdingSlice;

	private Sensor myFoot;
	private Sensor myLeft;
	private Sensor myRight;

	private const float JUMP_VELOCITY = 14.0f;
	private const float GRAVITY_ACCEL = 30.0f;
	private const float MOVE_SPEED = 25.0f;
	private const float GROUND_SLOWDOWN_FACTOR = 0.4f;
	private const float AIR_SLOWDOWN_FACTOR = 0.8f;
	private const float WALL_JUMP_HORIZONTAL_VELOCITY = 8.0f;

	private const float TERMINAL_VELOCITY_X = 5.0f;
	private const float TERMINAL_VELOCITY_Y = 17.0f;

	private MyInputManager myInputManager;

	// Use this for initialization
	void Start () {

		this.myRigidbody = this.GetComponent<Rigidbody2D>();
		this.myCollider = this.GetComponent<CircleCollider2D>();
		this.myInputManager = this.GetComponent<MyInputManager>();

		this.nextVelocity = Vector2.zero;

		this.jumpFlag = false;
		this.canSlice = true;
		this.holdingSlice = false;

		this.myFoot = this.transform.Find("Foot").gameObject.GetComponent<Sensor>();
		this.myLeft = this.transform.Find("Left").gameObject.GetComponent<Sensor>();
		this.myRight = this.transform.Find("Right").gameObject.GetComponent<Sensor>();

		Slicer.theSlicer = this;

	}
	
	// Update is called once per frame
	void Update () {

		this.nextVelocity = this.myRigidbody.velocity;

		if (!this.myFoot.IsOccupied()) {
			this.fall();
		}
		this.processInputBuffer();
		this.enforceTerminalVelocity();

		this.myRigidbody.velocity = this.nextVelocity * MyTime.GetTimeScale();
		
	}

	private void fall() {
		this.nextVelocity.y -= GRAVITY_ACCEL * Time.deltaTime;
	}

	private void processInputBuffer() {
		while (this.myInputManager.HasNext()) {
			InputBufferElement next = this.myInputManager.GetNext();
			if (next.GetStick() == Vector2.zero) {
				slow();
			}
			switch(next.GetMove()) {
				case Move.JUMP:
					this.processJump();
					break;

				case Move.HOLD_SLICE:
					processHoldSlice();
					break;

				case Move.RELEASE_SLICE:
					processReleaseSlice();
					break;

				case Move.MOVE:
					processMove(next.GetStick().x);
					break;
			}
		}
	}

	private void processJump() {
		if (this.jumpFlag && Slicer.CanJump()) {
			this.nextVelocity = this.GetJumpVector();
			this.jumpFlag = false;
		}
	}

	private void processHoldSlice() {

	}

	private void processReleaseSlice() {

	}

	private void processMove(float stickX) {
		nextVelocity.x += stickX * MOVE_SPEED * Time.deltaTime;
	}

	private void slow() {
		this.nextVelocity.x *= this.myFoot.IsOccupied() ? GROUND_SLOWDOWN_FACTOR : AIR_SLOWDOWN_FACTOR;
	}

	private void enforceTerminalVelocity() {
		if (Mathf.Abs(this.nextVelocity.x) > TERMINAL_VELOCITY_X) {
			this.nextVelocity.x = (this.nextVelocity.x > 0 ? 1 : -1) * TERMINAL_VELOCITY_X;
		}
		if (this.nextVelocity.y > TERMINAL_VELOCITY_Y) {
			this.nextVelocity.y = (this.nextVelocity.y > 0 ? 1 : -1) * TERMINAL_VELOCITY_Y;
		}
	}

	private Vector2 GetJumpVector() {
		/*
		Vector2 result = Vector2.zero;
		Collider2D[] touching = new Collider2D[10];
		if (Physics2D.GetContacts(this.myCollider, touching) <= 0) {
			return result;
		}
		for(int i = 0; i < touching.Length; i++) {
			if (touching[i] != null && touching[i].CompareTag("Tile")) {
				Vector3 diff = touching[i].transform.position - this.transform.position;
				result.x += diff.x;
				result.y += diff.y;
			}
		}
		return Vector2.ClampMagnitude(result * -1, 1.0f);
		*/
		Vector2 result = new Vector2(0, JUMP_VELOCITY);
		if (this.myFoot.IsOccupied()) {
			Debug.Log("Foot jump");
			this.myFoot.SensorUsed();
			return result;
		} else if (this.myLeft.IsOccupied()) {
			Debug.Log("Left wall jump");
			this.myLeft.SensorUsed();
			result.x = WALL_JUMP_HORIZONTAL_VELOCITY;
		} else if (this.myRight.IsOccupied()) {
			Debug.Log("Right wall jump");
			this.myRight.SensorUsed();
			result.x = -1 * WALL_JUMP_HORIZONTAL_VELOCITY;
		} else {
			return Vector2.zero;
		}
		return result;
	}

	public static bool CanJump() {
		//return Slicer.theSlicer.jumpFlag;
		return Slicer.theSlicer.myFoot.IsOccupied()
			|| Slicer.theSlicer.myLeft.IsOccupied()
			|| Slicer.theSlicer.myRight.IsOccupied();
	}

	public static bool CanSlice() {
		return Slicer.theSlicer.canSlice;
	}

	public static bool HoldingSlice() {
		return Slicer.theSlicer.holdingSlice;
	}

	public static void SensorActivated() {
		Slicer.theSlicer.jumpFlag = true;
	}

	public static void SensorDeactivated() {
		Slicer.theSlicer.jumpFlag = false;
	}

	/*
	public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Tile")) {
			this.jumpFlag = true;
		}
	}

	public void OnCollisionExit2D(Collision2D collision) {
		this.jumpFlag = false;
		if (collision.collider.CompareTag("Tile")) {
			Collider2D[] touching = new Collider2D[10];
			if (Physics2D.GetContacts(this.myCollider, touching) <= 0) {
				return;
			}
			for(int i = 0; i < touching.Length; i++) {
				if (touching[i] != null && touching[i].CompareTag("Tile")) {
					this.jumpFlag = true;
				}
			}
		}
	}
	*/

}
