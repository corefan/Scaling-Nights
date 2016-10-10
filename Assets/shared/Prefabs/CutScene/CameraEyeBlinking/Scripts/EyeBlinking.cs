using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EyeBlinking : MonoBehaviour
{

	public Animator BlinkController;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		

	}

	public void StartBlink ()
	{
		StartCoroutine ("BlinkRoutine");
	}

	IEnumerator BlinkRoutine ()
	{
		BlinkController.SetBool ("CutScene", true);
		yield return new WaitForSeconds (4);
		BlinkController.SetBool ("CutScene", false);

	}
}