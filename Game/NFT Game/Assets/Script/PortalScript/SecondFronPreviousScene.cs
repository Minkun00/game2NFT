using UnityEngine;

public class SecondFromPreviousScene : MonoBehaviour
{
    public string moveMapSecond;

    private PlayerMove thePlayerSecond;
    private MainCamera theCameraSecond;

    void Start()
    {
        theCameraSecond = FindObjectOfType<MainCamera>();

        // GlobalControl �ν��Ͻ��� playerObjectSecond�� Ȱ��ȭ
        if (GlobalControl.Instance.playerObjectSecond != null)
        {
            GlobalControl.Instance.playerObjectSecond.SetActive(true);
            GlobalControl.Instance.playerObjectSecond = GameObject.FindWithTag("Player");

            thePlayerSecond = GlobalControl.Instance.playerObjectSecond.GetComponent<PlayerMove>();
            Debug.LogError("thePlayerSecond.playerCurrentMapSecond: " + thePlayerSecond.playerCurrentMapSecond);

        }
        else
        {
            Debug.LogError("PlayerMove not found!");
        }

        // ������ �ڵ�...
        if (moveMapSecond == thePlayerSecond.playerCurrentMapSecond)
        {
            theCameraSecond.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
            thePlayerSecond.transform.position = this.transform.position;
            Debug.Log("ok " + this.transform.position);
        }
        else
        {
            Debug.Log("error");
            Debug.Log("moveMapSecond : " + moveMapSecond + " / thePlayerSecond.playerCurrentMapSecond : " + thePlayerSecond.playerCurrentMapSecond);
        }
    }

}
