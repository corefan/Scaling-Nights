﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class StroboLight : MonoBehaviour
{
	private Light strobo;
	public bool isOn = false;
	public bool activate = false;
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
		if (activate) {
			float offset = Random.value;
			if (strobo != null && Time.time > nextStrobo) {
				nextStrobo = Time.time + stroboRate * offset;
				strobo.enabled = !isOn;
			} else {
				strobo.enabled = isOn;
			}
		}
	}

	public void Activate (float rate)
	{
		stroboRate = rate;
		activate = true;
	}

	public void Deactivate ()
	{
		strobo.enabled = false;
		activate = false;
	}
}

