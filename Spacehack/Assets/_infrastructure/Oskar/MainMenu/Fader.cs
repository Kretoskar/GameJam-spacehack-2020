using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] private bool _fadeOutOnClickEnter = true;
    [SerializeField] private Image _image;
    
    private void Start()
    {
        FadeIn();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            FadeOut();
    }
    
    private void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        for (float i = 1; i > 0; i-= 0.01f)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, i);
            yield return new WaitForSeconds(0.02f);
        }
        
        if(!_fadeOutOnClickEnter)
            Destroy(gameObject);
    }

    private IEnumerator FadeOutCoroutine()
    {
        for (float i = 0; i < 1; i+= 0.01f)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, i);
            yield return new WaitForSeconds(0.02f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
