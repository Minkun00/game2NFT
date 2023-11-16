using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    private BoxCollider2D bound;

    private MainCamera theCamera;

    private void Start()
    {
        theCamera = FindObjectOfType<MainCamera>();
        theCamera.SetBound(bound);

    }

}
