using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GleyMobileAds;


public class AllMenus : MonoBehaviour
{

    [HideInInspector]
    public static bool screenOn = false;
    private bool showedStars = false;

    [HideInInspector]
    public GameObject OverScreen;
    [HideInInspector]
    public GameObject VictoryScreen;
    [HideInInspector]
    public GameObject NotEnough;
    [HideInInspector]
    public GameObject SlotMenu;
    [HideInInspector]
    public GameObject SlotButton;
    //Number of levels before ad shows.
    private int numAds = 4;

    //Coins required to skip/plasma
    [HideInInspector]
    public GameObject SkipCoinNum;
    [HideInInspector]
    public GameObject PlasmaCoinNum;
    [HideInInspector]
    public GameObject AmmoCoinNum;

    [HideInInspector]
    public GameObject PlasmaScreen;
    [HideInInspector]
    public GameObject SkipScreen;
    [HideInInspector]
    public GameObject RefillScreen;
    [HideInInspector]
    public GameObject Weapons;
    [HideInInspector]
    public Text NotEnoughText;

    [HideInInspector]
    public static int TotalGalaxyBucks = 0;
    [HideInInspector]
    public int GalaxyBucks;
    //And you also need a variable that holds the increasing score number, let's call it display score
    [HideInInspector]
    public int displayBucks;
    //Variable for the UI Text that will show the score
    [HideInInspector]
    public Text bucksUI;

    [HideInInspector]
    public GameObject[] stars;
    [HideInInspector]
    public GameObject coin;
    private int currentStarNum = 0;
    private int coinNum = 0;

    [HideInInspector]
    public GameObject PlasmaLauncherButton;
    [HideInInspector]
    public GameObject SkipButton;

    private string curScene;
    private string levelNum;
    public static GameObject currWeapon;

    public int PlasmaCost = 800;
    public int SkipCost = 500;
    public int RefillCost = 600;

    [HideInInspector]
    public Text PlasmaCostShow;
    [HideInInspector]
    public Text SkipCostShow;
    [HideInInspector]
    public Text RefillCostShow;
    [HideInInspector]
    public Text PlasmaAmmmo;

    public SpriteRenderer SpaceBackground;
    public Sprite[] SpaceSprites;
    private Saving save;
    private GameObject saving;

    public GameObject plasmaIntroScreen;

    [HideInInspector]
    public static int GalaxyNum;

    void Awake()
    {
        saving = GameObject.Find("Saving");

        save = saving.GetComponent<Saving>();

        for (int i = 0; i < 3; i++)
        {
            if (Weapons.transform.GetChild(i).gameObject.activeInHierarchy)
                currWeapon = Weapons.transform.GetChild(i).gameObject;
        }

        PlasmaCostShow.text = PlasmaCost.ToString();
        SkipCostShow.text = SkipCost.ToString();
        RefillCostShow.text = RefillCost.ToString();
        PlasmaAmmmo.text = PlayerPrefs.GetInt("PlasmaAmmo").ToString();

        allFalse();
        //PlasmaLauncherButton.SetActive(true);
        //SkipButton.SetActive(true);

        curScene = SceneManager.GetActiveScene().name;
        levelNum = curScene.Split(' ')[1];
        PlayerPrefs.SetInt("coin " + levelNum, 0);

        GalaxyNum = (int)Mathf.Ceil((int.Parse(levelNum) - 1) / 25);

        //Every 25 levels is a new galaxy
        SpaceBackground.sprite = SpaceSprites[GalaxyNum];

    }

    void Start()
    {
        TotalGalaxyBucks = PlayerPrefs.GetInt("GalaxyBucks");
        displayBucks = 0;
        GalaxyBucks = 0;
    }

    // Canvases 
    public void Victory(int ammo, int goldAmmo, int originalAmmo)
    {
        if (GameObject.FindWithTag("Alien") == null && GameObject.FindWithTag("Explosion") == null)
        {
            screenOn = true;
            VictoryScreen.SetActive(true);
            PlasmaLauncherButton.SetActive(false);
            SkipButton.SetActive(false);
            ShowStars(ammo, goldAmmo, originalAmmo);
            save.Save();
        }
        else
        {
            screenOn = false;
        }
    }

    public void checkCan()
    {
        if (GameObject.Find("Can").GetComponent<Coin>().collected)
        {
            coin.SetActive(true);
            coinNum = 1;
        }
        else
            coinNum = 0;
    }

