using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [Range(-2f, 2f)]
    public float rotSpeed = 1f;


    void OnEnable()
    {
        StartCoroutine(Spin());
    }

    void OnDisable(){
        StopAllCoroutines();
    }

    private IEnumerator Spin()
    {
        while (true)
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            transform.eulerAngles.z - rotSpeed
            );

            yield return new WaitForSeconds(0f);
        }
    }
}
