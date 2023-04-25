using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : SingletonPersistent<SceneChanger>
{
    public Animator animator;
    private string sceneToLoad;
    [HideInInspector]
    public PlayerLocation CurrentLocation { get; private set; }
    private Action onTransitionComplete;
    private void Start() 
    {
        animator.enabled = false;
    }

    public void TransitionToScene(string sceneName, Action onTransitionComplete)
    {
        this.onTransitionComplete = onTransitionComplete;
        animator.enabled = true;
        sceneToLoad = sceneName;
        animator.SetTrigger("StartTransition");
    }

    public void OnTransitionComplete()
    {
        StartCoroutine(LoadLevel());
    }
    private IEnumerator LoadLevel()
    {
        if(Timer.Instance != null) Timer.Instance.SetTimerShouldRun(false);
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        if (asyncLoadLevel.isDone)
        {
            SetCurrentLocation();
            SoundManager.Instance.ChangeMusic(CurrentLocation);
            animator.SetTrigger("EndTransition");
            // Wait for animation to end, ff lelijk geprogrammeerd
            yield return new WaitForSeconds(4f/6f);
            if(Timer.Instance != null) Timer.Instance.SetTimerShouldRun(true);
            if(this.onTransitionComplete != null) 
            {
                this.onTransitionComplete();
                this.onTransitionComplete = null;
            } 
        }
    }
    private void SetCurrentLocation()
    {
        switch(sceneToLoad)
        {
            case "BakeryScene":
                CurrentLocation = PlayerLocation.Bakery;
                break;
            case "CityScene":
                CurrentLocation = PlayerLocation.City;
                break;
            default:
                break;
        }
    }
}
public enum PlayerLocation
{
    Bakery,
    City
}