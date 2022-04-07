using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour {
    public GameObject[] waypoints;
    private int index = 0;
    private float speed = 2f;

    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (Vector3.Distance(waypoints[index].transform.position, this.transform.position) < 2f ) {
            index++;
            if (index >= waypoints.Length){
                index=0;
            }
        }
        transform.position = Vector3.MoveTowards(this.transform.position, waypoints[index].transform.position, speed * Time.deltaTime);
    }

    void OnCollisionStay (Collision col) {
        if (col.gameObject.transform.CompareTag("Player")) {
            //col.gameObject.transform.SetParent(this.transform);
            player.transform.position = Vector3.MoveTowards(player.transform.position, waypoints[index].transform.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionExit (Collision col) {
        if (col.gameObject.transform.CompareTag("Player")) {
            //col.gameObject.transform.SetParent(null);
        }
    }
}
