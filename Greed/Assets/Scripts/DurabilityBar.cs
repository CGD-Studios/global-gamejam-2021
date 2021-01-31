using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBar : MonoBehaviour
{

    public Slider slider;

    public void SetDurability(int durability)
    {
        slider.value = durability;
    }

    public void SetMaxDurability(int durability)
    {
        slider.maxValue = durability;
        slider.value = durability;
    }
}
