using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMove : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 alienPos;

    [Range(0f, 1f)]
    public float speedModifier = 0.5f;

    private bool coroutineAllowed;

    void OnEnable()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    void Update() {
        if (coroutineAllowed)
            StartCoroutine(RouteStart(routeToGo));
    }

    private IEnumerator RouteStart(int routeNumber)
    {

        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            alienPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * Mathf.Pow(tParam, 2) * (1 - tParam) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = alienPos;
            
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        coroutineAllowed = true;

    }
}
