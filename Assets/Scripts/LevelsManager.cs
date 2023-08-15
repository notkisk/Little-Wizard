using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public Button[] lvlButtons;
    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt",2);
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 2 > levelAt)
                lvlButtons[i].interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)&&Input.GetKey(KeyCode.LeftAlt)&&Input.GetKey(KeyCode.RightShift))
        {
            PlayerPrefs.SetInt("levelAt",20);
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.DeleteKey("levelAt");
        }
#endif
    }

}
