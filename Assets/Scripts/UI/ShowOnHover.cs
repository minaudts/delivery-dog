using UnityEngine;

public class ShowOnHover : MonoBehaviour
{
    public GameObject objectToShow;
    private void Start() 
    {
        objectToShow.gameObject.SetActive(false);
    }
    public void OnPointerEnter()
    {
        objectToShow.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        objectToShow.gameObject.SetActive(false);
    }
}
