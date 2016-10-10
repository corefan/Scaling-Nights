using UnityEngine;
using System.Collections;

public class AlarmingPlayerDecal : MonoBehaviour
{
	public Texture2D[] decals;
	private int currentIndex = -1;
	private float alpha = 0.0f;
	private int drawDepth = 1;

	public void ActivateDecalsByStatus (string status, float value)
	{
		alpha = value / 100;
		if ("dying".Equals (status)) {
			currentIndex = 0;
		} else if ("freezing".Equals (status)) {
			currentIndex = 1;
		} else if ("gettinghurt".Equals (status)) {
			currentIndex = 2;
		} else {
			print ("Incorrect status");
		}
			
		alpha = Mathf.Lerp (1.0f, 0f, alpha);
	}

	void OnGUI ()
	{
		if (currentIndex >= 0) {
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), decals [currentIndex]);
		}
	}

	public void FadeCurrentDecal ()
	{
		StartCoroutine ("CurrentDecalFadingRoutine");
	}

	IEnumerator CurrentDecalFadingRoutine ()
	{
		for (float t = alpha; t > 0; t--) {
			alpha = Mathf.Lerp (0f, 1.0f, t * Time.deltaTime);
			yield return null;
		}
	}
}
