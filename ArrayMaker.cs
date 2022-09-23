using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class ArrayMaker : MonoBehaviour
{
    public Sprite[] AllSkins;
    private string[] allNames;

    private string[] AllShop = {"NoAds", "Bundle1", "Bundle2", "Bundle3", "Bundle4"};
    private string[] allShopPurchases;

    //PlayerPrefs.SetInt("NoAds", 0);
    //PlayerPrefs.SetInt("Bundle1", 0);
    //PlayerPrefs.SetInt("Bundle2", 0);
    //PlayerPrefs.SetInt("Bundle3", 0);
    //PlayerPrefs.SetInt("Bundle4", 0);

    private void Start()
    {
        int l = AllSkins.Length;
        int l2 = AllShop.Length;
        allNames = new string[l];
        allShopPurchases = new string[l2];
        OutPutArray();
    }

    public string[] OutPutArray() {
        int i = 0;
        foreach (Sprite s in AllSkins)
        {
            if (PlayerPrefs.HasKey(s.name)) {
                allNames[i] = s.name;
                i++;
            }    
        }
        return allNames;
    }

    public string[] OutPutShopArray()
    {
        int i = 0;
        foreach (string sr in AllShop)
        {
            if (PlayerPrefs.HasKey(sr))
            {
                allShopPurchases[i] = sr;
                i++;
            }
        }
        return allShopPurchases;
    }

}
