using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    
    public Animator animator;
    //for picking up weapon I guess
    BoxCollider pickUpCollider;
    BoxCollider damageCollider;

    //variables for the stats
    //make it public so designer can set the value
    public int durability = 20;
    public int damage = 10;
    public double scoreValue = 10;
    public int weaponType;

    //variable for attacking status
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pickUpCollider = GetComponent<BoxCollider>();
        damageCollider = GetComponent<BoxCollider>();
        animator.SetInteger("WeaponType", weaponType);
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
        animator.SetBool("IsAttacking", true);
    }

    public void stopAttacking()
    {
        Debug.Log("Stop attacking");
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Started");

        if (isAttacking)
        {
            if (collision.collider.tag == "Enemy")
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
