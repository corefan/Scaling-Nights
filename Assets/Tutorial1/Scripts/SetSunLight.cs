using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour
{

	//public Renderer lightwall;

	Material sky;

	public Renderer water;

	public Transform worldProbe;

	// Use this for initialization
	void Start ()
	{

		sky = RenderSettings.skybox;

	}

	bool lighton = false;

	// Update is called once per frame
	void Update ()
	{


	
		Vector3 tvec = Camera.main.transform.position;
		worldProbe.transform.position = tvec;
		if (water != null) {
			water.material.mainTextureOffset = new Vector2 (Time.time / 100, 0);
			water.material.SetTextureOffset ("_DetailAlbedoMap", new Vector2 (0, Time.time / 80));
		}


	}
}
