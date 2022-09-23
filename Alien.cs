using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Alien : MonoBehaviour
{

    public int HP = 1;
    public static bool isMoving = false;
    public GameObject destroyEffect;

    public Sprite[] Skins;

    float speed;

    void Start()
    {
        //Change color depending on which galaxy
        GetComponent<SpriteRenderer>().sprite = Skins[AllMenus.GalaxyNum];
    }

    void Update()
    {

        speed = GetComponent<Rigidbody2D>().velocity.magnitude;
        if (speed <= 0.1 && !Destroyer.blackH)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        if (HP <= 0)
        {
            kill();
        }

    }

    public void kill()
    {
        Destroy(gameObject);
        AudioManager.instance.Play("Splat");
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        GameObject other = collision2D.gameObject;
        if (other.CompareTag("Rocket"))
        {
            HP = HP - 1;

            int i = UnityEngine.Random.Range(1, 6);
            if (i == 1)
                AudioManager.instance.Play("AlienHit1");
            if (i == 2)
                AudioManager.instance.Play("AlienHit2");
            if (i == 3)
                AudioManager.instance.Play("AlienHit3");
            if (i == 4)
                AudioManager.instance.Play("AlienHit4");
            if (i == 5)
                AudioManager.instance.Play("AlienHit5");
        }
    }

    void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy) {
            bool playOne = (UnityEngine.Random.value > 0.5f);
            if (playOne)
            {
                AudioManager.instance.Play("AlienFly1");
            }
            else
            {
                AudioManager.instance.Play("AlienFly2");
            }

            Destroy(gameObject);
        }
    }
}
