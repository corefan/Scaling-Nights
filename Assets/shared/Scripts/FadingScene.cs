using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FadingScene : MonoBehaviour
{
	public Texture2D[] fadeOutTextures;
	public int index;
	public float fadeSpeed = 0.8f;
	private float alpha = 1.0f;
	private int fadeDir = -1;
	private int drawDepth = -1000;
	// Use this for initialization
	void Start ()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		
	}

	void OnGUI ()
	{

		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTextures [index]);
	}

	public float StartFading (int direction, int selected)
	{
		index = selected;
		fadeDir = direction;
		return fadeSpeed;
	}

	void  OnSceneLoaded (Scene scene, LoadSceneMode loadSceneMode)
	{
		alpha = 1.0f;
		StartFading (-1, 0);
	}
}