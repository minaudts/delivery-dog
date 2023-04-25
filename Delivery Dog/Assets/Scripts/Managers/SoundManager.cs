using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistent<SoundManager>
{
    [SerializeField] 
    private AudioSource _musicSource, _effectsSource;
    [SerializeField]
    private AudioClip _titleScreenMusic, _bakeryMusic, _cityMusic, _defaultButtonClick;
    // Start is called before the first frame update
    void Start()
    {
        _musicSource.clip = _titleScreenMusic;
        _musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }
    public void PlayDefaultButtonClick()
    {
        _effectsSource.PlayOneShot(_defaultButtonClick);
    }
    public void ChangeMusic(PlayerLocation location)
    {
        switch(location)
        {
            case PlayerLocation.Bakery:
                _musicSource.clip = _bakeryMusic;
                break;
            case PlayerLocation.City:
                _musicSource.clip = _cityMusic;
                break;
            default:
                break;
        }
        _musicSource.Play();
    }
    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
