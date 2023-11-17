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
            Debug.LogError("MainCamera�� ã�� �� �����ϴ�.");
            return;
        }
        StartCoroutine(InitializePlayerMoveThird());
    }

    IEnumerator InitializePlayerMoveThird()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectThird != null);
        if (GlobalControl.Instance.playerObjectThird == null)
        {
            Debug.LogError("playerObjectThird�� null�Դϴ�.");
            yield break;
        }
        GlobalControl.Instance.playerObjectThird.SetActive(true);
        GlobalControl.Instance.playerObjectThird = GameObject.FindWithTag("Player");
        if (GlobalControl.Instance.playerObjectThird == null)
        {
            Debug.LogError("'Player' �±׸� ���� ���� ������Ʈ�� ã�� �� �����ϴ�.");
            yield break;
        }

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectThird.GetComponent<PlayerMove>() != null);
        if (GlobalControl.Instance.playerObjectThird.GetComponent<PlayerMove>() == null)
        {
            Debug.LogError("PlayerMove ������Ʈ�� null�Դϴ�.");
            yield break;
        }
        thePlayerThird = GlobalControl.Instance.playerObjectThird.GetComponent<PlayerMove>();

        // ������ �ڵ�...
        if ((moveMapThird == thePlayerThird.playerCurrentMapThird) && (GlobalControl.Instance.CurrentPhase == previousScene))
        {
            theCameraThird.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerThird.transform.position = this.transform.position;
        }
        else
        {
            Debug.LogError("moveMapThird�� playerCurrentMapThird�� ��ġ���� �ʽ��ϴ�.");
            Debug.LogError("moveMapThird : " + moveMapThird);
            Debug.LogError("playerCurrentMapThird : " + thePlayerThird.playerCurrentMapThird);
        }
    }
}
