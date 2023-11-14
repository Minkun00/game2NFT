using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromPreviousScene : MonoBehaviour
{
    public Vector3 playerArrivedPoint;
    public string arriveMap;
    public string previousMap;

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    void Start()
    {
        thePlayer = FindAnyObjectByType<PlayerMove>();
        theCamera = FindAnyObjectByType<MainCamera>();


        if ((arriveMap == thePlayer.playerCurrentMap) && (previousMap == thePlayer.playerPreviousMape))
        {
            Vector3 CameraPoint = new Vector3(playerArrivedPoint.x, playerArrivedPoint.y, -10f);
            theCamera.transform.position = CameraPoint;
            thePlayer.transform.position = playerArrivedPoint;
        }
        else
        {
            Debug.Log("else arriveMap: " + arriveMap + ", thePlayer.currentMapName: " + thePlayer.playerCurrentMap);
            Debug.Log("else previousMap: " + previousMap + ", thePlayer.previousMapName: " + thePlayer.playerPreviousMape);
            Debug.Log("else playerArrivedPoint : " + playerArrivedPoint);
        }
    }
}
