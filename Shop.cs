using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Transform Bundle1Skin;
    public Transform Bundle2Skin;
    public Transform Bundle3Skin;
    public Transform Bundle4Skin;

    public Text Bucks1Price;
    public Text Bucks2Price;
    public Text Bucks3Price;
    public Text Bucks4Price;
    public Text Bundle1Price;
    public Text Bundle2Price;
    public Text Bundle3Price;
    public Text Bundle4Price;
    public Text NoAdsPrice;

    public GameObject LoadingScreen;
    public Saving save;
    private bool close = false;

    private void OnEnable() {
        IAPManager.Instance.InitializeIAPManager(InitComplete);
        StartCoroutine(textUpdate());
        if (close)
        {
            CloseLoading();
        }

        }

    //www.youtube.com/watch?v=iLjzxaIrfm8&list=PLKeb94eicHQumyCLcJbprEgOhyKc2Q7EQ&index=6
    
    private void Start() {

        IAPManager.Instance.InitializeIAPManager(InitComplete);

        StartCoroutine(textUpdate());

        if (!PlayerPrefs.HasKey("NoAds"))
        {
            PlayerPrefs.SetInt("NoAds", 0);
            PlayerPrefs.SetInt("Bundle1", 0);
            PlayerPrefs.SetInt("Bundle2", 0);
            PlayerPrefs.SetInt("Bundle3", 0);
            PlayerPrefs.SetInt("Bundle4", 0);
        }
    }
    
    private IEnumerator textUpdate()
    {
        while (!IAPManager.Instance.IsInitialized())
        {
            yield return new WaitForSeconds(0.3f);
        }

        Bucks1Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bucks1);
        Bucks2Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bucks2);
        Bucks3Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bucks3);
        Bucks4Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bucks4);

        Bundle1Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bundle1);
        Bundle2Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bundle2);
        Bundle3Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bundle3);
        Bundle4Price.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Bundle4);

        NoAdsPrice.text = IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.NoAds);
    }

    public void AddGalaxyBucks(int amount)
    {
        int current = PlayerPrefs.GetInt("GalaxyBucks");
        PlayerPrefs.SetInt("GalaxyBucks", current + amount);
    }

    public void CloseLoading() {
        LoadingScreen.SetActive(false);
    }


    private void InitComplete(IAPOperationStatus status, string errorMsg, List<StoreProduct> allStoreProducts) {

        if (status == IAPOperationStatus.Success)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
            CloseLoading();
            close = true;
            
            for (int i = 0; i < allStoreProducts.Count; i++)
            {
                StoreProduct currProd = allStoreProducts[i];

                InitHelper(currProd, "NoAds");
                InitHelper(currProd, "Bundle1");
                InitHelper(currProd, "Bundle2");
                InitHelper(currProd, "Bundle3");
                InitHelper(currProd, "Bundle4");
            }
        }
    }

    private void InitHelper(StoreProduct currProd, string name) {
        if (currProd.productName == name)
        {
            if (currProd.active == true)
                PlayerPrefs.SetInt(name, 1);
            else
                PlayerPrefs.SetInt(name, 0);
        }
    }

    public void BuyBucks1()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bucks1, ProductBought);
    }
    public void BuyBucks2()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bucks2, ProductBought);
    }
    public void BuyBucks3()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bucks3, ProductBought);
    }
    public void BuyBucks4()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bucks4, ProductBought);
    }

    public void BuyBundle1()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bundle1, ProductBought);
    }
    public void BuyBundle2()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bundle2, ProductBought);
    }
    public void BuyBundle3()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bundle3, ProductBought);
    }
    public void BuyBundle4()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Bundle4, ProductBought);
    }

    public void BuyNoAds()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.NoAds, ProductBought);
    }

    private void ProductBought(IAPOperationStatus status, string errorMessage, StoreProduct boughtProduct) { 
        if(status == IAPOperationStatus.Success)
        {
            AddGalaxyBucks(boughtProduct.value);

            if (boughtProduct.productName == "Bundle1")
            {
                GetBundleSkin(Bundle1Skin);
                PlayerPrefs.SetInt("Bundle1", 1);
            }
            if (boughtProduct.productName == "Bundle2")
            {
                GetBundleSkin(Bundle2Skin);
                PlayerPrefs.SetInt("Bundle2", 1);
            }
            if (boughtProduct.productName == "Bundle3")
            {
                GetBundleSkin(Bundle3Skin);
                PlayerPrefs.SetInt("Bundle3", 1);
            }
            if (boughtProduct.productName == "Bundle4")
            {
                GetBundleSkin(Bundle4Skin);
                PlayerPrefs.SetInt("Bundle4", 1);
            }
            if (boughtProduct.productName == "NoAds")
            {
                PlayerPrefs.SetInt("NoAds", 1);
            }
            save.Save();
        }
    }

    public void GetBundleSkin(Transform skin)
    {
        Transform currSkin = skin;

        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Helmet.name))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Helmet.name, 1);
            //UIHelmet.sprite = Helmet;
        }

        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Gauntlets1.name))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Gauntlets1.name, 1);
            //UIGauntlets.sprite = Gauntlets;

            //UIGauntlets2.sprite = Gauntlets2;
        }

        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Chest.name))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Chest.name, 1);
            //UIChest.sprite = Chestplate;
        }

        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Backpack.name))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Backpack.name, 1);
            //UIBackpack.sprite = Backpack;
        }

        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Pants.name))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Pants.name, 1);
            //UIPants.sprite = Pants;
        }
    }
}
