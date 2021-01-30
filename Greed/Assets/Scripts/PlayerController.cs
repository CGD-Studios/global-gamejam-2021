using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;

    // Variables for movement
    Vector3 movement;
    [SerializeField] float speed;

    //TODO weapon array/inventory

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        // Sets the movement vector with input
        movement = (transform.right * Input.GetAxisRaw("Horizontal")) + (transform.forward * Input.GetAxisRaw("Vertical"));
        /*
        // Attack input
        if (Input.GetButtonDown("Attack")) {
            //TODO attack
        }
        
        // Interact input
        if (Input.GetButtonDown("Interact")) {
            //TODO interact/grab weapon
        }

        // Inventory hotbar inputs
        if (Input.GetButtonDown("Inv1")) {
            //TODO select weapon if not empty
        }
        if (Input.GetButtonDown("Inv2")) {

        }
        if (Input.GetButtonDown("Inv3")) {

        }
        if (Input.GetButtonDown("Inv4")) {

        }
        if (Input.GetButtonDown("Inv5")) {

        } //TODO Add more or delete inventory?
        */
    }

    void FixedUpdate() {
        // Moves the player
        rb.MovePosition(rb.position + (movement * speed * Time.fixedDeltaTime));


    }
}
