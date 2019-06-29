using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour {

	private InputBuffer buffer;
	private Move lastAdded;

	// Use this for initialization
	void Start () {

		this.buffer = new InputBuffer();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Slicer.CanJump() && Input.GetAxis("Jump") > 0) {
			this.buffer.Add(Move.JUMP);
		}

		if (Slicer.CanSlice() && !Slicer.HoldingSlice() && Input.GetAxis("Slice") > 0) {
			this.buffer.Add(Move.HOLD_SLICE);
		}
		
		if (Slicer.CanSlice() && Slicer.HoldingSlice() && Input.GetAxis("Slice") <= 0) {
			this.buffer.Add(Move.RELEASE_SLICE);
		}

		//if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			this.buffer.Add(Move.MOVE);
		//}

		//Debug.Log(this.getBufferAsString());

	}

	public bool HasNext() {
		return this.buffer.HasNext();
	}

	public InputBufferElement GetNext() {
		return this.buffer.GetNext();
	}

	private string getBufferAsString() {
		return this.buffer.GetBufferAsString();
	}

}
