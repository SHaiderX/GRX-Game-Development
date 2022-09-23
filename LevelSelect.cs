using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [HideInInspector]
    public bool unlocked = false;
    [HideInInspector]
    public GameObject[] stars;
    [HideInInspector]
    public GameObject emptyCoin;
    [HideInInspector]
    public GameObject coin;

    [HideInInspector]
    public string prevLevel;
    [HideInInspector]
    public int x;
    [HideInInspector]
    public string curLevel;

    void Awake()
    {
        curLevel = gameObject.GetComponentInChildren<Text>().text;
    }

    private void Update()
    {
        curLevel = gameObject.GetComponentInChildren<Text>().text;

        stars[0].gameObject.SetActive(false);
        stars[1].gameObject.SetActive(false);
        stars[2].gameObject.SetActive(false);
        coin.gameObject.SetActive(false);


        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelImage()
    {
        if (!unlocked)
        {
            GetComponent<Button>().interactable = false;
            stars[0].gameObject.SetActive(false);
            stars[1].gameObject.SetActive(false);
            stars[2].gameObject.SetActive(false);
            emptyCoin.gameObject.SetActive(false);
        }
        else
        {
            GetComponent<Button>().interactable = true;
            stars[0].gameObject.SetActive(false);
            stars[1].gameObject.SetActive(false);
            stars[2].gameObject.SetActive(false);
            coin.gameObject.SetActive(false);
            emptyCoin.gameObject.SetActive(true);

            if (PlayerPrefs.GetInt("Level " + curLevel) == -1)
            {
                stars[0].gameObject.SetActive(false);
                stars[1].gameObject.SetActive(false);
                stars[2].gameObject.SetActive(false);
                coin.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("Level " + curLevel) == 1)
            {
                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(false);
                stars[2].gameObject.SetActive(false);

                if (PlayerPrefs.GetInt("coin " + curLevel) == 1)
                    coin.gameObject.SetActive(true);
                else
                    coin.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("Level " + curLevel) == 2)
            {
                stars[0].gameObject.SetActive(false);
                stars[1].gameObject.SetActive(true);
                stars[2].gameObject.SetActive(false);

                if (PlayerPrefs.GetInt("coin " + curLevel) == 1)
                    coin.gameObject.SetActive(true);
                else
                    coin.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("Level " + curLevel) == 3)
            {
                stars[0].gameObject.SetActive(false);
                stars[1].gameObject.SetActive(false);
                stars[2].gameObject.SetActive(true);

                if (PlayerPrefs.GetInt("coin " + curLevel) == 1)
                    coin.gameObject.SetActive(true);
                else
                    coin.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateLevelStatus()
    {
        x = int.Parse(curLevel) - 1;
        prevLevel = x.ToString();
        if (PlayerPrefs.GetInt("Level " + prevLevel) > 0 || PlayerPrefs.GetInt("Level " + prevLevel) == -1)
            unlocked = true;
    }
}
