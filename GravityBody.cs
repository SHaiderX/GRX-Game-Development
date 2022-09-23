using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : MonoBehaviour {


    public float rotationSpeed = 10.0f;

    [Range(0f, 5f)]
    public float mass = 1;

    private Rigidbody2D rb;
    private PlanetScript currentPlanet;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 0;
    }

    private void Update()
    {
        currentPlanet = FindClosestPlanet();
        if (mass == 0){
            mass = 1;
        }
    }

    private PlanetScript FindClosestPlanet()
    {
        PlanetScript closestPlanet = null;
        float closestDistance = Mathf.Infinity;

        PlanetScript[] allPlanets = FindObjectsOfType<PlanetScript>();
        foreach (PlanetScript planet in allPlanets)
        {
            float currentDistance = Vector2.Distance(transform.position, planet.transform.position);
            if (currentDistance < closestDistance && currentDistance <= (planet.range * 100))
            {
                closestDistance = currentDistance;
                closestPlanet = planet;
            }
        }

        return closestPlanet;
    }

    private void FixedUpdate()
    {
        if (currentPlanet != null)
            currentPlanet.Attract(transform, rb, rotationSpeed, mass);
    }
}
