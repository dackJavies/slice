using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditingScript : MonoBehaviour
{

    void Start() {
        Debug.Log("foo");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Foo");
        }
    }
}
