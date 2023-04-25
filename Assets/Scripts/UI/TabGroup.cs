using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public GameObject[] tabs;
    private void OnEnable() {
        TurnOnTab(0);
    }
    public void TurnOnTab(int tab)
    {
        SoundManager.Instance.PlayDefaultButtonClick();
        for(int i = 0; i < tabs.Length; i++)
        {
            // If i is tab to turn on, set to true, else it's false.
            tabs[i].SetActive(i == tab);
        }
    }
}
