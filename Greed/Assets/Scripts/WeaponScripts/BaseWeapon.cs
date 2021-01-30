using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    Rigidbody rigidBody;
    Animator attackAnimation;
    Animator switchingOutAnimation;
    Animator switchingInAnimation;
    BoxCollider boxCollider;

    //variables for the stats
    //make it public so designer can set the value
    public int durability = 20;
    public int damage = 10;
    public double scoreValue = 10;

    //variable for attacking status
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        attackAnimation = GetComponent<Animator>();
        switchingInAnimation = GetComponent<Animator>();
        switchingOutAnimation = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void startAttacking()
    {
        if (isAttacking)
        {
            //prevent repeating motion
            return;
        }
        //tbh maybe move this bool to PlayerController instead
        isAttacking = true; 
        //starting animation
        attackAnimation.Play("Base Layer.");
    }

    public void stopAttacking()
    {
        isAttacking = false;
    }

    public void switchOutWeapon()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Started");

        if (isAttacking)
        {
            if (collision.collider.tag == "Melee")
            {
                //Damage Enemy
                durability -= 1;

                //Maybe move this to player controller instead
                if (durability == 0)
                {
                    weaponBreak();
                }
            }
        }
    }

    //Maybe handles weapon break logic in player controller instead
    //need to remove from inventory after all
    private void weaponBreak()
    {
        Destroy(this);
    }

}
