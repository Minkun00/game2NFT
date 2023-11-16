using System.Collections;
using UnityEngine;

public class FromPreviousScene : MonoBehaviour
{
    public string moveMap;

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    void Start()
    {
        theCamera = FindObjectOfType<MainCamera>();
        StartCoroutine(InitializePlayerMove());
    }

    IEnumerator InitializePlayerMove()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObject != null);
        GlobalControl.Instance.playerObject.SetActive(true);
        GlobalControl.Instance.playerObject = GameObject.FindWithTag("Player");

        yield return new WaitUntil(() => GlobalControl.Instance.playerObject.GetComponent<PlayerMove>() != null);
        thePlayer = GlobalControl.Instance.playerObject.GetComponent<PlayerMove>();

        // 나머지 코드...
        if (moveMap == thePlayer.playerCurrentMap)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayer.transform.position = this.transform.position;
        }
        else
        {
            Debug.Log("error");
        }
    }
}
