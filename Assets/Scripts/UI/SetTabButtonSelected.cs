using UnityEngine;
using UnityEngine.UI;

public class SetTabButtonSelected : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<Button>().Select();
    }
}
