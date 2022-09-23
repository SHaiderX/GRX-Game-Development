using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSet : MonoBehaviour
{
    public Image UIHelmet;
    public Image UIGauntlets;
    public Image UIBackpack;
    public Image UIChest;
    public Image UIPants;

    public Image UIRocket;

    

    Transform player;

    void Awake()
    {
        player = gameObject.transform.GetChild(1).transform.GetChild(2);
    }

    void Start()
    {
        player.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = UIHelmet.transform.GetComponent<Image>().sprite;
        player.transform.GetChild(1).transform.GetChild(3).transform.GetComponent<SpriteRenderer>().sprite = UIGauntlets.transform.GetComponent<Image>().sprite;
        player.transform.GetChild(1).transform.GetChild(2).transform.GetComponent<SpriteRenderer>().sprite = UIChest.transform.GetComponent<Image>().sprite;
        player.transform.GetChild(1).transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = UIBackpack.transform.GetComponent<Image>().sprite;
        player.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = UIPants.transform.GetComponent<Image>().sprite;

        gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = UIRocket.transform.GetComponent<Image>().sprite;
    
    }
}
