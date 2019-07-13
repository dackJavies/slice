using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private CircleCollider2D myCollider;

	private Vector2 nextVelocity;

	public static Slicer theSlicer;
	private bool canSlice;
	private bool holdingSlice;
	private bool canJump;

	private const float JUMP_VELOCITY = 14.0f;
	private const float GRAVITY_ACCEL = 25.0f;
	private const float MOVE_SPEED = 14.0f;
	private const float GROUND_SLOWDOWN_FACTOR = 0.4f;
	private const float AIR_SLOWDOWN_FACTOR = 0.8f;
	private const float WALL_JUMP_HORIZONTAL_VELOCITY = 8.0f;

	private const float TERMINAL_VELOCITY_X = 7.0f;
	private const float TERMINAL_VELOCITY_Y = 25.0f;

	private MyInputManager myInputManager;

	// Use this for initialization
	void Start () {

		this.myRigidbody = this.GetComponent<Rigidbody2D>();
		this.myCollider = this.GetComponent<CircleCollider2D>();
		this.myInputManager = this.GetComponent<MyInputManager>();

		this.nextVelocity = Vector2.zero;

		this.canSlice = true;
		this.holdingSlice = false;
		this.canJump = false;

		Slicer.theSlicer = this;

	}

	// Update is called once per frame
	void Update () {

		this.nextVelocity = this.myRigidbody.velocity;

		this.processInputBuffer();
		this.enforceTerminalVelocity();
		this.fall();

		this.myRigidbody.velocity = this.nextVelocity * Time.timeScale;

	}

	private void fall() {
		this.nextVelocity.y -= GRAVITY_ACCEL * Time.deltaTime;
	}

	private void processInputBuffer() {
		if (this.myInputManager.HasNext()) {
			InputBufferElement next = this.myInputManager.GetNext();
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
		if (this.canJump) {
			this.nextVelocity = this.GetJumpVector();
			this.canJump = false;
		}
	}

	private void processHoldSlice() {

	}

	private void processReleaseSlice() {

	}

	private void processMove(float stickX) {
		nextVelocity.x += stickX * MOVE_SPEED * Time.deltaTime;
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
		return Vector2.up * JUMP_VELOCITY;
	}

	public static bool CanJump() {
		return Slicer.theSlicer.canJump;
	}

	public static bool CanSlice() {
		return Slicer.theSlicer.canSlice;
	}

	public static bool HoldingSlice() {
		return Slicer.theSlicer.holdingSlice;
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Tile") && this.belowMe(collision.collider.transform)) {
			this.canJump = true;
		}
	}

	private bool belowMe(Transform t) {

		float myBottomEdge = this.transform.position.y - (this.transform.localScale.y / 2);
		float theirTopEdge = t.transform.position.y + (t.localScale.y / 2);

		float diff = Mathf.Abs(myBottomEdge - theirTopEdge);

		return diff <= 0.2f;

	}

}
