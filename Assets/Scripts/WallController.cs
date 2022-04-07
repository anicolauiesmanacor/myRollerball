using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour{
    public GameObject waypoint;
    private float speed = 1f;
    private bool isDown = false;
    private GameManager gm;
    void Start(){
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        if (gm.itemCount >= 3 && !isDown) {
            if (Vector3.Distance(this.transform.position, waypoint.transform.position) > 0.1f) {
                this.transform.position = Vector3.MoveTowards(this.transform.position, waypoint.transform.position, speed * Time.deltaTime);
            } else {
                isDown = true;
            }
        }
    }
}
