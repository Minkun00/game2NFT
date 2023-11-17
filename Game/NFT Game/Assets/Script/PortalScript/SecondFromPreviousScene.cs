using System.Collections;
using UnityEngine;

public class SecondFromPreviousScene : MonoBehaviour
{
    public string moveMapSecond;
    public int previousScene;

    private PlayerMove thePlayerSecond;
    private MainCamera theCameraSecond;

    void Start()
    {
        theCameraSecond = FindObjectOfType<MainCamera>();
        if (theCameraSecond == null)
        {
            Debug.LogError("MainCamera를 찾을 수 없습니다.");
            return;
        }
        StartCoroutine(InitializePlayerMoveSecond());
    }

    IEnumerator InitializePlayerMoveSecond()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectSecond != null);
        if (GlobalControl.Instance.playerObjectSecond == null)
        {
            Debug.LogError("playerObjectSecond가 null입니다.");
            yield break;
        }
        GlobalControl.Instance.playerObjectSecond.SetActive(true);
        GlobalControl.Instance.playerObjectSecond = GameObject.FindWithTag("Player");
        if (GlobalControl.Instance.playerObjectSecond == null)
        {
            Debug.LogError("'Player' 태그를 가진 게임 오브젝트를 찾을 수 없습니다.");
            yield break;
        }

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>() != null);
        if (GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>() == null)
        {
            Debug.LogError("PlayerMove 컴포넌트가 null입니다.");
            yield break;
        }
        thePlayerSecond = GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>();

        // 나머지 코드...
        if ((moveMapSecond == thePlayerSecond.playerCurrentMapSecond) && (GlobalControl.Instance.CurrentPhase == previousScene))
        {
            theCameraSecond.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerSecond.transform.position = this.transform.position;
        }
        else
        {
            Debug.LogError("moveMapSecond가 playerCurrentMapSecond와 일치하지 않습니다.");
            Debug.LogError("moveMapSecond : " + moveMapSecond);
            Debug.LogError("playerCurrentMapSecond : " + thePlayerSecond.playerCurrentMapSecond);
        }
    }
}
