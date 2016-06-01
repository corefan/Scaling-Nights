using UnityEngine;
using System;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class StroboLight : MonoBehaviour
{
	private Renderer emitter;
	private Color emissiveOff;
	private Color emissiveOn;
	public bool activate = false;
	public float stroboRate = 0.7f;
	private float nextStrobo = 0.0f;
	// Use this for initialization
	void Start ()
	{
		emitter = GetComponent<Renderer> ();
		emissiveOn = Color.white * Mathf.LinearToGammaSpace (9f);
		emissiveOff = emitter.sharedMaterial.GetColor ("_EmissionColor");
		emitter.sharedMaterial.SetColor ("_EmissionColor", emissiveOff);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (activate) {
			//float offset = Random.value;
			if (emitter != null && Time.time > nextStrobo) {
				nextStrobo = Time.time + stroboRate;
				emitter.sharedMaterial.SetColor ("_EmissionColor", emissiveOn);
				DynamicGI.SetEmissive (emitter, emissiveOn);
			} else {
				emitter.sharedMaterial.SetColor ("_EmissionColor", emissiveOff);
				DynamicGI.SetEmissive (emitter, emissiveOff);
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
		emitter.sharedMaterial.SetColor ("_EmissionColor", emissiveOff);
		DynamicGI.SetEmissive (emitter, emissiveOff);
		activate = false;
	}
}

