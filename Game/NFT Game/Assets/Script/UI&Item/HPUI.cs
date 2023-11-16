/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Slider hpbar;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            Debug.Log("ouch!");
            hpbar.value -= 0.1f;
        }
    }

    private void HandleHp()
    {
        hpbar.value = (float)curHp / (float)maxHp;
    }
}
*/
