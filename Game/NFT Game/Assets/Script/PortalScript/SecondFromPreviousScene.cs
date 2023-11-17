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

        StartCoroutine(InitializePlayerMoveSecond());
    }

    IEnumerator InitializePlayerMoveSecond()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectSecond != null);

        GlobalControl.Instance.playerObjectSecond.SetActive(true);
        GlobalControl.Instance.playerObjectSecond = GameObject.FindWithTag("Player");

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>() != null);

        thePlayerSecond = GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>();

        // 나머지 코드...
        if ((moveMapSecond == thePlayerSecond.playerCurrentMapSecond) && (GlobalControl.Instance.CurrentPhase == previousScene))
        {
            theCameraSecond.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerSecond.transform.position = this.transform.position;
        }
    }
}
