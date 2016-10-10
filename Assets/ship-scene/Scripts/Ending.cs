using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
	public GameObject player;
	public FadingScene fading;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		fading = FindObjectOfType <FadingScene> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.gameObject.tag.Equals ("Player")) {
			StartCoroutine ("EndingRoutine");

		}

	}

	IEnumerator EndingRoutine ()
	{
		player.GetComponent <FPSInput> ().enabled = false;
		MouseLook[] mouselooks = player.GetComponentsInChildren <MouseLook> ();
		foreach (MouseLook mouselook in mouselooks) {
			mouselook.enabled = false;
		}
		float fadeTime = fading.StartFading (1, 1);
		yield return new WaitForSeconds (fadeTime + 0.4f);
		AsyncOperation async;
		fadeTime = fading.StartFading (1, 0);
		async = SceneManager.LoadSceneAsync (0);
		while (!async.isDone) {
			yield return null;
		}
		yield return null;
	}
}
