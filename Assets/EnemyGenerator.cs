using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{

	[SerializeField] private GameObject[] enemyPrefab;
	[SerializeField] private Transform[] startingPoints;

	private bool canGenerate;
	public int[] enemyCount;
	private GameObject[][] enemies;

	public float waitGenerationTime = 5.0f;

	void Start ()
	{
//		if (enemyCount.Length < startingPoints.Length || enemyPrefab.Length <= startingPoints.Length)
//			return;
		
		enemies = new GameObject[startingPoints.Length][];

//		enemyCount = new int[startingPoints.Length];

		for (int i = 0; i < enemies.Length; i++) {
			enemies [i] = new GameObject[enemyCount [i]];
		}

		canGenerate = true;
			

	}

	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < startingPoints.Length; i++) {

			for (int j = 0; j < enemyCount [i]; j++) {
				if (canGenerate && enemies [i] [j] == null) { 
					StartCoroutine (WaitAndInstantiate (waitGenerationTime, i,j));
				}
			}
		}
	}

	IEnumerator WaitAndInstantiate (float waitTime, int i,int j)
	{

		canGenerate = false;

		yield return new WaitForSeconds (waitTime);

		enemies[i][j] = Instantiate (enemyPrefab [i]) as GameObject;

		enemies[i][j].transform.position = startingPoints [i].transform.position;

		canGenerate = true;
	}
}
