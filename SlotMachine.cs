// Simple Scroll-Snap
// Version: 1.0.0
// Author: Daniel Lochner

using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DanielLochner.Assets.SimpleScrollSnap
{
    public class SlotMachine : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        protected SimpleScrollSnap[] slots;
        public Transform[] content;

        public GameObject spinButton;
        public GameObject stopButton;

        public Transform allSkins;
        public Transform Rockets;

        public GameObject RewardMenu;
        public Image RewardImage;
        public Text RewardText;

        public Sprite buckSprite;
        public Sprite canSprite;
        public Sprite PlasmaSprite;
        public GameObject NoConnScreen;
        public Saving save;
        #endregion

        #region Methods


        IEnumerator CheckInternetConnection(Action<bool> action)
        {
            UnityWebRequest request = new UnityWebRequest("http://google.com");
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                action(false);
            }
            else
            {
                action(true);
            }
        }

        public void onClick() {
            //Check if device is connected to WiFi
            StartCoroutine(CheckInternetConnection(isConnected =>
            {
                if (isConnected)
                {
                    //Debug.Log("Internet Available! Showing Ad.");
                    Advertisements.Instance.ShowRewardedVideo(SlotAdCheck);
                }
                else
                {
                    //Show "No Connection" Screen
                    NoConnScreen.SetActive(true);
                }
            }));
        }

        public void closeNoConnScreen()
        {
            NoConnScreen.SetActive(false);
        }


        private void SlotAdCheck(bool completed)
        {
            if (completed)
                OnSpin();
        }

        public void OnSpin()
        {
            foreach (SimpleScrollSnap slot in slots)
            {
                slot.AddVelocity(UnityEngine.Random.Range(1000, 6000) * Vector2.up);
                AudioManager.instance.Play("SlotMachine");
                spinButton.SetActive(false);
                stopButton.SetActive(true);
            }
        }

        public void onStop()
        {
            AudioManager.instance.Stop("SlotMachine");
            AudioManager.instance.Play("SlotStop");
            spinButton.SetActive(true);
            stopButton.SetActive(false);
        }

        public void GetSlots() {
            StartCoroutine(GetAllSlots());
        }

        private IEnumerator GetAllSlots()
        {
            int i = 0;
            while (true)
            {
                i++;
                if (i == 4)
                {
                    CheckSimilars(content[0].GetChild(slots[0].CurrentPanel).name, content[0].GetChild(slots[1].CurrentPanel).name,
                        content[0].GetChild(slots[2].CurrentPanel).name);
                }

                yield return new WaitForSeconds(0.3f);
            }
        }

        private void GiveReward(string item, int Score) {
            UnityEngine.Debug.Log(item + ": " + Score);

            if (Score == 1) {
                int rewardValue = UnityEngine.Random.Range(1, 50);
                int current = PlayerPrefs.GetInt("GalaxyBucks");
                PlayerPrefs.SetInt("GalaxyBucks", current + rewardValue);

                OpenRewardMenu(buckSprite, "You Got <color=blue>" + rewardValue + "</color> <color=fuchsia>Galaxy Bucks</color>!", Score);
            }
            if (Score == 2)
            {
                if (item == "GalaxyBuck")
                {
                    int rewardValue = UnityEngine.Random.Range(100, 500);
                    int current = PlayerPrefs.GetInt("GalaxyBucks");
                    PlayerPrefs.SetInt("GalaxyBucks", current + rewardValue);
                    OpenRewardMenu(buckSprite, "You Got <color=blue>" + rewardValue + "</color> <color=fuchsia>Galaxy Bucks</color>!", Score);
                }
                if (item == "Alien")
                {
                    SkinHolder sh = randomSkinSelect();
                    while (PlayerPrefs.HasKey(sh.Gauntlets1.name))
                        sh = randomSkinSelect();
                    PlayerPrefs.SetInt(sh.Gauntlets1.name, 1);

                    OpenRewardMenu(sh.Gauntlets2, "You Unlocked New <color=orange>Gauntlets</color>!", Score);
                }
                if (item == "Rocketship")
                {
                    Transform cr = randomRocketSelect();
                    while (PlayerPrefs.HasKey(cr.name))
                        cr = randomRocketSelect();

                    PlayerPrefs.SetInt(cr.name, 1);

                    OpenRewardMenu(cr.GetComponent<Image>().sprite, "You Unlocked a New <color=cyan>Rocket</color>!", Score);
                }
                if (item == "Planet")
                {
                    SkinHolder sh = randomSkinSelect();
                    while (PlayerPrefs.HasKey(sh.Backpack.name))
                        sh = randomSkinSelect();
                    PlayerPrefs.SetInt(sh.Backpack.name, 1);

                    OpenRewardMenu(sh.Backpack, "You Unlocked a New <color=purple>Backpack</color>!", Score);
                }
                if (item == "Satellite")
                {
                    SkinHolder sh = randomSkinSelect();
                    while (PlayerPrefs.HasKey(sh.Chest.name))
                        sh = randomSkinSelect();
                    PlayerPrefs.SetInt(sh.Chest.name, 1);

                    OpenRewardMenu(sh.Chest, "You Unlocked a New <color=red>Chestplate</color>!", Score);
                }
                if (item == "Fuel Can")
                {
                    int rewardValue = 1;
                    int current = PlayerPrefs.GetInt("ExtraCoin");
                    PlayerPrefs.SetInt("ExtraCoin", current + rewardValue);
                    OpenRewardMenu(canSprite, "You Got <color=blue>" + rewardValue + "</color> <color=yellow>Rocket Fuel</color>!", Score);
                }
                if (item == "Helmet")
                {
                    SkinHolder sh = randomSkinSelect();
                    while (PlayerPrefs.HasKey(sh.Helmet.name))
                        sh = randomSkinSelect();
                    PlayerPrefs.SetInt(sh.Helmet.name, 1);

                    OpenRewardMenu(sh.Helmet, "You Unlocked a New <color=aqua>Helmet</color>!", Score);
                }
                if (item == "Rocket")
                {
                    int increase = 1;
                    int ammo = PlayerPrefs.GetInt("PlasmaAmmo");
                    PlayerPrefs.SetInt("PlasmaAmmo", ammo + increase);

                    OpenRewardMenu(PlasmaSprite, "You Got <color=blue>" + increase + "</color> ammo for your <color=aqua>Plasma Launcher</color>!", Score);
                }

                if (item == "AlienB") {
                    SkinHolder sh = randomSkinSelect();
                    while (PlayerPrefs.HasKey(sh.Pants.name))
                        sh = randomSkinSelect();
                    PlayerPrefs.SetInt(sh.Pants.name, 1);

                    OpenRewardMenu(sh.Pants, "You Unlocked New <color=navy>Pants</color>!", Score);
                }
            }
            if (Score == 3)
            {
                if (item == "GalaxyBuck")
                {
                    int rewardValue = UnityEngine.Random.Range(1000, 1200);
                    int current = PlayerPrefs.GetInt("GalaxyBucks");
                    PlayerPrefs.SetInt("GalaxyBucks", current + rewardValue);
                    OpenRewardMenu(buckSprite, "You Got <color=blue>" + rewardValue + "</color> <color=fuchsia>Galaxy Bucks</color>!", Score);
                }
                if (item == "Alien")
                {
                    SkinHolder sh = LuckySkinSelect();
                    while (PlayerPrefs.HasKey(sh.Gauntlets1.name))
                        sh = LuckySkinSelect();
                    PlayerPrefs.SetInt(sh.Gauntlets1.name, 1);

                    OpenRewardMenu(sh.Gauntlets2, "You Unlocked New <color=orange>Gauntlets</color>!", Score);
                }
                if (item == "Rocketship")
                {
                    Transform cr = LuckyRocketSelect();
                    while (PlayerPrefs.HasKey(cr.name))
                        cr = LuckyRocketSelect();

                    PlayerPrefs.SetInt(cr.name, 1);

                    OpenRewardMenu(cr.GetComponent<Image>().sprite, "You Unlocked a New <color=cyan>Rocket</color>!", Score);
                }
                if (item == "Planet")
                {
                    SkinHolder sh = LuckySkinSelect();
                    while (PlayerPrefs.HasKey(sh.Backpack.name))
                        sh = LuckySkinSelect();
                    PlayerPrefs.SetInt(sh.Backpack.name, 1);

                    OpenRewardMenu(sh.Backpack, "You Unlocked a New <color=purple>Backpack</color>!", Score);
                }
                if (item == "Satellite")
                {
                    SkinHolder sh = LuckySkinSelect();
                    while (PlayerPrefs.HasKey(sh.Chest.name))
                        sh = LuckySkinSelect();
                    PlayerPrefs.SetInt(sh.Chest.name, 1);

                    OpenRewardMenu(sh.Chest, "You Unlocked a New <color=red>Chestplate</color>!", Score);
                }
                if (item == "Fuel Can")
                {
                    int rewardValue = UnityEngine.Random.Range(3, 5);
                    int current = PlayerPrefs.GetInt("ExtraCoin");
                    PlayerPrefs.SetInt("ExtraCoin", current + rewardValue);
                    OpenRewardMenu(canSprite, "You Got <color=blue>" + rewardValue + "</color> <color=yellow>Rocket Fuel</color>!", Score);
                }
                if (item == "Helmet")
                {
                    SkinHolder sh = LuckySkinSelect();
                    while (PlayerPrefs.HasKey(sh.Helmet.name))
                        sh = LuckySkinSelect();
                    PlayerPrefs.SetInt(sh.Helmet.name, 1);

                    OpenRewardMenu(sh.Helmet, "You Unlocked a New <color=aqua>Helmet</color>!", Score);
                }
                if (item == "Rocket")
                {
                    int increase = 3;
                    int ammo = PlayerPrefs.GetInt("PlasmaAmmo");
                    PlayerPrefs.SetInt("PlasmaAmmo", ammo + increase);

                    OpenRewardMenu(PlasmaSprite, "You Got <color=blue>" + increase + "</color> ammo for your <color=aqua>Plasma Launcher</color>!", Score);
                }
                if (item == "AlienB")
                {
                    SkinHolder sh = LuckySkinSelect();
                    while (PlayerPrefs.HasKey(sh.Pants.name))
                        sh = LuckySkinSelect();
                    PlayerPrefs.SetInt(sh.Pants.name, 1);

                    OpenRewardMenu(sh.Pants, "You Unlocked New <color=navy>Pants</color>!", Score);
                }
            }
            save.Save();
        }

        private void CheckSimilars(string r1, string r2, string r3) {

            if (r1 == r2)
            {
                if (r1 == r3)
                {
                    GiveReward(r1, 3);
                    return;
                }

                GiveReward(r1, 2);
                return;
            }

            if (r1 == r3)
            {
                GiveReward(r1, 2);
                return;
            }

            if (r3 == r2)
            {
                GiveReward(r3, 2);
                return;
            }

            GiveReward("None", 1);
            return;

        }

        private SkinHolder randomSkinSelect()
        {
            int index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
            if (index > allSkins.childCount / 2)
                index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
            if (index > allSkins.childCount / 2)
                index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
            if (index > allSkins.childCount / 2)
                index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
            return allSkins.GetChild(index).GetComponent<SkinHolder>();
            //PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Gauntlets1.name, 1);
        }
        
        private SkinHolder LuckySkinSelect()
        {
            int index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
            if (index < allSkins.childCount / 2)
                index = UnityEngine.Random.Range(1, allSkins.childCount - 1);
            return allSkins.GetChild(index).GetComponent<SkinHolder>();
            //PlayerPrefs.SetInt(currSkin.GetComponent<SkinHolder>().Gauntlets1.name, 1);
        }

        public Transform randomRocketSelect()
        {
            int index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
            if (index > Rockets.childCount / 2)
                index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
            if (index > Rockets.childCount / 2)
                index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
            if (index > Rockets.childCount / 2)
                index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
            return Rockets.GetChild(index);
        }

        public Transform LuckyRocketSelect()
        {
            int index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
            if (index < Rockets.childCount / 2)
                index = UnityEngine.Random.Range(1, Rockets.childCount - 1);
            return Rockets.GetChild(index);
        }

        public void OpenRewardMenu(Sprite im, string t, int score)
        {
            AudioManager.instance.Play("Slot" + score);
            RewardMenu.SetActive(true);
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

        public void CloseRewardMenu()
        {
            RewardMenu.SetActive(false);
        }
        #endregion
    }
}