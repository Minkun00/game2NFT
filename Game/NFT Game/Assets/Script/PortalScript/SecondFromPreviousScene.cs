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
            Debug.LogError("MainCamera�� ã�� �� �����ϴ�.");
            return;
        }
        StartCoroutine(InitializePlayerMoveSecond());
    }

    IEnumerator InitializePlayerMoveSecond()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectSecond != null);
        if (GlobalControl.Instance.playerObjectSecond == null)
        {
            Debug.LogError("playerObjectSecond�� null�Դϴ�.");
            yield break;
        }
        GlobalControl.Instance.playerObjectSecond.SetActive(true);
        GlobalControl.Instance.playerObjectSecond = GameObject.FindWithTag("Player");
        if (GlobalControl.Instance.playerObjectSecond == null)
        {
            Debug.LogError("'Player' �±׸� ���� ���� ������Ʈ�� ã�� �� �����ϴ�.");
            yield break;
        }

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>() != null);
        if (GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>() == null)
        {
            Debug.LogError("PlayerMove ������Ʈ�� null�Դϴ�.");
            yield break;
        }
        thePlayerSecond = GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>();

        // ������ �ڵ�...
        if ((moveMapSecond == thePlayerSecond.playerCurrentMapSecond) && (GlobalControl.Instance.CurrentPhase == previousScene))
        {
            theCameraSecond.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerSecond.transform.position = this.transform.position;
        }
        else
        {
            Debug.LogError("moveMapSecond�� playerCurrentMapSecond�� ��ġ���� �ʽ��ϴ�.");
            Debug.LogError("moveMapSecond : " + moveMapSecond);
            Debug.LogError("playerCurrentMapSecond : " + thePlayerSecond.playerCurrentMapSecond);
        }
    }
}
