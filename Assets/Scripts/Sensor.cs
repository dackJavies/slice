using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

	protected BoxCollider2D myCollider;
	protected bool occupied;
	protected bool blocked;

	// Use this for initialization
	void Start () {
		this.myCollider = this.GetComponent<BoxCollider2D>();
		this.occupied = false;
		this.blocked = false;
	}

	public bool IsOccupied() {
		return !this.blocked && this.occupied;
	}

	public virtual void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Tile")) {
			if (!this.blocked) {
				this.occupied = true;
				Slicer.SensorActivated();
			}
		}
	}

	public virtual void OnTriggerExit2D(Collider2D collider) {
		if (collider.CompareTag("Tile")) {
			bool hasRemainingContacts = Physics2D.GetContacts(this.myCollider, new ContactPoint2D[5]) > 0;
			if (blocked) {
				this.blocked = hasRemainingContacts;
			} else {
				this.occupied = hasRemainingContacts;
			}
		}
	}

	public void SensorUsed() {
		this.blocked = true;
	}
	
}
