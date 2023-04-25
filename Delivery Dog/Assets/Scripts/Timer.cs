using UnityEngine;
using TMPro;

public class Timer : SingletonPersistent<Timer>
{
    public float timeSpent { get; private set; }
    public TMP_Text timerText;
    private bool timerShouldRun = false;
    [SerializeField] private AudioClip _gameOverSound;
    // Start is called before the first frame update
    void Start()
    {
        // Start at total time.
        timeSpent = 0;
        UpdateTimerText();
    }

    public void SetTimerShouldRun(bool val)
    {
        timerShouldRun = val;
    }

    void Update()
    {
        if(timerShouldRun)
        {
            timeSpent += Time.deltaTime;
            UpdateTimerText();
        }
    }
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeSpent / 60);
        int seconds = Mathf.FloorToInt(timeSpent % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        InGameUILogic.Instance.GameOver();
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySound(_gameOverSound);
        timeSpent = 0;
    }
}
