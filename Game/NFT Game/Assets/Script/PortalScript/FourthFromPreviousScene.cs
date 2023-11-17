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
        
        StartCoroutine(InitializePlayerMoveFourth());
    }

    IEnumerator InitializePlayerMoveFourth()
    {
        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectFourth != null);
        
        GlobalControl.Instance.playerObjectFourth.SetActive(true);
        GlobalControl.Instance.playerObjectFourth = GameObject.FindWithTag("Player");
        

        yield return new WaitUntil(() => GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>() != null);
        
        thePlayerFourth = GlobalControl.Instance.playerObjectFourth.GetComponent<PlayerMove>();

        // 나머지 코드...
        if ((moveMapFourth == thePlayerFourth.playerCurrentMapFourth) && (GlobalControl.Instance.CurrentPhase == previousScene))
        {
            theCameraFourth.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerFourth.transform.position = this.transform.position;
        }
       
    }
}
