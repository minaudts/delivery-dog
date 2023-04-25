using UnityEngine;

public class PersistentUILogic : SingletonPersistent<PersistentUILogic>
{
    public GameObject exitScreenPanel;
    public GameObject infoScreenPanel;
    public GameObject exitInfoPanel;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize values, so does not matter what they are set in editor.
        exitScreenPanel.SetActive(false);
        infoScreenPanel.SetActive(false);
        exitInfoPanel.SetActive(true);
    }

    public void ShowExitScreen()
    {
        Debug.Log("Toggle exit screen...");
        exitScreenPanel.SetActive(!exitScreenPanel.activeSelf);
        infoScreenPanel.SetActive(false);
    }

    public void ShowInfoScreen()
    {
        Debug.Log("Show info screen...");
        infoScreenPanel.SetActive(true);
        exitScreenPanel.SetActive(false);
    }
    public void CloseInfoScreen()
    {
        infoScreenPanel.SetActive(false);
        exitScreenPanel.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}

