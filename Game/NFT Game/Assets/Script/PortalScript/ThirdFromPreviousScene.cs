using System.Collections;
using UnityEngine;

public class ThirdFromPreviousScene : MonoBehaviour
{
    public string moveMapThird;
    public int previousScene;


    private PlayerMove thePlayerThird;
    private MainCamera theCameraThird;

    void Start()
    {
        theCameraThird = FindObjectOfType<MainCamera>();
        if (theCameraThird == null)
        {
            Debug.LogError("MainCamera를 찾을 수 없습니다.");
            return;
        }
        StartCoroutine(InitializePlayerMoveThird());
    }

    IEnumerator InitializePlayerMoveThird()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectThird != null);
        if (GlobalControl.Instance.playerObjectThird == null)
        {
            Debug.LogError("playerObjectThird가 null입니다.");
            yield break;
        }
        GlobalControl.Instance.playerObjectThird.SetActive(true);
        GlobalControl.Instance.playerObjectThird = GameObject.FindWithTag("Player");
        if (GlobalControl.Instance.playerObjectThird == null)
        {
            Debug.LogError("'Player' 태그를 가진 게임 오브젝트를 찾을 수 없습니다.");
            yield break;
        }

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectThird.GetComponent<PlayerMove>() != null);
        if (GlobalControl.Instance.playerObjectThird.GetComponent<PlayerMove>() == null)
        {
            Debug.LogError("PlayerMove 컴포넌트가 null입니다.");
            yield break;
        }
        thePlayerThird = GlobalControl.Instance.playerObjectThird.GetComponent<PlayerMove>();

        // 나머지 코드...
        if ((moveMapThird == thePlayerThird.playerCurrentMapThird) && (GlobalControl.Instance.CurrentPhase == previousScene))
        {
            theCameraThird.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerThird.transform.position = this.transform.position;
        }
        else
        {
            Debug.LogError("moveMapThird가 playerCurrentMapThird와 일치하지 않습니다.");
            Debug.LogError("moveMapThird : " + moveMapThird);
            Debug.LogError("playerCurrentMapThird : " + thePlayerThird.playerCurrentMapThird);
        }
    }
}
