using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public string yourButtonName; // 버튼의 이름
    public Button yourButton; // 클릭 이벤트에 메서드를 연결할 버튼

    private Inventory inven;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            GameObject uiObject = GameObject.Find(yourButtonName);
            if (uiObject != null)
            {
                yourButton = uiObject.GetComponent<Button>();
                if (yourButton != null)
                {
                    yourButton.onClick.AddListener(AddSlot);
                }
                else
                {
                    Debug.LogError("Button component not found");
                }
            }
            else
            {
                Debug.LogError("UI object not found");
            }
        };
    }

    public void AddSlot()
    {
        if (inven == null)
        {
            inven = Inventory.Instance;
        }
        if (inven != null)
        {
            inven.SlotCnt = inven.SlotCnt + 1;
        }
        else
        {
            Debug.LogError("Inventory instance not found");
        }
    }
}