    public void ShowStars(int ammo, int goldAmmo, int originalAmmo)
    {
        if (!showedStars)
            //3 Stars
            if (ammo >= originalAmmo - goldAmmo)
            {
                currentStarNum = 3;
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(true);
                showedStars = true;

                GalaxyBucks = Random.Range(25, 30);
            }

            //2 Stars
            else if (originalAmmo - goldAmmo > ammo && ammo >= originalAmmo - goldAmmo - 1)
            {
                currentStarNum = 2;
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                showedStars = true;

                GalaxyBucks = Random.Range(18, 22);
            }

            //1 Star
            else
            {
                currentStarNum = 1;
                stars[0].SetActive(true);
                showedStars = true;

                GalaxyBucks = Random.Range(7, 12);
            }

        checkCan();

        //For bucks
        if (currentStarNum > PlayerPrefs.GetInt(curScene))
        {
            //int x;
            GalaxyBucks -= 10 * PlayerPrefs.GetInt(curScene);
            if (GalaxyBucks < 0)
                GalaxyBucks = 0;

            TotalGalaxyBucks += GalaxyBucks;

            PlayerPrefs.SetInt("GalaxyBucks", TotalGalaxyBucks);

            StartCoroutine(ScoreUpdater());
        }

        //To see if coin is collected
        if (currentStarNum >= PlayerPrefs.GetInt(curScene))
        {
            PlayerPrefs.SetInt(curScene, currentStarNum);
            PlayerPrefs.SetInt("coin " + levelNum, coinNum);
        }
    }

    private IEnumerator ScoreUpdater()
    {
        PlayerPrefs.SetInt("bucks" + levelNum, currentStarNum);
        while (true)
        {
            if (displayBucks < GalaxyBucks)
            {
                displayBucks++; //Increment the display score by 1
                bucksUI.text = "+" + displayBucks.ToString(); //Write it to the UI
            }
            yield return new WaitForSeconds(0.03f); // How long to wait before showing next number (secs)
        }
    }

    private IEnumerator Defeat()
    {
        int i = 0;
        while (true)
        {
            if (i == 1) {

                screenOn = true;
                OverScreen.SetActive(true);
                PlasmaLauncherButton.SetActive(false);
                SkipButton.SetActive(false);
            }

            i++;
            yield return new WaitForSeconds(0.2f); // How long to wait before showing next number (secs)
        }
    }


    public void GameOver()
    {
        // if there is no rocket currently active, then game ends
        if (GameObject.FindWithTag("Rocket") == null && !Alien.isMoving && GameObject.FindWithTag("Alien") != null)
        {
            StartCoroutine(Defeat());
        }
    }

    public void updateCoinNum()
    {
        SkipCoinNum.GetComponent<Text>().text = PlayerPrefs.GetInt("GalaxyBucks").ToString();
        PlasmaCoinNum.GetComponent<Text>().text = PlayerPrefs.GetInt("GalaxyBucks").ToString();
        AmmoCoinNum.GetComponent<Text>().text = PlayerPrefs.GetInt("GalaxyBucks").ToString();
    }

    private void Haptics()
    {
        Vibration.CreateOneShot(10, 50);
    }

    // Buttons
    public void onPlasmaButtonClick()
    {
        AudioManager.instance.Play("Button2");
        screenOn = true;
        updateCoinNum();
        PlasmaScreen.SetActive(true);
    }

    public void onCloseClick()
    {
        StopAllCoroutines();
        StartCoroutine(ScreenOff());
        PlasmaScreen.SetActive(false);
        SkipScreen.SetActive(false);
        NotEnough.SetActive(false);
        RefillScreen.SetActive(false);
    }


    //Agree to watch video.
    public void onPlasmaYesClick(){
        Advertisements.Instance.ShowRewardedVideo(PlasmaAdCheck);
    }

    private void PlasmaAdCheck(bool completed) {
        if (completed)
            givePlasma();
    }

    public void onPlasmaCoinUse()
    {
        if (Pay(PlasmaCost))
        {
            givePlasma();
        }
    }

    public void onPlasmaAmmoUse()
    {
        if (PlayerPrefs.GetInt("PlasmaAmmo") > 0)
        {
            int ammo = PlayerPrefs.GetInt("PlasmaAmmo");
            PlayerPrefs.SetInt("PlasmaAmmo", ammo - 1);
            PlasmaAmmmo.text = PlayerPrefs.GetInt("PlasmaAmmo").ToString();
            givePlasma();
            save.Save();
        }
        else
            AudioManager.instance.Play("ButtonFail");
    }

