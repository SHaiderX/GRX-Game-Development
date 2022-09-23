using System.Collections;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private float timeToNewPoint = 1;

    [SerializeField] private GameObject pointPrefab = null;

    [SerializeField] private GameObject[] points;

	[SerializeField] private int maxPoints = 5;

	[SerializeField] private int pointNum;

	private void Start()
	{
		ClearPoints();
        StartCoroutine(SpawnPoint());	
	}

	void ClearPoints()
	{
		GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
		for (int i = 0; i < points.Length; i++)
		{
			Destroy(points[i]);
		}

		pointNum = 0;
	}

	IEnumerator SpawnPoint()
	{
        yield return new WaitForSeconds(timeToNewPoint);
        Instantiate(pointPrefab, transform.position, Quaternion.identity);
		pointNum++;

		if (pointNum < maxPoints)
		{
			StartCoroutine(SpawnPoint());
		}
    }
	private void OnTriggerEnter2D(Collider2D collider)
	{
		StopAllCoroutines();
		/*transform.GetComponent<SpawnPoints>().enabled = false;*/
		Destroy(gameObject);
	}
}
