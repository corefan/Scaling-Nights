using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float health = 100;
	public float hunger = 100;
	public float thirst = 100;
	public float heat = 100;
	public float hunger_scale = 0.8f;
	public float thirst_scale = 1.2f;
	public float heat_scale = 1f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameEvent.GameOver) {
			hunger -= 1 * Time.deltaTime * hunger_scale;
			thirst -= 1 * Time.deltaTime * thirst_scale;
			heat -= 1 * Time.deltaTime * heat_scale;
			if (health <= 0) {
				GameEvent.GameOver = true;
			}
		}
	}
}
