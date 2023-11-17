using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public int CurrentPhase;
    public string loadingSceneName;

    public GameObject playerObject;
    public GameObject playerObjectSecond;
    public GameObject playerObjectThird;
    public GameObject playerObjectFourth;


    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

