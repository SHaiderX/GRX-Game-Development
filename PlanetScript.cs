using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public float gravity = -9.8f;
    public float range = 10;
    public bool reversed;

    public void Attract(Transform target, Rigidbody2D rb, float rotationSpeed, float mass)
    {
        float multiplier = (reversed) ? -1 : 1;
        Vector3 gravityUp = (target.position - transform.position).normalized * multiplier;
        Vector3 localUp = target.up;

        rb.AddForce(gravityUp * gravity * 100 * mass);
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * target.rotation;
        target.rotation = Quaternion.Slerp(target.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        
        //Prevent from rotating on x and y axis
        Quaternion q = target.rotation;
        q.eulerAngles = new Vector3(0, 0, q.eulerAngles.z);
        target.rotation = q;

    }
}