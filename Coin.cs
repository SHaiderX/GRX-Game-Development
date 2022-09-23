using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool collected = false;

    //float speed = 0.1f;
    //float delta = 0.3f;  //delta is the difference between min y to max y.

    //void Update()
    //{
    //    float y = Mathf.PingPong(speed * Time.time, delta);
    //    Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);
    //    transform.position = pos;
    //}

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        GameObject other = collision2D.gameObject;

        if (other.CompareTag("Rocket") == true)
        {
            collected = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            AudioManager.instance.Play("Coin");
            UnityEngine.Debug.Log("Coin Collected");
        }
    }

    void PlayNoise() {
        AudioManager.instance.Play("Coin");
    }
}
