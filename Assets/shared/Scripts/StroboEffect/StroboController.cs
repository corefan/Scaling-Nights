using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class StroboController : MonoBehaviour
{

	public StroboLight[] lights;
	public float maxDuration;
	private float lastTime = 0.0f;
	private bool triggerable;

	// Use this for initialization
	void Start ()
	{
		triggerable = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (lastTime != 0.0f) {
			if (Time.time - lastTime > maxDuration) {
				StopStrobing ();
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && triggerable) {
			lastTime = Time.time;
			StartStrobing ();
		}

	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player" && triggerable) {
			StopStrobing ();
		}
	}

	void StartStrobing ()
	{
		foreach (StroboLight strobo in lights) {
			strobo.Activate (0.7f);
		}
	}

	void StopStrobing ()
	{
		triggerable = false;
		foreach (StroboLight strobo in lights) {
			strobo.Deactivate ();
		}
	}

}
