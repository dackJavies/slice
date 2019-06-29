using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer {

	private Queue<InputBufferElement> buffer;

	private const float ELEMENT_EXPIRATION_TIME = 0.3f;

	public InputBuffer() {
		buffer = new Queue<InputBufferElement>();
	}

	public void Add(Move move) {
		this.buffer.Enqueue(new InputBufferElement(move));
	}

	public InputBufferElement GetNext() {
		if (!this.HasNext()) {
			return null;
		}
		while(this.HasNext()) {
			if (Time.time - this.buffer.Peek().GetTimestamp() < ELEMENT_EXPIRATION_TIME) {
				return this.buffer.Dequeue();
			}
			this.buffer.Dequeue();
		}
		return null;
	}

	public bool HasNext() {
		return this.buffer != null && this.buffer.Count > 0;
	}

	public string GetBufferAsString() {
		string result = "[";
		InputBufferElement[] arr = this.buffer.ToArray();
		for(int i = 0; i < arr.Length; i++) {
			if (i > 0) {
				result += ",";
			}
			result += " " + arr[i].GetMove();
		}
		result += " ]";
		return result;
	}

}
