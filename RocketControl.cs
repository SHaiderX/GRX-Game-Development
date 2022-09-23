using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine.Events;
using UnityEngine;

public class RocketControl : MonoBehaviour
{

    Rigidbody2D rb;
    public static int Damage = 1;

    public int radius;
    private Vector3 curPos = new Vector3(0, 0, 0);
    public LayerMask alienLayer;
    private float minDepth = -Mathf.Infinity;
    private float maxDepth = Mathf.Infinity;
    //Alien Destroy Effect
    public GameObject destroyEffect;
    public GameObject Explosion;
    public int lifeTime;

    Collider2D[] AliensInRange;


    public static int apm;
    public static int vibr;

    private void Start() {
        
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyTimer());

    }

    void Update() {
        trajectoryFacing();
        curPos = transform.position;
        AliensInRange = Physics2D.OverlapCircleAll(curPos, radius, alienLayer, minDepth, maxDepth);
    }

    private IEnumerator DestroyTimer()
    {
        int i = 0;
        while (true)
        {
            i++;

            if (i == lifeTime)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                AudioManager.instance.Play("LaunchSound");
                Destroy(gameObject);
                Haptics();
                RadiusExplosion();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void trajectoryFacing() {
        
        //FindObjectOfType<AudioManager>().Play("RocketTravel");
        Vector2 dirr = rb.velocity;
        float angle = Mathf.Atan2(dirr.y, dirr.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void RadiusExplosion() {
        if (AliensInRange != null)
        {
            foreach (Collider2D entity in AliensInRange)
            {
                GameObject alien = entity.gameObject;
                Destroy(alien);
                AudioManager.instance.Play("Splat");
                Instantiate(destroyEffect, alien.transform.position, alien.transform.rotation);
            }

        }
    }

    public static void VibUp(int v, int a) {
        vibr = v;
        apm = a;
    }

    void Haptics() {
        if (gameObject.name == "Rocket(Clone)")
            Vibration.CreateOneShot(55, 60);
        if (gameObject.name == "Plasma(Clone)")
            Vibration.CreateOneShot(400, 150);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Haptics();
        RadiusExplosion();
    }
}