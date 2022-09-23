using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

public class SkinSelect : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public Image UIHelmet;
    [HideInInspector]
    public Image UIGauntlets;
    [HideInInspector]
    public Image UIGauntlets2;
    [HideInInspector]
    public Image UIBackpack;
    [HideInInspector]
    public Image UIChest;
    [HideInInspector]
    public Image UIPants;

    [HideInInspector]
    public Image UIRocket;

    [HideInInspector]
    public Image MenuHelmet;
    [HideInInspector]
    public Image MenuGauntlets;
    [HideInInspector]
    public Image MenuBackpack;
    [HideInInspector]
    public Image MenuChest;
    [HideInInspector]
    public Image MenuPants;

    [HideInInspector]
    public Image MenuRocket;

    [HideInInspector]
    public Transform allSkins;
    [HideInInspector]
    public Transform allRockets;
    [HideInInspector]
    private int index;
    [HideInInspector]
    private int rocketIndex;

    [HideInInspector]
    private Transform currSkin;
    [HideInInspector]
    private Transform currRocket;
    [HideInInspector]
    public SimpleScrollSnap SkinsScrollSnapper;
    [HideInInspector]
    public SimpleScrollSnap RocketMenuSnapper;

    [HideInInspector]
    public GameObject HeadBuy;
    [HideInInspector]
    public GameObject BackpackBuy;
    [HideInInspector]
    public GameObject GauntletsBuy;
    [HideInInspector]
    public GameObject ChestplateBuy;
    [HideInInspector]
    public GameObject PantsBuy;
    [HideInInspector]
    public GameObject AllBuy;

    //Set buttons
    [HideInInspector]
    public GameObject HeadB;
    [HideInInspector]
    public GameObject BackpackB;
    [HideInInspector]
    public GameObject GauntletsB;
    [HideInInspector]
    public GameObject ChestplateB;
    [HideInInspector]
    public GameObject PantsB;
    [HideInInspector]
    public GameObject AllB;

    //Buy Buttons
    [HideInInspector]
    public GameObject[] BuyButtons;
    [HideInInspector]
    public GameObject[] PriceText;
    [HideInInspector]
    public GameObject[] CoinImage;

    [HideInInspector]
    public GameObject NotEnough;
    [HideInInspector]
    public Text NotEnoughText;
    [HideInInspector]
    public GameObject PurchaseConfirmation;
    [HideInInspector]
    public GameObject Success;

    [HideInInspector]
    public GameObject RocketNotEnough;
    [HideInInspector]
    public GameObject RocketPurchaseConfirmation;
    [HideInInspector]
    public GameObject RocketSuccess;

    [HideInInspector]
    public Button Confirmed;

    [HideInInspector]
    public GameObject RocketBuyButton;
    [HideInInspector]
    public GameObject RocketSetButton;
    [HideInInspector]
    public GameObject RocketButtonText;

    Sprite Helmet;
    Sprite Gauntlets;
    Sprite Gauntlets2;
    Sprite Chestplate;
    Sprite Backpack;
    Sprite Pants;

    bool ChangingFade = false;
    bool coroutineRunning;
    Coroutine co;

    [HideInInspector]
    public Sprite HelmetButton;
    [HideInInspector]
    public Sprite HelmetButtonSelected;
    [HideInInspector]
    public Sprite GauntletsButton;
    [HideInInspector]
    public Sprite GauntletsButtonSelected;
    [HideInInspector]
    public Sprite BackpackButton;
    [HideInInspector]
    public Sprite BackpackButtonSelected;
    [HideInInspector]
    public Sprite ChestplateButton;
    [HideInInspector]
    public Sprite ChestplateButtonSelected;
    [HideInInspector]
    public Sprite PantsButton;
    [HideInInspector]
    public Sprite PantsButtonSelected;
    [HideInInspector]
    public Sprite AllButton;
    [HideInInspector]
    public Sprite AllButtonSelected;

    [HideInInspector]
    public Sprite RocketButton;
    [HideInInspector]
    public Sprite RocketSelected;
    [HideInInspector]
    public Sprite RedBuyButton;

    [HideInInspector]
    public Text HelmetCostText;
    [HideInInspector]
    public Text ChestplateCostText;
    [HideInInspector]
    public Text BackpackCostText;
    [HideInInspector]
    public Text GauntletsCostText;
    [HideInInspector]
    public Text PantsCostText;

    private int HelmetCost;
    private int GauntletsCost;
    private int ChestplateCost;
    private int BackpackCost;
    private int PantsCost;
    int AllCost;

    public Saving save;

    void Awake()
    {
        PlayerPrefs.SetInt("Orange", 1);
        PlayerPrefs.SetInt("OrangeHelmet", 1);
        PlayerPrefs.SetInt("OrangeArms1", 1);
        PlayerPrefs.SetInt("OrangeBackpack", 1);
        PlayerPrefs.SetInt("OrangeChest", 1);
        PlayerPrefs.SetInt("OrangePants", 1);
        PlayerPrefs.SetInt("rocketship2", 1);
    }
    
    #endregion

    void Update()
    {

        
        UpdateNums();

        CheckPayHelmet();
        CheckPayGauntlet();
        CheckPayBackpack();
        CheckPayChest();
        CheckPayPants();
        CheckPayAll();
        RocketButtonColor();
        CheckPayRocket();
        if (!ChangingFade)
            ButtonColorChange();
    }

    #region Misc

    public void UpdateNums(){
        MenuHelmet.sprite = UIHelmet.transform.GetComponent<Image>().sprite;
        MenuGauntlets.sprite = UIGauntlets2.transform.GetComponent<Image>().sprite;
        MenuBackpack.sprite = UIBackpack.transform.GetComponent<Image>().sprite;
        MenuChest.sprite = UIChest.transform.GetComponent<Image>().sprite;
        MenuPants.sprite = UIPants.transform.GetComponent<Image>().sprite;
        MenuRocket.sprite = UIRocket.transform.GetComponent<Image>().sprite;

        index = SkinsScrollSnapper.CurrentPanel;
        rocketIndex = RocketMenuSnapper.CurrentPanel;
        currSkin = allSkins.GetChild(index);
        currRocket = allRockets.GetChild(rocketIndex);

        //UnityEngine.Debug.Log(currSkin.name);

        Helmet = currSkin.GetComponent<SkinHolder>().Helmet;
        Gauntlets = currSkin.GetComponent<SkinHolder>().Gauntlets1;
        Gauntlets2 = currSkin.GetComponent<SkinHolder>().Gauntlets2;
        Chestplate = currSkin.GetComponent<SkinHolder>().Chest;
        Backpack = currSkin.GetComponent<SkinHolder>().Backpack;
        Pants = currSkin.GetComponent<SkinHolder>().Pants;

        HelmetCost = currSkin.GetComponent<SkinHolder>().HelmetCost;
        GauntletsCost = currSkin.GetComponent<SkinHolder>().GauntletsCost;
        ChestplateCost = currSkin.GetComponent<SkinHolder>().ChestplateCost;
        BackpackCost = currSkin.GetComponent<SkinHolder>().BackpackCost;
        PantsCost = currSkin.GetComponent<SkinHolder>().PantsCost;

        HelmetCostText.text = HelmetCost.ToString();
        ChestplateCostText.text = ChestplateCost.ToString();
        GauntletsCostText.text = GauntletsCost.ToString();
        BackpackCostText.text = BackpackCost.ToString();
        PantsCostText.text = PantsCost.ToString();

        
    }

    public void ButtonSound1()
    {
        AudioManager.instance.Play("Button2");
        Vibration.CreateOneShot(10, 50);
    }

    public void ButtonSound2()
    {
        AudioManager.instance.Play("ButtonSuccess");
        StartCoroutine(Vibration.VibratePattern(10, 50, 1, 2));
    }

    public void ButtonSound3()
    {
        AudioManager.instance.Play("ButtonFail");
        StartCoroutine(Vibration.VibratePattern(20, 70, 1, 2));
    }

    public void AddGalaxyBucks(int amount)
    {
        int current = PlayerPrefs.GetInt("GalaxyBucks");
        PlayerPrefs.SetInt("GalaxyBucks", current + amount);
    }

    IEnumerator FadeOut()
    {
        Color HelmetC = HeadB.GetComponent<Image>().color;
        Color GauntletC = GauntletsB.GetComponent<Image>().color;
        Color ChestC = ChestplateB.GetComponent<Image>().color;
        Color BackpackC = BackpackB.GetComponent<Image>().color;
        Color PantsC = PantsB.GetComponent<Image>().color;
        Color AllC = AllB.GetComponent<Image>().color;

        Color LockC = HeadBuy.GetComponent<Image>().color;
        Color WhiteC = Color.white;
        while (HelmetC.a > 0)
        {                   //use "< 1" when fading in
            ChangingFade = true;
            HelmetC.a -= Time.deltaTime / 0.3f;    //fades out over 1 second. change to += to fade in
            HeadB.GetComponent<Image>().color = HelmetC;

            GauntletC.a -= Time.deltaTime / 0.3f;
            GauntletsB.GetComponent<Image>().color = GauntletC;

            ChestC.a -= Time.deltaTime / 0.3f;
            ChestplateB.GetComponent<Image>().color = ChestC;

            BackpackC.a -= Time.deltaTime / 0.3f;
            BackpackB.GetComponent<Image>().color = BackpackC;

            PantsC.a -= Time.deltaTime / 0.3f;
            PantsB.GetComponent<Image>().color = PantsC;

            AllC.a -= Time.deltaTime / 0.3f;
            AllB.GetComponent<Image>().color = AllC;

            LockC.a -= Time.deltaTime / 0.3f;
            WhiteC.a -= Time.deltaTime / 0.3f;

            for (int i = 0; i < 6; i++)
            {
                BuyButtons[i].GetComponent<Image>().color = LockC;
                PriceText[i].GetComponent<Text>().color = WhiteC;
                CoinImage[i].GetComponent<Image>().color = WhiteC;
            }

            yield return null;
        }
    }

    public void FadeOutButton()
    {
        co = StartCoroutine(FadeOut());
    }


    public bool Pay(int amount)
    {
        if (amount <= PlayerPrefs.GetInt("GalaxyBucks"))
        {

            PlayerPrefs.SetInt("GalaxyBucks", PlayerPrefs.GetInt("GalaxyBucks") - amount);
            //UnityEngine.Debug.Log("Success!! " + amount + "charged, Remaining: " + PlayerPrefs.GetInt("GalaxyBucks"));
            Success.SetActive(true);
            RocketSuccess.SetActive(true);
            ButtonSound2();
            return true;
        }
        //UnityEngine.Debug.Log("Not Enough Galaxy Bucks");
        PurchaseConfirmation.SetActive(false);
        RocketPurchaseConfirmation.SetActive(false);
        ButtonSound3();
        NotEnough.SetActive(true);
        NotEnoughText.text = "Not Enough Galaxy Bucks.\n " + amount.ToString() + " required.\n Have: " + PlayerPrefs.GetInt("GalaxyBucks").ToString();
        RocketNotEnough.SetActive(true);
        return false;
    }

    public void Close()
    {
        NotEnough.SetActive(false);
        Success.SetActive(false);
        RocketNotEnough.SetActive(false);
        RocketSuccess.SetActive(false);

        PurchaseConfirmation.SetActive(false);
        RocketPurchaseConfirmation.SetActive(false);
        Confirmed.onClick.RemoveAllListeners();
    }

    public void CloseConfirmation()
    {
        PurchaseConfirmation.SetActive(false);
        RocketPurchaseConfirmation.SetActive(false);
        Confirmed.onClick.RemoveAllListeners();
    }

    public void ButtonColorChange()
    {
        int check = 0;

        if (UIHelmet.sprite == Helmet)
        {
            HeadB.GetComponent<Image>().color = Color.white;
            HeadB.GetComponent<Image>().sprite = HelmetButtonSelected;
            check++;
        }
        else
        {
            HeadB.GetComponent<Image>().color = Color.white;
            check--;
            HeadB.GetComponent<Image>().sprite = HelmetButton;
        }

        if (UIGauntlets.sprite == Gauntlets)
        {
            GauntletsB.GetComponent<Image>().color = Color.white;
            check++;
            GauntletsB.GetComponent<Image>().sprite = GauntletsButtonSelected;
        }
        else
        {
            GauntletsB.GetComponent<Image>().color = Color.white;
            check--;
            GauntletsB.GetComponent<Image>().sprite = GauntletsButton;
        }

        if (UIBackpack.sprite == Backpack)
        {
            BackpackB.GetComponent<Image>().color = Color.white;
            check++;
            BackpackB.GetComponent<Image>().sprite = BackpackButtonSelected;
        }
        else
        {
            BackpackB.GetComponent<Image>().color = Color.white;
            check--;
            BackpackB.GetComponent<Image>().sprite = BackpackButton;
        }

        if (UIChest.sprite == Chestplate)
        {
            ChestplateB.GetComponent<Image>().color = Color.white;
            check++;
            ChestplateB.GetComponent<Image>().sprite = ChestplateButtonSelected;
        }
        else
        {
            ChestplateB.GetComponent<Image>().color = Color.white;
            check--;
            ChestplateB.GetComponent<Image>().sprite = ChestplateButton;
        }

        if (UIPants.sprite == Pants)
        {
            PantsB.GetComponent<Image>().color = Color.white;
            check++;
            PantsB.GetComponent<Image>().sprite = PantsButtonSelected;
        }
        else
        {
            PantsB.GetComponent<Image>().color = Color.white;
            check--;
            PantsB.GetComponent<Image>().sprite = PantsButton;
        }

        if (check == 5)
        {
            AllB.GetComponent<Image>().color = Color.white;
            AllB.GetComponent<Image>().sprite = AllButtonSelected;
        }
        else
        {
            AllB.GetComponent<Image>().color = Color.white;
            AllB.GetComponent<Image>().sprite = AllButton;
        }

        Color Lock = Color.red;
        Lock.a -= 0.1f;

        for (int i = 0; i < 6; i++)
        {
            BuyButtons[i].GetComponent<Image>().color = Lock;
            PriceText[i].GetComponent<Text>().color = Color.white;
            CoinImage[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        }
    }

    public void StopChangingFade()
    {
        StopAllCoroutines();
        ChangingFade = false;
    }
    #endregion

    #region Rocket

    public void RocketButtonColor()
    {
        if (UIRocket.sprite == currRocket.GetComponent<Image>().sprite)
        {
            RocketButtonText.GetComponent<Text>().text = "Selected";
            RocketSetButton.GetComponent<Image>().sprite = RocketSelected;
        }
        else
        {
            RocketButtonText.GetComponent<Text>().text = "Set";
            RocketSetButton.GetComponent<Image>().sprite = RocketButton;
        }

    }

    public void SelectRocket()
    {

        Sprite Rocket = currRocket.GetComponent<Image>().sprite;
        UIRocket.sprite = Rocket;
    }

    public void CheckPayRocket()
    {
        if (PlayerPrefs.HasKey(currRocket.name))
            RocketBuyButton.SetActive(false);
        else
        {
            RocketBuyButton.SetActive(true);
            RocketButtonText.GetComponent<Text>().text = " ";
        }
    }

    public void ButtonConfirmRocket()
    {
        RocketPurchaseConfirmation.SetActive(true);
    }

    public void PayForRocket()
    {
        if (Pay(700))
        {
            Sprite Rocket = currRocket.GetComponent<Image>().sprite;
            UIRocket.sprite = Rocket;
            PlayerPrefs.SetInt(currRocket.name, 1);
            CloseConfirmation();
            save.Save();
        }
    }
    #endregion

    #region Helmet
    public void skinSelectHelmet()
    {

        UIHelmet.sprite = Helmet;
        UnityEngine.Debug.Log(Helmet.name);
    }

    public void CheckPayHelmet()
    {
        if (PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Helmet.name))
            HeadBuy.SetActive(false);
        else
            HeadBuy.SetActive(true);
    }

    public void ButtonConfirmHelmet()
    {
        PurchaseConfirmation.SetActive(true);
        Confirmed.onClick.AddListener(PayForHelmet);

    }

    public void PayForHelmet()
    {
        if (Pay(HelmetCost))
        {
            UIHelmet.sprite = Helmet;
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Helmet.name, 1);
            //Save in all PayFor...
            save.Save();
            CloseConfirmation();
        }
    }
    #endregion

    #region Gauntlets
    public void skinSelectGauntlets()
    {
        UIGauntlets.sprite = Gauntlets;
        UIGauntlets2.sprite = Gauntlets2;
    }

    public void CheckPayGauntlet()
    {
        if (PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Gauntlets1.name))
            GauntletsBuy.SetActive(false);
        else
            GauntletsBuy.SetActive(true);
    }

    public void ButtonConfirmGauntlets()
    {
        PurchaseConfirmation.SetActive(true);
        Confirmed.onClick.AddListener(PayForGauntlet);
    }

    public void PayForGauntlet()
    {
        if (Pay(GauntletsCost))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Gauntlets1.name, 1);
            UIGauntlets.sprite = Gauntlets;

            UIGauntlets2.sprite = Gauntlets2;
            CloseConfirmation();
            save.Save();
        }
    }
    #endregion

    #region Chestplate
    public void skinSelectChest()
    {
        UIChest.sprite = Chestplate;
    }

    public void CheckPayChest()
    {
        if (PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Chest.name))
            ChestplateBuy.SetActive(false);
        else
            ChestplateBuy.SetActive(true);
    }

    public void ButtonConfirmChest()
    {
        PurchaseConfirmation.SetActive(true);
        Confirmed.onClick.AddListener(PayForChest);
    }

    public void PayForChest()
    {
        if (Pay(ChestplateCost))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Chest.name, 1);
            UIChest.sprite = Chestplate;
            CloseConfirmation();
            save.Save();
        }
    }
    #endregion

    #region Backpack
    public void skinSelectBackpack()
    {
        UIBackpack.sprite = Backpack;
    }

    public void CheckPayBackpack()
    {
        if (PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Backpack.name))
            BackpackBuy.SetActive(false);
        else
            BackpackBuy.SetActive(true);
    }

    public void ButtonConfirmBackpack()
    {
        PurchaseConfirmation.SetActive(true);
        Confirmed.onClick.AddListener(PayForBackpack);
    }

    public void PayForBackpack()
    {
        if (Pay(BackpackCost))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Backpack.name, 1);
            UIBackpack.sprite = Backpack;
            CloseConfirmation();
            save.Save();
        }
    }
    #endregion

    #region Pants
    public void skinSelectLegs()
    {
        UIPants.sprite = Pants;
    }

    public void CheckPayPants()
    {
        if (PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Pants.name))
            PantsBuy.SetActive(false);
        else
            PantsBuy.SetActive(true);
    }

    public void ButtonConfirmPants()
    {
        PurchaseConfirmation.SetActive(true);
        Confirmed.onClick.AddListener(PayForPants);
    }

    public void PayForPants()
    {
        if (Pay(PantsCost))
        {
            PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Pants.name, 1);
            UIPants.sprite = Pants;
            CloseConfirmation();
            save.Save();
        }
    }
    #endregion

    #region All
    public void skinSelectAll()
    {
        skinSelectHelmet();
        skinSelectGauntlets();
        skinSelectChest();
        skinSelectBackpack();
        skinSelectLegs();
    }

    public void PayForAll()
    {
        PurchaseConfirmation.SetActive(true);
        if (Pay(AllCost))
        {
            if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Helmet.name))
            {
                PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Helmet.name, 1);
                UIHelmet.sprite = Helmet;
            }

            if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Gauntlets1.name))
            {
                PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Gauntlets1.name, 1);
                UIGauntlets.sprite = Gauntlets;

                UIGauntlets2.sprite = Gauntlets2;
            }

            if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Chest.name))
            {
                PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Chest.name, 1);
                UIChest.sprite = Chestplate;
            }

            if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Backpack.name))
            {
                PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Backpack.name, 1);
                UIBackpack.sprite = Backpack;
            }

            if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Pants.name))
            {
                PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Pants.name, 1);
                UIPants.sprite = Pants;
            }
            CloseConfirmation();
            save.Save();
        }
    }

    public void ButtonConfirmAll()
    {
        PurchaseConfirmation.SetActive(true);
        Confirmed.onClick.AddListener(PayForAll);
    }


    public void CheckPayAll()
    {
        int i = 0;
        int total = 0;

        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Helmet.name))
        {
            i++;
            total += HelmetCost;
        }
        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Gauntlets1.name))
        {
            i++;
            total += GauntletsCost;
        }
        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Chest.name))
        {
            i++;
            total += ChestplateCost;
        }
        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Backpack.name))
        {
            i++;
            total += BackpackCost;
        }
        if (!PlayerPrefs.HasKey(currSkin.GetComponent<SkinHolder>().Pants.name))
        {
            i++;
            total += PantsCost;
        }

        if (i == 0)
            AllBuy.SetActive(false);
        else
        {
            AllBuy.SetActive(true);
            AllBuy.GetComponentInChildren<Text>().text = total.ToString();
            AllCost = total;
        }
    }

    #endregion
}
