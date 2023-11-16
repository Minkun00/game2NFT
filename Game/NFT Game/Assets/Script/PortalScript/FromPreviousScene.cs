using UnityEngine;

public class FromPreviousScene : MonoBehaviour
{
    public string moveMap;

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    void Start()
    {
        theCamera = FindAnyObjectByType<MainCamera>();

        // GlobalControl 인스턴스의 playerObject를 활성화
        if (GlobalControl.Instance.playerObject != null)
        {
            GlobalControl.Instance.playerObject.SetActive(true);
            GlobalControl.Instance.playerObject = GameObject.FindWithTag("Player");

            thePlayer = GlobalControl.Instance.playerObject.GetComponent<PlayerMove>();

        }
        else
        {
            Debug.LogError("PlayerMove not found!");
        }

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
