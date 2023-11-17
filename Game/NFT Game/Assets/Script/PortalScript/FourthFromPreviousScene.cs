using System.Collections;
using UnityEngine;

public class FourthFromPreviousScene : MonoBehaviour
{
    public string moveMapFourth;

    private PlayerMove thePlayerFourth;
    private MainCamera theCameraFourth;

    void Start()
    {
        theCameraFourth = FindObjectOfType<MainCamera>();
        if (theCameraFourth == null)
        {
            Debug.LogError("MainCamera�� ã�� �� �����ϴ�.");
            return;
        }
        StartCoroutine(InitializePlayerMove());
    }

    IEnumerator InitializePlayerMove()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectFourth != null);
        if (GlobalControl.Instance.playerObjectFourth == null)
        {
            Debug.LogError("playerObjectFourth�� null�Դϴ�.");
            yield break;
        }
        GlobalControl.Instance.playerObjectFourth.SetActive(true);
        GlobalControl.Instance.playerObjectFourth = GameObject.FindWithTag("Player");
        if (GlobalControl.Instance.playerObjectFourth == null)
        {
            Debug.LogError("'Player' �±׸� ���� ���� ������Ʈ�� ã�� �� �����ϴ�.");
            yield break;
        }

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>() != null);
        if (GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>() == null)
        {
            Debug.LogError("PlayerMove ������Ʈ�� null�Դϴ�.");
            yield break;
        }
        thePlayerFourth = GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>();

        // ������ �ڵ�...
        if (moveMapFourth == thePlayerFourth.playerCurrentMapFourth)
        {
            theCameraFourth.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerFourth.transform.position = this.transform.position;
        }
        else
        {
            Debug.LogError("moveMapSecond�� playerCurrentMapSecond�� ��ġ���� �ʽ��ϴ�.");
            Debug.LogError("moveMapSecond : " + moveMapFourth);
            Debug.LogError("playerCurrentMapSecond : " + thePlayerFourth.playerCurrentMapFourth);
        }
    }
}
