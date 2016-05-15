using UnityEngine;
using System.Collections;

public class StroboLight : MonoBehaviour
{
	[SerializeField] private Light strobo;
	public bool isOn = false;
	public float stroboRate = 0.7f;
	private float nextStrobo = 0.0f;
	// Use this for initialization
	void Start ()
	{
		strobo = GetComponent<Light> ();
		strobo.enabled = isOn;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float offset = Random.value;
		if (strobo != null && Time.time > nextStrobo) {
			nextStrobo = Time.time + stroboRate * offset;
			strobo.enabled = !isOn;
		} else {
			strobo.enabled = isOn;
		}
	}
}

