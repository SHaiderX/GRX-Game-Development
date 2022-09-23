using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    void PlaySound1() {
        AudioManager.instance.Play("Star1");
    }

    void PlaySound2()
    {
        AudioManager.instance.Play("Star2");
    }

    void PlaySound3()
    {
        AudioManager.instance.Play("Star3");
        AudioManager.instance.Play("3Star");
    }
}
