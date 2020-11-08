using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimation : MonoBehaviour
{
    private Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
        animation.Play();
    }
}
