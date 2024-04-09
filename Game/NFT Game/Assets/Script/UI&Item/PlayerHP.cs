using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Slider slider;
    PlayerMove Instance;

    public static HP instance;

    void Awake()
    {
        Instance = GameObject.Find("Player").GetComponent<PlayerMove>();
        Instance.health = Instance.maxHealth;

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        slider.value = Instance.sliderValue;
    }
}
