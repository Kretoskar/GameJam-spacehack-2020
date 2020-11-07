using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsAudio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private float _timeBetweenSounds = 0.5f;

    private bool _isPlaying;
    
    private AudioSource _audioSource;
    
    private AudioClip GetRandomClip() => _clips[Random.Range(0, _clips.Count)];

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartPlaying()
    {
        if(_isPlaying) return;

        _isPlaying = true;
        
        StopAllCoroutines();
        StartCoroutine(StepSoundCoroutine());
    }

    public void StopPlaying()
    {
        if(!_isPlaying) return;
        _isPlaying = false;
        
        StopAllCoroutines();
    }
    
    private IEnumerator StepSoundCoroutine()
    {
        while (true)
        {
            _audioSource.clip = GetRandomClip();
            _audioSource.Play();
            yield return new WaitForSeconds(_timeBetweenSounds);
        }
    }
}
