using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : Sensor {

	public override void OnTriggerEnter2D(Collider2D collider) {
		base.OnTriggerEnter2D(collider);
	}

	public override void OnTriggerExit2D(Collider2D collider) {
		if (collider.CompareTag("Tile")) {
			this.blocked = false;
			base.OnTriggerExit2D(collider);
		}
	}

}
