using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public GameObject destroyEffect;
    public Rigidbody2D rb;
    int i = 0;
    private Vector3 Pos;

    void Awake() {
        rb.gravityScale = 0.0f;
        rb.mass *= 10;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        StartCoroutine(DestroySatellite());
    }

    private IEnumerator DestroySatellite()
    {
        while (true)
        {

            Pos = transform.position;
            
            int x = UnityEngine.Random.Range(-200, 200);
            Pos.x += x;
            int y = UnityEngine.Random.Range(-200, 200);
            Pos.y += y;

            Instantiate(destroyEffect, Pos, Quaternion.identity);
            AudioManager.instance.Play("SatelliteExplosion");
            i++;

            if (i == 3)
            {
                AllMenus.currWeapon.GetComponent<Shooting>().ammo = 0;
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}
