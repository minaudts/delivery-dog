using UnityEngine;

public class GoToCity : MonoBehaviour
{
    [SerializeField] 
    private AudioClip musicInCity;
    public void OnGoToCity()
    {
        Debug.Log("Player goes to city...");
        SoundManager.Instance.PlayDefaultButtonClick();
        SceneChanger.Instance.TransitionToScene("CityScene", null);
    }
}
