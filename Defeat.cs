using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    bool called = false;
    void Awake()
    {
        if (!called)
            AudioManager.instance.Play("Defeat");
    }
}
