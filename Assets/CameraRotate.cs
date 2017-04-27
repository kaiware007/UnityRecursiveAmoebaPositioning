using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public Vector3 target;
    public Vector3 axis = Vector3.up;
    public float rotateSpeed = 100;

	// Update is called once per frame
	void Update () {
        transform.RotateAround(target, axis, rotateSpeed * Time.deltaTime);
	}
}
