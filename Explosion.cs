using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void DestroyGameObject() {
        Destroy(gameObject);
    }

    void NormalExplosion() {
        AudioManager.instance.Play("NormalExplosion");
    }

    void BigExplosion()
    {
        AudioManager.instance.Play("BigExplosion");
    }
}
