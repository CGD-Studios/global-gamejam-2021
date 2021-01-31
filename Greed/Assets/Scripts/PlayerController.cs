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
    public BaseWeapon[] inventory; //Maybe change GameObject to item class - DO NOT FORGET remove "public"
    int inventorySize = 5;
    int selectedInventory = 0;
    int weaponStored = 0;
    [SerializeField] BaseWeapon activeWeapon;

    //Weapon
    public BaseWeapon weapon;
    bool isAttacking;
    bool isSwitching;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        inventory = new BaseWeapon[inventorySize];

        isAttacking = false;
        isSwitching = false;
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

            if (activeWeapon)
            {
                if (!isAttacking || !isSwitching)
                {
                    activeWeapon.startAttacking();
                }
            }
        }
        
        // Interact input
        if (Input.GetButtonDown("Interact")) {
            print("interacting");
            BaseWeapon selectionWeapon;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out itemRayHit, itemRange, LayerMask.GetMask("Weapon"))) {
                print($"You interacted with {itemRayHit.transform.name}");
                if (weaponStored == inventorySize) {
                    Destroy(inventory[selectedInventory]);

                    selectionWeapon = itemRayHit.transform.GetComponent<BaseWeapon>();

                    if (selectionWeapon)
                    {
                        inventory[selectedInventory] = selectionWeapon;
                        inventory[selectedInventory].transform.position = activeWeapon.transform.position;
                        inventory[selectedInventory].transform.rotation = cam.transform.rotation;
                        inventory[selectedInventory].transform.SetParent(activeWeapon.transform);
                    }
                } else {
                    selectionWeapon = itemRayHit.transform.GetComponent<BaseWeapon>();
                    for (int i = 0; i < inventorySize; i++) {
                        if (!inventory[i])
                        {
                            if (selectionWeapon)
                            {
                                inventory[i] = selectionWeapon;
                                inventory[i].gameObject.SetActive(false);
                                weaponStored++;
                                if (i == 0)
                                {
                                    //Might need if somehow player were able to bypass the getting the initial weapon.
                                    //if (!activeWeapon)
                                    //{
                                    //    activeWeapon = selectionWeapon;
                                    //}

                                    SelectWeapon(i); // Automatically selects the weapon if its the only one
                                }
                                break;
                            }
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
            inventory[selectedInventory].gameObject.SetActive(false);
            inventory[selectedInventory].transform.SetParent(null);
            selectedInventory = invSlot;
            inventory[selectedInventory].gameObject.SetActive(true);
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