    private void givePlasma() {
        AudioManager.instance.Play("ButtonSuccess");
        StartCoroutine(ScreenOff());
        Weapons.transform.GetChild(0).gameObject.SetActive(false);
        Weapons.transform.GetChild(1).gameObject.SetActive(true);
        OverScreen.SetActive(false);
        PlasmaScreen.SetActive(false);
    }

    public void ClosePlasmaIntro() {
        plasmaIntroScreen.SetActive(false);
        givePlasma();
    }

    public void onSkipClick()
    {
        AudioManager.instance.Play("Button2");
        screenOn = true;
        SkipScreen.SetActive(true);
        updateCoinNum();
    }

    public void onSkipYesClick()
    {
        Advertisements.Instance.ShowRewardedVideo(SkipAdCheck);
    }

    private void SkipAdCheck(bool completed)
    {
        if (completed)
            Skip();
    }

    public void onSkipCoinUse()
    {
        if (Pay(SkipCost))
        {
            Skip();
        }
    }

    private void Skip() {
        AudioManager.instance.Play("ButtonSuccess");
        screenOn = false;
        PlayerPrefs.SetInt(curScene, -1);
        OnNextClick();
        SkipScreen.SetActive(false);
    }

    IEnumerator ScreenOff()
    {
        yield return new WaitForSeconds(0.05f);
        screenOn = false;

    }

    public bool Pay(int amount)
    {
        if (amount <= PlayerPrefs.GetInt("GalaxyBucks"))
        {
            PlayerPrefs.SetInt("GalaxyBucks", PlayerPrefs.GetInt("GalaxyBucks") - amount);
            UnityEngine.Debug.Log("Success!! " + amount + "charged, Remaining: " + PlayerPrefs.GetInt("GalaxyBucks"));
            return true;
        }
        UnityEngine.Debug.Log("Not Enough Galaxy Bucks");
        AudioManager.instance.Play("ButtonFail");
        PlasmaScreen.SetActive(false);
        SkipScreen.SetActive(false);
        NotEnoughText.text = "Not Enough Galaxy Bucks.\n " + amount.ToString() + " required.\n Have: " + PlayerPrefs.GetInt("GalaxyBucks").ToString();
        NotEnough.SetActive(true);
        return false;
    }

    public void OnAmmoRefill()
    {
        screenOn = true;
        RefillScreen.SetActive(true);
        AudioManager.instance.Play("Button2");
        updateCoinNum();
    }

    public void OnRefillYes()
    {
        Advertisements.Instance.ShowRewardedVideo(RefillAdCheck);
    }

    private void RefillAdCheck(bool completed)
    {
        if (completed)
            Refill();
        else
            UnityEngine.Debug.Log("Bruh");
    }

    public void OnRefillCoin()
    {
        if (Pay(RefillCost))
        {
            Refill();
        }
    }

    private void Refill() {
        AudioManager.instance.Play("ButtonSuccess");
        StartCoroutine(ScreenOff());
        OverScreen.SetActive(false);
        PlasmaLauncherButton.SetActive(true);
        RefillScreen.SetActive(false);
        SkipButton.SetActive(true);
        currWeapon.GetComponent<Shooting>().RefillAmmo();
    }

    public void OnHomeClick()
    {
        allFalse();
        AudioManager.instance.Play("Retry");
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRetryClick()
    {
        allFalse();
        AudioManager.instance.Play("Retry");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextClick()
    {
        allFalse();
        int level = int.Parse(levelNum);
        string nextLevel = (level + 1).ToString();

        SceneManager.LoadScene("Level " + nextLevel);
        AudioManager.instance.Play("NextLevel");

        if (PlayerPrefs.GetInt("NoAds") == 0) {
            PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);
            if (PlayerPrefs.GetInt("AdCount") == numAds)
            {
                Advertisements.Instance.ShowInterstitial();
                PlayerPrefs.SetInt("AdCount", 0);
            }
        }

    }

    public void onSlotButtonClick() {
        SlotMenu.SetActive(true);
        SlotButton.SetActive(false);
    }

    public void onSlotBack() {
        SlotMenu.SetActive(false);
        SlotButton.SetActive(true);
    }

    public void allFalse()
    {
        screenOn = false;
        showedStars = false;
        OverScreen.SetActive(false);
        VictoryScreen.SetActive(false);
        PlasmaScreen.SetActive(false);
        SkipScreen.SetActive(false);
    }

}
