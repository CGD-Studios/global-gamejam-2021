using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDurability : MonoBehaviour
{

    public int maxDurability = 100;
    public int currentDurability;

    public DurabilityBar durabilityBar;
    public BaseWeapon storedWeapon;
    Image icon;

    // Start is called before the first frame update
    void Start()
    {
        /*currentDurability = maxDurability;
        durabilityBar.SetMaxDurability(maxDurability);*/
        icon = GetComponent<Image>();
        icon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }*/
        
    }

    public void TakeDamage(int damage)
    {
        currentDurability -= damage;

        durabilityBar.SetDurability(currentDurability);

        if (currentDurability <= 0) {
            GameController.ItemDestroyed();
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void InsertItem(BaseWeapon weapon) {
        storedWeapon = weapon;
        icon.sprite = storedWeapon.icon;
        maxDurability = weapon.durability;
        currentDurability = weapon.durability;
        durabilityBar.SetMaxDurability(maxDurability);
        icon.enabled = true;
    }
}
