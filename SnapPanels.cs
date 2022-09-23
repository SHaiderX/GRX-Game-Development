using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapPanels : MonoBehaviour
{
    string fuelReq;
    public int level;
    
    void Start()
    {
        fuelReq = PlayerPrefs.GetString("Snap" + level);

        UnityEngine.Debug.Log("t:" + PlayerPrefs.GetString("Snap" + level));
        gameObject.GetComponentInChildren<Text>().text = fuelReq;
    }

    
}
