using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour {
	
	private Text textComponent;
	private int frameCount = 0;
	private float fps = 0;
	private float timeLeft;
	private float accum = 0f;
	private float updateInterval = 0.5f;

	void Start () {
		textComponent = GetComponent <Text> ();
		timeLeft = updateInterval;
	}
	
	// Update is called once per frame

	void Update () {
		frameCount += 1;
		timeLeft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		if (timeLeft <= 0f) {
			fps = accum / frameCount;
			timeLeft = updateInterval;
			accum = 0f;
			frameCount = 0;
		}
		if(fps < 30) {textComponent.color = Color.red;}
		else if (fps < 60) { textComponent.color = Color.yellow;}
		else { textComponent.color = Color.green;}
		textComponent.text = fps.ToString ("F");
	}
}
