using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{


	public GameObject[] lightRenderer;
	public Renderer[] exitRenderer;

	public bool activated = false;

	void Start ()
	{
		lightRenderer = GameObject.FindGameObjectsWithTag ("EmissionLightCorridor");

	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter (Collider collider)
	{
		if (!activated) {
			activated = true;
			foreach (GameObject obj in lightRenderer) {
				StartCoroutine (EmissionLower (obj.GetComponent <Renderer> ()));
			}
			foreach (Renderer ren in exitRenderer) {
				StartCoroutine (ExitRaiser (ren));
			}
			StartCoroutine (AmbientLower ());
		}
	}

	IEnumerator EmissionLower (Renderer ren)
	{
		Color baseColor = ren.material.GetColor ("_EmissionColor");
		float t = 0f;
		float color = 0.08f + Random.Range (0.014f, 0.110f);
		float randomTime = Random.Range (0.7f, 2.5f);
		while (t <= randomTime) {
			Color lastColor = Color.Lerp (baseColor, new Color (color, color, color), t);
			ren.material.SetColor ("_EmissionColor", lastColor);
			DynamicGI.SetEmissive (ren, lastColor);
			t += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}

	IEnumerator ExitRaiser (Renderer ren)
	{
		float t = 0f;
		while (t <= 2f) {
			t += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		Color color = new Color (0f, 0.5f, 0f);
		ren.material.SetColor ("_EmissionColor", color);
		DynamicGI.SetEmissive (ren, color);
	}

	IEnumerator AmbientLower ()
	{
		yield return new WaitForSeconds (2f);
		RenderSettings.ambientIntensity = 0.05f;
	}
}