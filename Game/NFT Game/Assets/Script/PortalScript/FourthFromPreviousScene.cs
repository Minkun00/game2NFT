using System.Collections;
using UnityEngine;

public class FourthFromPreviousScene : MonoBehaviour
{
    public string moveMapFourth;
    public int previousScene;

    private PlayerMove thePlayerFourth;
    private MainCamera theCameraFourth;

    void Start()
    {
        theCameraFourth = FindObjectOfType<MainCamera>();
        if (theCameraFourth == null)
        {
            Debug.LogError("MainCamera를 찾을 수 없습니다.");
            return;
        }
        StartCoroutine(InitializePlayerMoveFourth());
    }

    IEnumerator InitializePlayerMoveFourth()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectFourth != null);
        if (GlobalControl.Instance.playerObjectFourth == null)
        {
            Debug.LogError("playerObjectFourth가 null입니다.");
            yield break;
        }
        GlobalControl.Instance.playerObjectFourth.SetActive(true);
        GlobalControl.Instance.playerObjectFourth = GameObject.FindWithTag("Player");
        if (GlobalControl.Instance.playerObjectFourth == null)
        {
            Debug.LogError("'Player' 태그를 가진 게임 오브젝트를 찾을 수 없습니다.");
            yield break;
        }

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>() != null);
        if (GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>() == null)
        {
            Debug.LogError("PlayerMove 컴포넌트가 null입니다.");
            yield break;
        }
        thePlayerFourth = GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>();

        // 나머지 코드...
        if ((moveMapFourth == thePlayerFourth.playerCurrentMapFourth) && (GlobalControl.Instance.CurrentPhase == previousScene))
        {
            theCameraFourth.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerFourth.transform.position = this.transform.position;
        }
        else
        {
            Debug.LogError("moveMapFourth가 playerCurrentMapFourth와 일치하지 않습니다.");
            Debug.LogError("moveMapFourth : " + moveMapFourth);
            Debug.LogError("playerCurrentMapFourth : " + thePlayerFourth.playerCurrentMapFourth);
        }
    }
}
