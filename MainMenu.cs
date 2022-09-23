using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class MainMenu : MonoBehaviour
{
    #region Variables
    public Transform[] LevelPanels;

    public Transform[] snapContent;
    public int[] snapReqs;

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;
    private Vector3 desiredMenuPos;

    public GameObject fuelCount;
    public GameObject buckCount;

    public int totalFuel = 0;

    public GameObject MainMenuPanel;
    public GameObject GalaxySelectMenu;
    public GameObject[] LevelSelectMenus;
    public GameObject SkinsMenu;
    public GameObject RocketsMenu;
    public GameObject SlotsMenu;
    public GameObject ShopMenu;

    public GameObject Settings;
    public GameObject MusicButton;
    public GameObject MusicMuteButton;
    public GameObject SoundButton;
    public GameObject SoundMuteButton;
    public GameObject VibrateOn;
    public GameObject VibrateOff;

    public static bool MusicMuted;
    public static bool AllMuted;

    public int[] GalaxySelectCans = new int[] { 0, 0, 0, 0, 0, 0 };
    public int[] GalaxySelectStars = new int[] { 0, 0, 0, 0, 0, 0 };

    public Transform allSkins;
    public Transform Rockets;

    public GameObject RewardMenu;
    public Image RewardImage;
    public Text RewardText;
    public GameObject GDPRMenu;
    public Saving save;
    private bool consentValue;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        save.Load();
        ShopMenu.SetActive(true);
        ShopMenu.SetActive(false);
        RateGame.Instance.ShowRatePopup();

        GleyDailyRewards.Calendar.Show();
        if (TimeSpan.Compare(GleyDailyRewards.Calendar.GetRemainingTimeSpan(), new TimeSpan(0, 0, 0)) != 0)
            GameObject.FindGameObjectWithTag("Calendar").GetComponent<GleyDailyRewards.CalendarPopup>().InstantClose();

        GleyDailyRewards.Calendar.AddClickListener(CalendarButtonClicked);

        MainMenuPanel.SetActive(true);
        GalaxySelectMenu.SetActive(false);
        SkinsMenu.SetActive(false);
        RocketsMenu.SetActive(false);
        foreach (GameObject panels in LevelSelectMenus)
            panels.SetActive(false);

        // To Levels
        InitLevel();
        InitSnap();
        Advertisements.Instance.Initialize();
        if (!PlayerPrefs.HasKey("GalaxyBucks"))
        {
            PlayerPrefs.SetInt("GalaxyBucks", 0);
            PlayerPrefs.SetInt("MuteAll", 0);
            PlayerPrefs.SetInt("MuteSong", 0);
            PlayerPrefs.SetInt("Vibrate", 0);
            PlayerPrefs.SetInt("ExtraCoin", 0);
            PlayerPrefs.SetInt("PlasmaAmmo", 0);
            PlayerPrefs.SetInt("AdCount", 0);
            GDPROpen();
        }
        if (PlayerPrefs.GetInt("MuteSong") == 1)
            MuteSong();
        if (PlayerPrefs.GetInt("MuteAll") == 1)
            MuteAll();
        if (PlayerPrefs.GetInt("Vibrate") == 1)
            VibrationOff();


        Appodeal.initialize("4830f302a0ec2d007db7295ff9405ab353eb6bb0b09291e9", Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentValue);
    }

    private void Update()
    {
        buckCount.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("GalaxyBucks").ToString();
    }

    public void GDPRTrue() {
        consentValue = true;
        Advertisements.Instance.SetUserConsent(true);
        GDPRClose();
    }

    public void GDPRFalse()
    {
        consentValue = false;
        Advertisements.Instance.SetUserConsent(false);
        GDPRClose();
    }

    public void GDPROpen()
    {
        GDPRMenu.SetActive(true);
    }

    public void GDPRClose()
    {
        GDPRMenu.SetActive(false);
    }

    #endregion

    #region Calendar
    public void CalendarButtonClicked(int day, int rewardValue, Sprite rewardSprite) {
        UnityEngine.Debug.Log("Day " + day);
        UnityEngine.Debug.Log("Value " + rewardValue);
        UnityEngine.Debug.Log("Item " + rewardSprite.name);

        if (rewardSprite.name == "GalaxyBuck")
        {
            int current = PlayerPrefs.GetInt("GalaxyBucks");
            PlayerPrefs.SetInt("GalaxyBucks", current + rewardValue);
            OpenRewardMenu(rewardSprite, "You Got <color=blue>" + rewardValue + "</color> <color=fuchsia>Galaxy Bucks</color>!");
        }
        else if (rewardSprite.name == "JerryCan_Adobe")
        {
            int current = PlayerPrefs.GetInt("ExtraCoin");
            PlayerPrefs.SetInt("ExtraCoin", current + rewardValue);
            OpenRewardMenu(rewardSprite, "You Got <color=blue>" + rewardValue + "</color> <color=yellow>Rocket Fuel</color>!");
            InitLevel();
        }
        else if (rewardSprite.name == "PlasmaButton")
        {
            int ammo = PlayerPrefs.GetInt("PlasmaAmmo");
            PlayerPrefs.SetInt("PlasmaAmmo", ammo + rewardValue);

            OpenRewardMenu(rewardSprite, "You Got <color=blue>" + rewardValue + "</color> ammo for your <color=aqua>Plasma Launcher</color>!");
        }
        else if (rewardSprite.name == "HelmetButton")
        {
            SkinHolder sh = randomSkinSelect();
            while (PlayerPrefs.HasKey(sh.Helmet.name))
                sh = randomSkinSelect();
            PlayerPrefs.SetInt(sh.Helmet.name, 1);

            OpenRewardMenu(sh.Helmet, "You Unlocked a New <color=aqua>Helmet</color>!");
        }
        else if (rewardSprite.name == "BackpackButton")
        {
            SkinHolder sh = randomSkinSelect();
            while (PlayerPrefs.HasKey(sh.Backpack.name))
                sh = randomSkinSelect();
            PlayerPrefs.SetInt(sh.Backpack.name, 1);

            OpenRewardMenu(sh.Backpack, "You Unlocked a New <color=purple>Backpack</color>!");
        }
        else if (rewardSprite.name == "ChestButton")
        {
            SkinHolder sh = randomSkinSelect();
            while (PlayerPrefs.HasKey(sh.Chest.name))
                sh = randomSkinSelect();
            PlayerPrefs.SetInt(sh.Chest.name, 1);

            OpenRewardMenu(sh.Chest, "You Unlocked a New <color=red>Chestplate</color>!");
        }
        else if (rewardSprite.name == "GauntletsButton")
        {
            SkinHolder sh = randomSkinSelect();
            while (PlayerPrefs.HasKey(sh.Gauntlets1.name))
                sh = randomSkinSelect();
            PlayerPrefs.SetInt(sh.Gauntlets1.name, 1);

            OpenRewardMenu(sh.Gauntlets2, "You Unlocked New <color=orange>Gauntlets</color>!");
        }
        else if (rewardSprite.name == "PantsButton")
        {
            SkinHolder sh = randomSkinSelect();
            while (PlayerPrefs.HasKey(sh.Pants.name))
                sh = randomSkinSelect();
            PlayerPrefs.SetInt(sh.Pants.name, 1);

            OpenRewardMenu(sh.Pants, "You Unlocked New <color=navy>Pants</color>!");
        }
        else if (rewardSprite.name == "AllButtonTest")
        {
            SkinHolder sh = randomSkinSelect();
            while (PlayerPrefs.HasKey(sh.Helmet.name))
                sh = randomSkinSelect();
            PlayerPrefs.SetInt(sh.Helmet.name, 1);
            PlayerPrefs.SetInt(sh.Chest.name, 1);
            PlayerPrefs.SetInt(sh.Backpack.name, 1);
            PlayerPrefs.SetInt(sh.Gauntlets1.name, 1);
            PlayerPrefs.SetInt(sh.Pants.name, 1);

            OpenRewardMenu(sh.Helmet, "You Won a new Skin Set!");
        }
        else if (rewardSprite.name == "RocketButton") {
            Transform cr = randomRocketSelect();
            while (PlayerPrefs.HasKey(cr.name))
                cr = randomRocketSelect();
            
            PlayerPrefs.SetInt(cr.name, 1);

            OpenRewardMenu(cr.GetComponent<Image>().sprite, "You Unlocked a New <color=cyan>Rocket</color>!");
        }
        else
        {
            UnityEngine.Debug.Log("Not Implemented!!!");
        }
        GameObject.FindGameObjectWithTag("Calendar").GetComponent<GleyDailyRewards.CalendarPopup>().ClosePopup();
    }

    private SkinHolder randomSkinSelect() {
        int index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
        if (index > allSkins.childCount / 2)
            index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
        return allSkins.GetChild(index).GetComponent<SkinHolder>();
        //PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Gauntlets1.name, 1);
    }

    public Transform randomRocketSelect() {
        int index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
        if (index > Rockets.childCount / 2)
            index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
        return Rockets.GetChild(index);
    }

    public void OpenRewardMenu(Sprite im, String t) {
        RewardMenu.SetActive(true);
        RewardImage.sprite = im;
        resizeImage(im);
        RewardText.text = t;
    }

    public void resizeImage(Sprite im)
    {
        float x;
        float y;
        float change;
        float ogHeight = RewardImage.rectTransform.sizeDelta.y;

        RewardImage.sprite = im;
        RewardImage.SetNativeSize();
        x = RewardImage.rectTransform.sizeDelta.x;
        y = RewardImage.rectTransform.sizeDelta.y;

        change = ogHeight / y;

        RewardImage.rectTransform.sizeDelta = new Vector2(x * change, y * change);

    }

    public void CloseRewardMenu() {
        RewardMenu.SetActive(false);
    }

    #endregion

    #region Init

    private void InitLevel()
    {
        int i = 0;
        int j = 0;
        int panelFuel;
        int panelStars;
        foreach (Transform levelPanel in LevelPanels)
        {
            panelFuel = 0;
            panelStars = 0;
            foreach (Transform t in levelPanel)
            {
                int currentIndex = i;
                Button b = t.GetComponent<Button>();
                b.onClick.AddListener(() => OnLevelSelect(currentIndex));

                string levelNumber = (i + 1).ToString();
                if (!(t.GetComponentInChildren<Text>().text is null))
                    t.GetComponentInChildren<Text>().text = levelNumber;

                //Returns number of stars recieved on that level
                panelStars += PlayerPrefs.GetInt("Level " + levelNumber);

                totalFuel += PlayerPrefs.GetInt("coin " + levelNumber);
                panelFuel += PlayerPrefs.GetInt("coin " + levelNumber);

                i++;
            }
            GalaxySelectCans[j] = panelFuel;
            GalaxySelectStars[j] = panelStars;
            j++;
        }
        totalFuel += PlayerPrefs.GetInt("ExtraCoin");
        fuelCount.GetComponentInChildren<Text>().text = totalFuel.ToString();
    }

    private void InitSnap()
    {

        int i = 0;
        foreach (Transform content in snapContent)
        {
            int fuelReq = snapReqs[i];
            if (fuelReq == -1)
            {
                content.GetComponentInChildren<Text>().text = "Coming Soon";
                content.GetChild(1).gameObject.SetActive(false);
                content.GetChild(2).gameObject.SetActive(false);
                content.GetChild(3).gameObject.SetActive(false);
                content.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            }
            else if (fuelReq > totalFuel)
            {
                content.GetComponentInChildren<Text>().text = fuelReq + " Fuel Required\n\n" + totalFuel + "/" + fuelReq;
                content.GetChild(1).gameObject.SetActive(false);
                content.GetChild(2).gameObject.SetActive(false);
                content.GetChild(3).gameObject.SetActive(false);
                content.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            }
            else
            {
                content.GetComponentInChildren<Text>().text = "Galaxy " + (i + 1);
                content.GetChild(1).gameObject.SetActive(true);
                content.GetChild(2).gameObject.SetActive(true);
                content.GetChild(3).gameObject.SetActive(true);
                content.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                content.GetChild(2).GetComponentInChildren<Text>().text = GalaxySelectStars[i].ToString() + "/75";
                content.GetChild(3).GetComponentInChildren<Text>().text = GalaxySelectCans[i].ToString() + "/25";
            }
            i++;
        }
    }

    #endregion

    #region Buttons


    private void Haptics()
    {
        AudioManager.instance.Play("Button1");
        Vibration.CreateOneShot(10, 50);
    }

    public void ShopClick() {
        ShopMenu.SetActive(true);
        Haptics();
    }

    public void SettingsClick() { 
        Settings.SetActive(true);
        Haptics();
    }

    public void VibrationOn() {
        PlayerPrefs.SetInt("Vibrate", 0);
        VibrateOff.SetActive(false);
        VibrateOn.SetActive(true);
    }

    public void VibrationOff() {
        PlayerPrefs.SetInt("Vibrate", 1);
        VibrateOn.SetActive(false);
        VibrateOff.SetActive(true);
    }

    public void MuteAll() {
        AudioManager.instance.MuteAll();
        SoundButton.SetActive(false);
        SoundMuteButton.SetActive(true);
        PlayerPrefs.SetInt("MuteAll", 1);
    }

    public void UnmuteAll()
    {
        AudioManager.instance.UnmuteAll();
        SoundButton.SetActive(true);
        SoundMuteButton.SetActive(false);
        AllMuted = false;
        PlayerPrefs.SetInt("MuteAll", 0);
    }

    public void MuteSong()
    {
        AudioManager.instance.MuteSong();
        MusicButton.SetActive(false);
        MusicMuteButton.SetActive(true);
        MusicMuted = true;
        PlayerPrefs.SetInt("MuteSong", 1);
    }

    public void UnmuteSong()
    {
        AudioManager.instance.UnmuteSong();
        MusicButton.SetActive(true);
        MusicMuteButton.SetActive(false);
        MusicMuted = false;
        PlayerPrefs.SetInt("MuteSong", 0);
    }

    public void CloseSettings() {
        Settings.SetActive(false);
    }

    private void OnLevelSelect(int currentIndex)
    {
        Haptics();
        string level = "Level ";
        int levelNum = currentIndex + 1;
        level = level + levelNum.ToString();
        UnityEngine.Debug.Log("Selecting " + level);

        SceneManager.LoadScene(level);
    }

    public void onSlotButtonClick() {
        SlotsMenu.SetActive(true);
        SkinsMenu.SetActive(false);
        RocketsMenu.SetActive(false);
    }
    
    public void OnLevelSelectClick(int i)
    {
        Haptics();
        LevelSelectMenus[i].SetActive(true);
        GalaxySelectMenu.SetActive(false);
    }

    public void OnSkinSelectClick()
    {
        Haptics();
        SkinsMenu.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OnRocketsSelectClick()
    {
        Haptics();
        RocketsMenu.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OnGalaxySelectClick()
    {
        Haptics();
        GalaxySelectMenu.SetActive(true);
        MainMenuPanel.SetActive(false);
        foreach (GameObject panels in LevelSelectMenus)
            panels.SetActive(false);
    }

    public void OnHomeClick()
    {
        Haptics();
        MainMenuPanel.SetActive(true);
        GalaxySelectMenu.SetActive(false);
        SlotsMenu.SetActive(false);
        SkinsMenu.SetActive(false);
        RocketsMenu.SetActive(false);
        ShopMenu.SetActive(false);
        foreach (GameObject panels in LevelSelectMenus)
            panels.SetActive(false);

    }

    public void OnPlayClick()
    {
        Haptics();
        string level = "1";
        foreach (Transform levelPanel in LevelPanels)
            foreach (Transform t in levelPanel)
                if (t.GetComponent<LevelSelect>().unlocked == true)
                {
                    level = t.GetComponentInChildren<Text>().text;
                }
                else
                {
                    SceneManager.LoadScene("Level " + level);
                }
    }
    #endregion
}
