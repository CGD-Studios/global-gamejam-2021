using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDurability : MonoBehaviour
{

    public int maxDurability = 100;
    public int currentDurability;

    public DurabilityBar durabilityBar;

    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability;
        durabilityBar.SetMaxDurability(maxDurability);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }
        if (currentDurability <= 0)
        {
            Destroy(gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        currentDurability -= damage;

        durabilityBar.SetDurability(currentDurability);
    }
}
