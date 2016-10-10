using UnityEngine;
using System.Collections;
using System.Security.AccessControl;
using UnityEngine.SceneManagement;

public class EndingPortal : MonoBehaviour
{

	public Player player;
	public FadingScene fading;
	public Animator animator;
	public Renderer render;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player").GetComponent <Player> ();
		fading = FindObjectOfType <FadingScene> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.gameObject.GetComponent<Player> () != null) {
			StartCoroutine ("EndingRoutine");

		}

	}

	IEnumerator EndingRoutine ()
	{
		player.gameObject.GetComponent <FPSInput> ().enabled = false;
		MouseLook[] mouselooks = player.gameObject.GetComponentsInChildren <MouseLook> ();
		foreach (MouseLook mouselook in mouselooks) {
			mouselook.enabled = false;
		}
		Color baseColor = render.material.GetColor ("_EmissionColor");
		float t = 0f;
		while (t <= 1.5f) {
			Color lastColor = Color.Lerp (baseColor, Color.white, t);
			render.material.SetColor ("_EmissionColor", lastColor);
			DynamicGI.SetEmissive (render, lastColor);
			t += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		float fadeTime = fading.StartFading (1, 1);
		yield return new WaitForSeconds (fadeTime + 2);
		GameEvent.GameOver ();
		ShipScene ();
	}

	public void ShipScene ()
	{
		StartCoroutine ("shipSceneRoutine");
	}

	IEnumerator shipSceneRoutine ()
	{
		AsyncOperation async;
		float fadeTime = fading.StartFading (1, 0);
		async = SceneManager.LoadSceneAsync (2);
		while (!async.isDone) {
			yield return null;
		}
		yield return null;
	}
}
