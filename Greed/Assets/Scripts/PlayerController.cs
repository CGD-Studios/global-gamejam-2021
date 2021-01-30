using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController player;

    // Variables for movement
    Vector3 movement;
    [SerializeField] float speed;
    [SerializeField] Transform groundCheck; //Potentially make it an array
    bool grounded;

    // Variables for item interaction
    Camera cam;
    [SerializeField] float itemRange;
    RaycastHit itemRayHit;

    // Inventory
    public GameObject[] inventory; //Maybe change GameObject to item class - DO NOT FORGET remove "public"
    int inventorySize = 5;
    int selectedInventory = 0;
    int weaponStored = 0;
    [SerializeField] GameObject activeWeapon;

    //Weapon
    BaseWeapon weapon;
    bool isAttacking;
    bool isSwitching;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        inventory = new GameObject[inventorySize];
    }

    // Update is called once per frame
    void Update() {
        // Checks if grounded
        grounded = Physics.OverlapSphere(groundCheck.position, 0.1f, LayerMask.GetMask("Ground")).Length > 0;

        // Sets the movement vector with input
        movement = ((transform.right * Input.GetAxisRaw("Horizontal")) + (transform.forward * Input.GetAxisRaw("Vertical"))).normalized;

        // Move the player and apply gravity when not grounded
        player.Move(movement * speed * Time.deltaTime);
        if (!grounded) {
            player.Move(Vector3.up * -9.81f * Time.deltaTime);
        }

        // Attack input
        if (Input.GetButtonDown("Attack")) {

            if (!isAttacking || !isSwitching)
            {
                weapon.startAttacking();
            }
            //TODO attack
        }
        
        // Interact input
        if (Input.GetButtonDown("Interact")) {
            print("interacting");
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out itemRayHit, itemRange, LayerMask.GetMask("Weapon"))) {
                print($"You interacted with {itemRayHit.transform.name}");
                if (weaponStored == inventorySize) {
                    Destroy(inventory[selectedInventory]);
                    inventory[selectedInventory] = itemRayHit.transform.gameObject;
                    inventory[selectedInventory].transform.position = activeWeapon.transform.position;
                    inventory[selectedInventory].transform.rotation = cam.transform.rotation;
                    inventory[selectedInventory].transform.SetParent(activeWeapon.transform);
                } else {
                    for (int i = 0; i < inventorySize; i++) {
                        if (!inventory[i]) {
                            inventory[i] = itemRayHit.transform.gameObject;
                            inventory[i].SetActive(false);
                            weaponStored++;
                            if (i == 0) {
                                SelectWeapon(i); // Automatically selects the weapon if its the only one
                            }
                            break;
                        }
                    }
                }
            }
            else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out itemRayHit, itemRange, LayerMask.GetMask("Key"))) {
                Destroy(itemRayHit.transform.gameObject);
                // Start Timer
            }
        }

        // Inventory hotbar inputs
        if (Input.GetButtonDown("Inv1")) {
            SelectWeapon(0);
        }
        if (Input.GetButtonDown("Inv2")) {
            SelectWeapon(1);
        }
        if (Input.GetButtonDown("Inv3")) {
            SelectWeapon(2);
        }
        if (Input.GetButtonDown("Inv4")) {
            SelectWeapon(3);
        }
        if (Input.GetButtonDown("Inv5")) {
            SelectWeapon(4);
        } //TODO Add more or remove inventory?
    }

    // Selects weapon from your inventory
    void SelectWeapon(int invSlot) {
        if (inventory[invSlot]) { // If not empty
            inventory[selectedInventory].SetActive(false);
            inventory[selectedInventory].transform.SetParent(null);
            selectedInventory = invSlot;
            inventory[selectedInventory].SetActive(true);
            inventory[selectedInventory].transform.position = activeWeapon.transform.position;
            inventory[selectedInventory].transform.rotation = cam.transform.rotation;
            inventory[selectedInventory].transform.SetParent(activeWeapon.transform);
        }
    }

    // Call when a weapon breaks (maybe get called by a Game Manager/Controller)
    public void WeaponBreak() {
        Destroy(inventory[selectedInventory]);
        weaponStored--;
    }

    private void OnDrawGizmosSelected() {
        /*Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, 0.1f);*/
    }
}
