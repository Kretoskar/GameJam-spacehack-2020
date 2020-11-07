using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : MonoBehaviour
{
   private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        _audioSource = FindObjectOfType<DontDestroyOnLoad>().GetComponent<AudioSource>();

        if (_audioSource != null)
        {
            _audioSource.clip = _clip;
            _audioSource.Play();
        }
    }
}
