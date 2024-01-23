using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public bool equipmentUIactive = false;

    [SerializeField]
    private GameObject equipBase;

    private void Update()
    {
        TryOpenEquipment();
    }

    private void TryOpenEquipment()
    {
        if (Input.GetKeyDown(KeyCode.E) && !equipmentUIactive)
        {
            equipmentUIactive = true;
            OpenEquipment();
        }
        else if (Input.GetKeyDown(KeyCode.E) && equipmentUIactive)
        {
            equipmentUIactive = false;
            CloseEquipment();
        }
    }

    private void OpenEquipment()
    {
        equipBase.SetActive(true);
    }

    private void CloseEquipment()
    {
        equipBase.SetActive(false);
    }
}
