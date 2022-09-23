using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    //www.youtube.com/watch?v=sZhhfOH0Q3Y

    private CanvasGroup fade;
    private float loadTime;
    private float minLogoTime = 2.0f;

    private void Start()
    {
        fade = FindObjectOfType<CanvasGroup>();

        fade.alpha = 1;

        if (Time.time < minLogoTime)
            loadTime = minLogoTime;
        
        else 
            loadTime = Time.time;
       
    }

    private void Update() 
    {
        if (Time.time < minLogoTime)
            fade.alpha = 1 - Time.time;

        if (Time.time > minLogoTime && loadTime != 0)
        {
            fade.alpha = Time.time - minLogoTime;
            if (fade.alpha >= 1)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

}
