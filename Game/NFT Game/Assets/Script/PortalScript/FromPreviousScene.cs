using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FromPreviousScene : MonoBehaviour
{
    public string moveMap;

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    void Start()
    {
        thePlayer = FindAnyObjectByType<PlayerMove>();
        theCamera = FindAnyObjectByType<MainCamera>();

        if (moveMap == thePlayer.playerCurrentMap)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayer.transform.position = this.transform.position;
            Debug.Log("ok " + this.transform.position);
        }
        else
        {
            Debug.Log("error");
            Debug.Log("moveMap : " + moveMap + " / thePlayer.playerCurrentMap : " + thePlayer.playerCurrentMap);
        }
    }
}
