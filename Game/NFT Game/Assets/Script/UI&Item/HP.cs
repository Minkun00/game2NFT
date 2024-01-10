using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Slider slider;
    PlayerMove Instance;

    void Awake()
    {
        Instance = GameObject.Find("Player").GetComponent<PlayerMove>();
        Instance.health = Instance.maxHealth;
    }

    void Update()
    {
        slider.value = Instance.sliderValue;
    }
}
