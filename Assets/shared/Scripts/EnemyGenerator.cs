using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{

	[SerializeField] private GameObject[] enemyPrefab;
	[SerializeField] private Transform[] startingPoints;

	private int[] enemyCount;
	private GameObject[][] enemies;

	public float waitGenerationTime = 5.0f;

	void Start ()
	{
//		if (enemyCount.Length < startingPoints.Length || enemyPrefab.Length <= startingPoints.Length)
//			return;
		
		enemies = new GameObject[startingPoints.Length][];

		enemyCount = new int[startingPoints.Length];
		enemyCount [0] = 4;
		enemyCount [1] = 5;

		for (int i = 0; i < enemies.Length; i++) {
			enemies[i] = new GameObject[enemyCount[i]];
		}
			

	}

	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < startingPoints.Length; i++) {

			for(int j = 0; j < enemyCount[i]; j++) {

				StartCoroutine(WaitAndInstantiate(waitGenerationTime));

				if (enemies [i][j] == null) {

					enemies [i][j] = Instantiate (enemyPrefab[i]) as GameObject;

					enemies [i][j].transform.position = startingPoints[i].transform.position;

					float angle = Random.Range (0, 360);
					enemies [i][j].transform.Rotate (0, angle, 0);
				}

			}
		}
	}

	IEnumerator WaitAndInstantiate(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}

}
