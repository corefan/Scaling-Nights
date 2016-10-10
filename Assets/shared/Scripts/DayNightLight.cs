using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Remoting.Lifetime;
using UnityStandardAssets.Water;


[RequireComponent (typeof(Light))]
public class DayNightLight : MonoBehaviour
{
	public Material skybox;
	public float lightStepDuringDay;
	public float lightStepDuringNight;
	public float maxLightIntensity;
	public float minLightIntensity;
	public Material waterMaterial;
	public Color waterReflectionDay;
	public Color waterReflectionNight;
	public Vector3 dusk = new Vector3 (180, 0, 0);
	public Vector3 dawn = new Vector3 (0, 0, 0);
	private float skyBoxBlendFactor = 0.0f;
	private bool dayPhase;
	private Light lightSource;
	// Use this for initialization
	void Start ()
	{
		lightSource = GetComponent <Light> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePhaseDay ();
		UpdateLightAngle ();
		UpdateLightIntensity ();
		UpdateSkyboxBlendFactor ();
		UpdateWater ();
	}

	void UpdatePhaseDay ()
	{
		if (transform.eulerAngles.x >= dawn.x && transform.eulerAngles.x <= dusk.x) {
			dayPhase = true;
		} else {
			dayPhase = false;
		}
	}

	void UpdateSkyboxBlendFactor ()
	{
		if (!dayPhase) {
			skyBoxBlendFactor = 1.0f;
		} else {
			skyBoxBlendFactor = Mathf.Cos (transform.eulerAngles.x * Mathf.PI / 180);
		}
		skybox.SetFloat ("_Blend", skyBoxBlendFactor);
	}

	void UpdateLightAngle ()
	{
		float currentlightStep = (dayPhase) ? lightStepDuringDay : lightStepDuringNight;
		gameObject.transform.Rotate (Vector3.right * currentlightStep * Time.deltaTime);
	}

	void UpdateLightIntensity ()
	{
		float lightSinAngle;
		if (!dayPhase) {
			lightSource.intensity = minLightIntensity;
		} else {
			lightSinAngle = Mathf.Sin (transform.eulerAngles.x * Mathf.PI / 180);
			lightSource.intensity = lightSinAngle;
		}

	}

	void UpdateWater ()
	{	
		float lightCosAngle;
		if (!dayPhase) {
			waterMaterial.SetColor ("_ReflectionColor", waterReflectionNight);
		} else {
			lightCosAngle = Mathf.Cos (transform.eulerAngles.x * Mathf.PI / 180);
			waterMaterial.SetColor ("_ReflectionColor", Color.Lerp (waterReflectionDay, waterReflectionNight, lightCosAngle));
		}

	}

	public bool GetDayPhase ()
	{
		return dayPhase;
	}
}
