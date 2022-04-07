using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public GameObject target;
    private Vector3 camOffset;
    void Start() {
        camOffset = new Vector3(0, 10f, -10f);
    }

    void Update() {
        this.transform.position = target.transform.TransformPoint(camOffset);
        transform.LookAt(target.transform);
    }
}
