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
    [SerializeField] GameObject activeWeapon;

    //Weapon
    //public BaseWeapon weapon;
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
        } else if (movement.magnitude > 0) SoundManager.PlayWalk();

        // Attack input
        if (Input.GetButtonDown("Attack")) {
            if (inventory[selectedInventory]) {
                if (!isAttacking || !isSwitching) {
                    inventory[selectedInventory].startAttacking();
                    SoundManager.PlayAttack(inventory[selectedInventory].weaponType);
                }
            }
        }

        // Interact input
        if (Input.GetButtonDown("Interact")) {
            print("interacting");
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out itemRayHit, itemRange, LayerMask.GetMask("Weapon"))) {
                print($"You interacted with {itemRayHit.transform.name}");
                if (weaponStored == inventorySize) {
                    Destroy(inventory[selectedInventory].transform.parent.gameObject);
                    inventory[selectedInventory] = itemRayHit.transform.GetComponent<BaseWeapon>();
                    inventory[selectedInventory].transform.parent.position = activeWeapon.transform.position;
                    inventory[selectedInventory].transform.parent.rotation = cam.transform.rotation;
                    inventory[selectedInventory].transform.parent.SetParent(activeWeapon.transform);
                    GameController.InsertItem(inventory[selectedInventory], selectedInventory);
                } else {
                    for (int i = 0; i < inventorySize; i++) {
                        if (!inventory[i]) {
                            inventory[i] = itemRayHit.transform.GetComponent<BaseWeapon>();
                            inventory[i].transform.parent.gameObject.SetActive(false);
                            GameController.InsertItem(inventory[i], i);
                            weaponStored++;
                            if (weaponStored == 1) {
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
                itemRayHit.transform.GetComponent<BaseWeapon>().PickedUp();
            } else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out itemRayHit, itemRange, LayerMask.GetMask("Key"))) {
                Destroy(itemRayHit.transform.gameObject);
                GameController.StartTimer();
                SoundManager.PlayClockSlow();
            }
        }

        // Inventory hotbar inputs
        if (Input.GetButtonDown("Inv1")) {
            if ((selectedInventory != 0 && !inventory[selectedInventory].GetIsAttacking()) || !inventory[selectedInventory]) SelectWeapon(0);
        }
        if (Input.GetButtonDown("Inv2")) {
            print(inventory[selectedInventory]);
            if ((selectedInventory != 1 && !inventory[selectedInventory].GetIsAttacking()) || !inventory[selectedInventory]) SelectWeapon(1);
        }
        if (Input.GetButtonDown("Inv3")) {
            if ((selectedInventory != 2 && !inventory[selectedInventory].GetIsAttacking()) || !inventory[selectedInventory]) SelectWeapon(2);
        }
        if (Input.GetButtonDown("Inv4")) {
            if ((selectedInventory != 3 && !inventory[selectedInventory].GetIsAttacking()) || !inventory[selectedInventory]) SelectWeapon(3);
        }
        if (Input.GetButtonDown("Inv5")) {
            if ((selectedInventory != 4 && !inventory[selectedInventory].GetIsAttacking()) || !inventory[selectedInventory]) SelectWeapon(4);
        } //TODO Add more or remove inventory?
    }

    private void FixedUpdate() {
        if (Physics.OverlapSphere(transform.position, 7f, LayerMask.GetMask("Enemy")).Length > 0) {
            SoundManager.PlayFight();
        } else SoundManager.OutOfCombat();
    }

    // Selects weapon from your inventory
    void SelectWeapon(int invSlot) {

        if (inventory[invSlot]) { // If not empty
            if (inventory[selectedInventory]) { // If item not destroyed
                inventory[selectedInventory].transform.parent.gameObject.SetActive(false);
                inventory[selectedInventory].transform.parent.SetParent(null);
            }
            selectedInventory = invSlot;
            inventory[selectedInventory].transform.parent.gameObject.SetActive(true);
            inventory[selectedInventory].transform.parent.position = activeWeapon.transform.position;
            inventory[selectedInventory].transform.parent.rotation = cam.transform.rotation;
            inventory[selectedInventory].transform.parent.SetParent(activeWeapon.transform);
        }
    }

    // Call when a weapon breaks (maybe get called by a Game Manager/Controller)
    public void WeaponBreak() {
        inventory[selectedInventory].StopAnimation();
        Destroy(inventory[selectedInventory].transform.parent.gameObject);
        weaponStored--;
    }

    public void TakeDamage(int damage) {
        GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    private void OnDrawGizmosSelected() {
        /*Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, 0.1f);*/
    }
}
