using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour
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
    }
}
