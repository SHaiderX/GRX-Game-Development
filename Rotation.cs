using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{

    Touch touch;
    public float offset;
    //Legs of the player
    public GameObject player;
    public Vector3 characterScale;

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (!AllMenus.screenOn)
            {
                Rotate();
            }
                     
        }
    }

        void Rotate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        difference = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        Vector3 selfPos = transform.position;
        characterScale = player.transform.localScale;
        Vector3 selfScale = transform.localScale;

        // if -90 < rotZ < 90 and x is negative
        if (rotZ > -90 & rotZ < 90 & characterScale.x < 0)
        {
            characterScale.x = -characterScale.x;
            player.transform.localScale = characterScale;
            selfScale.x = -selfScale.x;
            transform.localScale = selfScale;
            offset = -6;
            selfPos.x = selfPos.x - 85.14f;
            transform.position = selfPos;
        }
        else if ((rotZ < -90 || rotZ > 90) & characterScale.x > 0)
        {
            characterScale.x = -characterScale.x;
            player.transform.localScale = characterScale;
            selfScale.x = -selfScale.x;
            transform.localScale = selfScale;
            offset = 186;
            selfPos.x = selfPos.x + 85.14f;
            transform.position = selfPos;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }
}
