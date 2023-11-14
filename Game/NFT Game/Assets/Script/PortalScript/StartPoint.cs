using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public Vector3 startPoint;
    public string arriveMap;
    private PlayerMove thePlayer;
    private Camera theCamera;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
        theCamera = FindAnyObjectByType<Camera>();
        
        if (arriveMap == thePlayer.currentMapName)
        {
            theCamera.transform.position = startPoint;
            thePlayer.transform.position = startPoint;
        }
    }

    void Update()
    {
        
    }
}
