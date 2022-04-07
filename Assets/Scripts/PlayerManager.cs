using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    private float hInput, vInput;
    private float moveSpeed = 10;
    private float rotateSpeed = 80f;
    private float jumpSpeed = 200f;
    private bool isGrounded;
    private bool isDead;
    private Rigidbody rb;
    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();
        if (SceneManager.GetActiveScene().buildIndex > 0) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().totalItems[SceneManager.GetActiveScene().buildIndex] = 
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().totalItems[SceneManager.GetActiveScene().buildIndex-1] + GameObject.FindGameObjectsWithTag("Item").Length;
        } else {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().totalItems[SceneManager.GetActiveScene().buildIndex] = GameObject.FindGameObjectsWithTag("Item").Length;
        }
    }

    void Update() {
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 rotation = Vector3.up * hInput;
        Quaternion angelRot = Quaternion.Euler(rotation * Time.deltaTime);

        rb.MovePosition(transform.position + (transform.forward * vInput * Time.deltaTime));
        rb.MoveRotation(rb.rotation * angelRot);
    
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayFX(3);
            rb.AddForce(Vector3.up * jumpSpeed);
            isGrounded = false;
        }

        if (this.transform.position.y < -10 && !isDead)  {
            isDead = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver();
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Item")) {
            GameObject.Find("GameManager").GetComponent<GameManager>().updateItemsCount();
            Destroy(col.gameObject);
        }
    }
}
