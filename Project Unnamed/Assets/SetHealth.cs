using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealth : MonoBehaviour
{
    public Slider slider;

    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void setHealth(float health)
    {
        while (health<slider.value)
        {
            slider.value -= 0.1f;
        }
      
    }
}
