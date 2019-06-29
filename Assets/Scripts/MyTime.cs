using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyTime {

	private static float timeScale = 1.0f;

	public static float DeltaTime() {
		return Time.deltaTime * MyTime.timeScale;
	}

	public static void SetTimeScale(float timeScale) {
		MyTime.timeScale = timeScale;
		if (MyTime.timeScale < 0) {
			MyTime.timeScale = 0;
		}
	}

	public static float GetTimeScale() {
		return MyTime.timeScale;
	}

}
