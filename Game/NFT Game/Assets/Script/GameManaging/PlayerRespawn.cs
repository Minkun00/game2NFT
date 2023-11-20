using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private PlayerMove thePlayer;
    private MainCamera theCamera;

    void Start()
    {
        theCamera = FindObjectOfType<MainCamera>();
        StartCoroutine(InitializePlayerRespawn());
    }

    IEnumerator InitializePlayerRespawn()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.GameOverPlayer != null);
        GlobalControl.Instance.GameOverPlayer.SetActive(true);
        GlobalControl.Instance.GameOverPlayer = GameObject.FindWithTag("Player");

        yield return new WaitUntil(() => GlobalControl.Instance.GameOverPlayer.GetComponent<PlayerMove>() != null);
        thePlayer = GlobalControl.Instance.GameOverPlayer.GetComponent<PlayerMove>();
        
        theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
        thePlayer.transform.position = this.transform.position;
    }
}
