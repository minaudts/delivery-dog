using UnityEngine;

public class GoToBakery : MonoBehaviour
{
    public GameObject enterBakeryPopup;
    [SerializeField] 
    private AudioClip musicInBakery;
    private void Start()
    {
        enterBakeryPopup.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            ShowEnterBakeryPopup();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            HideEnterBakeryPopup();
        }
    }
    private void ShowEnterBakeryPopup()
    {
        enterBakeryPopup.SetActive(true);
    }
    private void HideEnterBakeryPopup()
    {
        enterBakeryPopup.SetActive(false);
    }
    public void OnGoToBakery()
    {
        Debug.Log("Player goes to bakery...");
        SoundManager.Instance.PlayDefaultButtonClick();
        SceneChanger.Instance.TransitionToScene("BakeryScene", () =>
        {
            Debug.Log("Performing gotobakery action");
            RecipeGenerator.Instance.CheckCurrentRecipe();
        });
    }
}
