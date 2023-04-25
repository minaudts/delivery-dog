using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGameListener : MonoBehaviour
{
    public GameObject exitScreenPanel;
    public GameObject infoScreenPanel;
    // Update is called once per frame
    void Update()
    {
        // Als any key pressed en title screen is enabled
        if (!exitScreenPanel.activeSelf && !infoScreenPanel.activeSelf && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            OnStartGame();
        }
    }
    public void OnStartGame()
    {
        Debug.Log("Starting game...");
        SceneChanger.Instance.TransitionToScene("BakeryScene", null);
    }
}
