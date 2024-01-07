using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Slider slider;
    float maxHealth = 100f;
    public static float health;

    void Awake()
    {
        health = maxHealth;
    }

    void Update()
    {
        slider.value = health / maxHealth;
        slider.value = 0f;
    }
}
