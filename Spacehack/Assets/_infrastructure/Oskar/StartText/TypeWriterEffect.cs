using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private bool _initializeAtStart = true;
    [SerializeField] private float _startDelay = 5;
    [SerializeField] private float _delay = .1f;
    
    private string _fullText;

    private TextMeshProUGUI _tmp;
    
    private string _currentText = "";

    public string FullText
    {
        get => _fullText;
        set
        {
            _currentText = "";
            _fullText = value;
        }
    }

    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();

        if (_initializeAtStart)
            _fullText = _tmp.text;
        
        _tmp.text = _currentText;

        StartCoroutine(WriteCoroutine(true));
    }

    public void Rewrite(string newText)
    {
        StopAllCoroutines();
        StartCoroutine(RewriteCoroutine(newText));
    }

    private IEnumerator WriteCoroutine(bool waitForStart)
    {
        if(waitForStart)
            yield return new WaitForSeconds(_startDelay);
        
        for (int i = 0; i < _fullText.Length; i++)
        {
            _currentText = _fullText.Substring(0, i);
            _tmp.text = _currentText;
            yield return new WaitForSeconds(_delay);
        }

        _delay = _delay / 5;
    }

    private IEnumerator RewriteCoroutine(string newText)
    {
        for (int i = _fullText.Length; i >= 0; i--)
        {
            _currentText = _fullText.Substring(0, i);
            _tmp.text = _currentText;
            yield return new WaitForSeconds(_delay);
        }

        FullText = newText;
        
        for (int i = 0; i < _fullText.Length; i++)
        {
            _currentText = _fullText.Substring(0, i);
            _tmp.text = _currentText;
            yield return new WaitForSeconds(_delay);
        }
    }
}
