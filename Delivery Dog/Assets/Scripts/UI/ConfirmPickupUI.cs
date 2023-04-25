using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPickupUI : Singleton<ConfirmPickupUI>
{
    [SerializeField]
    private Image ingredientPreview;
    [SerializeField]
    private GameObject confirmPickupPanel;
    [SerializeField]
    private GameObject neutralView;
    [SerializeField]
    private GameObject collectedView;
    [SerializeField]
    private GameObject declinedView;
    private Action collectAction;
    private Action declineAction;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip confirmSound;
    [SerializeField] private AudioClip declineSound;

    // Start is called before the first frame update
    private void Start()
    {
        confirmPickupPanel.SetActive(false);
        Hide();
    }
    public void CollectBtnClick()
    {
        SoundManager.Instance.PlaySound(confirmSound);
        this.collectAction();
        this.neutralView.SetActive(false);
        this.collectedView.SetActive(true);
    }
    public void DeclineBtnClick()
    {
        SoundManager.Instance.PlaySound(declineSound);
        declineAction();
        neutralView.SetActive(false);
        declinedView.SetActive(true);
    }

    public void CloseDialogClick()
    {
        // Resume game   
        Time.timeScale = 1;
        SoundManager.Instance.PlayDefaultButtonClick();
        Hide();
    }

    public void ShowConfirmDialog(Sprite ingredientSprite, Action collectAction, Action declineAction) 
    {
        SoundManager.Instance.PlaySound(pickUpSound);
        confirmPickupPanel.gameObject.SetActive(true);
        neutralView.SetActive(true);
        collectedView.SetActive(false);
        declinedView.SetActive(false);
        Debug.Log(ingredientSprite);
        ingredientPreview.sprite = ingredientSprite;
        this.collectAction = collectAction;
        this.declineAction = declineAction;
        // Pause game
        Time.timeScale = 0;
    }

    private void Hide() 
    {
        confirmPickupPanel.SetActive(false);
    }
}