using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    
    public Animator animator;
    //for picking up weapon I guess
    public BoxCollider pickUpCollider;
    public BoxCollider damageCollider;

    //variables for the stats
    //make it public so designer can set the value
    public int durability = 20;
    public int damage = 10;
    public double scoreValue = 10;
    public int weaponType;

    //variable for attacking status
    bool isAttacking;
    float attackCooldown;

    int attackCount;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pickUpCollider = GetComponent<BoxCollider>();
        damageCollider = GetComponent<BoxCollider>();
        animator.SetInteger("WeaponType", weaponType);
        damageCollider.enabled = false;
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

        damageCollider.enabled = true;
    }

    //Animation event will call this function when the animation is done
    public void stopAttacking()
    {
        Debug.Log("Stop attacking");
        isAttacking = false;
        animator.SetBool("IsAttacking", false);

        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Started On trigger enter");

        if (isAttacking)
        {
            Debug.Log("Is Attackingg");

            if (other.tag == "Enemy")
            {
                Debug.Log("Found Enemy");
                //Damage Enemy
                Destroy(other);
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
        
    }

}
