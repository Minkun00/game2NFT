using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FromPreviousScene : MonoBehaviour
{
    public string moveMap;
    public Vector3 Position;

    private PlayerMove thePlayer;
    private MainCamera theCamera;


    void Start()
    {
        thePlayer = FindAnyObjectByType<PlayerMove>();
        theCamera = FindAnyObjectByType<MainCamera>();

        if (moveMap == thePlayer.playerCurrentMap)
        {
            Vector3 CameraPoint = new Vector3(Position.x, Position.y, -10f);
            theCamera.transform.position = CameraPoint;
            thePlayer.transform.position = Position;
            Debug.Log("ok " + Position);
        }
        else
        {
            Debug.Log("error");
            Debug.Log("moveMap : " + moveMap + " / thePlayer.playerCurrentMap : " + thePlayer.playerCurrentMap);
        }
    }
}
