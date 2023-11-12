using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Road2ToHidden : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 포탈에 도달하면 Hidden 씬으로 이동
            SceneManager.LoadScene("Hidden");
        }
    }
}
