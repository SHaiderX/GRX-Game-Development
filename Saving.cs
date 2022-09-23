using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saving : MonoBehaviour
{
    //make references to UI elements
    public ArrayMaker arrayMaker;

    //save file name
    const string fileName = "GalaxyRoyaleData";
    //save path, it will be constructed in Awake
    string path;

    public int TotalLevels = 100;
    //variable to store all your saved values
    SaveData savedValues;

    //Construct path
    private void Awake(){
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath + "/" + fileName;
    }

    //public int GalaxyBucks;
    //public int ExtraCans;
    //public int PlasmaAmmo;

    //public string[] allskins;
    //public string[] shopPurchases;

    //public int[] lvlStars;
    //public int[] lvlCans;

    //Create save method
    public void Save()
    {
        savedValues.GalaxyBucks = PlayerPrefs.GetInt("GalaxyBucks");
        savedValues.ExtraCans = PlayerPrefs.GetInt("ExtraCoin");
        savedValues.PlasmaAmmo = PlayerPrefs.GetInt("PlasmaAmmo");

        savedValues.allskins = arrayMaker.OutPutArray();
        savedValues.shopPurchases = arrayMaker.OutPutShopArray();

        int[] stars = new int[TotalLevels];
        int[] cans = new int[TotalLevels];

        for (int i = 0; i < TotalLevels; i++) {
            int lvl = i + 1;
            stars[i] = PlayerPrefs.GetInt("Level " + lvl);
            cans[i] = PlayerPrefs.GetInt("coin " + lvl);
        }
        savedValues.lvlStars = stars;
        savedValues.lvlCans = cans;


        //save the values to a file
        SaveManager.Instance.Save(savedValues, path, SaveComplete, true);
    }

    //this method will be called after save process is done
    private void SaveComplete(SaveResult result, string message)
    {
        //check for error
        if (result == SaveResult.Error)
        {
            Debug.LogError("Save error " + message);
        }

        //if no error save was successful

    }

    //Load data from file
    public void Load()
    {
        SaveManager.Instance.Load<SaveData>(path, LoadComplete, true);
    }

    //this method will be called when load process is done
    private void LoadComplete(SaveData data, SaveResult result, string message)
    {
        //result is success-> load your saved data into variables
        if (result == SaveResult.Success)
        {
            savedValues = data;
            LoadData();
        }

        //if for some reason your load failed, create an empty data to work with inside your game or give a message to the user
        if (result == SaveResult.Error || result == SaveResult.EmptyData)
        {
            savedValues = new SaveData();
        }  
    }

    private void LoadData() {
        PlayerPrefs.SetInt("GalaxyBucks", savedValues.GalaxyBucks);
        PlayerPrefs.SetInt("ExtraCoin", savedValues.ExtraCans);
        PlayerPrefs.SetInt("PlasmaAmmo", savedValues.PlasmaAmmo);

        for (int i = 1; i <= TotalLevels; i++)
        {
            PlayerPrefs.SetInt("Level " + i, savedValues.lvlStars[i-1]);
            PlayerPrefs.SetInt("coin " + i, savedValues.lvlCans[i-1]);
        }

        foreach (string s in savedValues.allskins) {
            PlayerPrefs.SetInt(s, 1);
        }

        foreach (string st in savedValues.shopPurchases)
        {
            PlayerPrefs.SetInt(st, 1);
        }
    }


    //1. Levels done
    //if (PlayerPrefs.GetInt("Level " + curLevel) == 3) | unlocked 3 star
    //if (PlayerPrefs.GetInt("coin " + curLevel) == 1) | Can or not
    //PlayerPrefs.GetInt("Level " + levelNumber);
    //PlayerPrefs.GetInt("coin " + levelNumber);
    //levelNumber in range(1, 100)

    //2. Skins collected
    //void Start() {

    //    allskins = arrayMaker.OutPutArray();

    //}

    //3. RocketAmmo

    //4. Shop
    //PlayerPrefs.SetInt("NoAds", 0);
    //PlayerPrefs.SetInt("Bundle1", 0);
    //PlayerPrefs.SetInt("Bundle2", 0);
    //PlayerPrefs.SetInt("Bundle3", 0);
    //PlayerPrefs.SetInt("Bundle4", 0);
}
