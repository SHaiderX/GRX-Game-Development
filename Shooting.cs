using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{

    Touch touch;

    [HideInInspector]
    public GameObject rocket;
    [HideInInspector]
    public Transform shotPoint;
    [HideInInspector]
    public GameObject shootEffect;
    [HideInInspector]
    public AllMenus menu;

    public int ammo;
    private int originalAmmo;
    public int goldAmmoNum;
    public float rocketPower = 1.8f;

    [HideInInspector]
    public GameObject lineRendererPrefab;

    [HideInInspector]
    public Image[] rockets;
    [HideInInspector]
    public Sprite blackAmmo;
    [HideInInspector]
    public Sprite goldAmmo;

    [HideInInspector]
    public Vector2 currPos;

    void Start()
    {
        originalAmmo = ammo;
    }

    private void Update()
    {
        if (!AllMenus.screenOn)
        {
            menu.Victory(ammo, goldAmmoNum, originalAmmo);
            updateAmmoVisuals();
            Aiming();

            if (ammo == 0)
                menu.GameOver();
                
        }
    }

    void Aiming()
    {
        if (Input.touchCount > 0 && !AllMenus.screenOn)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                DragStart();
            if (touch.phase == TouchPhase.Moved)
            {
                if (Vector2.Distance(touch.position, currPos) > 4f)
                    Dragging();
                currPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (ammo > 0)
                    Shoot(rocket);
            }
        }
    }

    void updateAmmoVisuals()
    {
        for (int i = 0; i < rockets.Length; i++)
        {
            if (i < ammo)
            {
                if (i + 1 > originalAmmo - goldAmmoNum)
                    rockets[i].sprite = goldAmmo;
                else
                {
                    rockets[i].sprite = blackAmmo;
                    rockets[i].color = new Color(1f, 1f, 1f, 1f);
                }
            }
            else
                rockets[i].color = new Color(1f, 1f, 1f, 0f);

            if (i < ammo)
                rockets[i].enabled = true;
            else
                rockets[i].enabled = false;
        }
    }

    public void RefillAmmo()
    {
        ammo = originalAmmo;
        goldAmmoNum = 0;
    }

    void Shoot(GameObject launch)
    {
        GameObject RocketIns = Instantiate(launch, shotPoint.position, transform.rotation);
        Vector3 RocketPos = GameObject.Find("ShotPos").transform.position;
        RocketPos.z = 0f;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = RocketPos - dragReleasePos;
        force.z = 0f;

        RocketIns.GetComponent<Rigidbody2D>().AddForce(-force * rocketPower, ForceMode2D.Impulse);
        RocketIns.GetComponent<Rigidbody2D>().gravityScale = 0.0f;

        if (launch.tag != "Line")
        {
            AudioManager.instance.Play("LaunchSound");
            Instantiate(shootEffect, RocketPos, Quaternion.identity);
            ammo = ammo - 1;
        }
    }

    void DragStart()
    {
        Vector3 dragStartPos = GameObject.Find("ShotPos").transform.position;
        dragStartPos.z = 0f;
    }

    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        if (GameObject.FindGameObjectWithTag("Line") != null)
            Destroy(GameObject.FindGameObjectWithTag("Line"));
        if (ammo > 0)
            Shoot(lineRendererPrefab);
    }

}
