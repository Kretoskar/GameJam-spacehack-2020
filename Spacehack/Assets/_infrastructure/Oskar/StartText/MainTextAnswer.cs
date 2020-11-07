using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TypeWriterEffect))]
public class MainTextAnswer : MonoBehaviour
{
    [SerializeField] private List<string> _answers;

    private int _currentIndex = 0;
    
    private TypeWriterEffect _typeWriter;
    
    private void Start()
    {
        _typeWriter = GetComponent<TypeWriterEffect>();

        _typeWriter.FullText = _answers[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetText(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GetText(false);
        }
    }

    private void GetText(bool _goLeft)
    {
        if (_goLeft)
        {
            _currentIndex--;
            
            if (_currentIndex < 0)
                _currentIndex = _answers.Count - 1;
        }
        else
        {
            _currentIndex++;

            if (_currentIndex > _answers.Count - 1)
                _currentIndex = 0;
        }

        _typeWriter.Rewrite(_answers[_currentIndex]);
    }
}
