using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Destructible2D;

public class Destroyer : MonoBehaviour
{

    public static bool blackH = false;
    private string objName;
    private string sound;

    private float ogScale;

    //Script for objects that destroy other object on impact (Blackholes, the sun, or the zappers)
    void Start() {
        objName = gameObject.name;

        if (objName == "shutterstock_BlackHole")
            sound = "Blackhole_amb";

        else if (objName == "Sun")
            sound = "Sun_amb";

        else if (gameObject.CompareTag("Zapper"))
            sound = "Zap_amb";

        AudioManager.instance.Play(sound);
    }

    void OnDestroy()
    {
        AudioManager.instance.Stop(sound);
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        GameObject other = collision2D.gameObject;
        ogScale = other.transform.localScale.x;

        //Blackhole
        if (gameObject.name == "Circle" && other.name != "Line Renderer(Clone)") {
            //put it at the centre of the black hole.
            
            if (other.CompareTag("Rocket"))
                other.GetComponent<RocketControl>().enabled = false;
            if (other.CompareTag("Alien"))
                AudioManager.instance.Play("AlienFly1");

            //If asteroid piece hits black hole
            if (!((other.CompareTag("Rocket") || other.CompareTag("Alien"))))
            {
                other = collision2D.gameObject.transform.parent.gameObject;
                ogScale = other.transform.localScale.x;
                other.GetComponent<D2dPolygonCollider>().enabled = false;
            }

            other.transform.position = transform.position;
            other.AddComponent<Spinner>();
            blackH = true;
            StartCoroutine(BlackHole(other));
        }

        if (gameObject.name == "Sun" && other.name != "Line Renderer(Clone)")
        {
            Destroy(other);
            AudioManager.instance.Play("Sizzle");
        }

        else if (gameObject.CompareTag("Zapper") && (other.CompareTag("Rocket") || other.CompareTag("Alien")))
        {
            AudioManager.instance.Play("Zap");
            Destroy(other);
        }
    }

    private IEnumerator BlackHole(GameObject other)
    {
        if ((other.CompareTag("Rocket") || other.CompareTag("Alien")))
            other.GetComponent<GravityBody>().enabled = false;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;

        

        while (true)
        {
            if(!other.activeInHierarchy)
                StopAllCoroutines();

            other.transform.localScale = other.transform.localScale * 0.98f;

            if (other.transform.localScale.x < (ogScale/4))
            {
                blackH = false;
                Destroy(other);
                blackH = false;
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(0.001f); // How long to wait before showing next number (secs)
        }
    }
}
