using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        // 플레이어 오브젝트를 찾아서 GlobalControl 인스턴스에 저장
        if (playerObject != null)
        {
            GameManager.Instance.GameOverPlayer = playerObject;
            playerObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Town");
            //캐릭터 불러오기 필요

        }
    }
}
