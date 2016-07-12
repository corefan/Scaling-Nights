using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SimpleCritter: MonoBehaviour
{
	public float speed = 6.0f;
	public float obstacle_range = 5.0f;
	public float stupid_direction_time;
	public float changedTime;
	public bool alive;
	// Use this for initialization
	void Start ()
	{
		stupid_direction_time = Random.Range (2.0f, 2.5f);
		changedTime = 0f;
		alive = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float currentTime = Time.time;
		if (alive) {
			transform.Translate (0, 0, speed * Time.deltaTime);
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast (ray, 0.75f, out hit)) {
				if (hit.distance < obstacle_range) {
					float angle = Random.Range (-110, 100);
					transform.Rotate (0, angle, 0);
				} else if (currentTime - changedTime >= stupid_direction_time) {
					changedTime = currentTime;	
				}
			}
		}

	}
}
