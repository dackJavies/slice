using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBufferElement {

	private Move move;
	private float timestamp;
	private Vector2 stick;

	public InputBufferElement(Move move) {
		this.move = move;
		this.timestamp = Time.time;
		this.stick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	public Move GetMove() {
		return this.move;
	}

	public float GetTimestamp() {
		return this.timestamp;
	}

	public Vector2 GetStick() {
		return this.stick;
	}

}
