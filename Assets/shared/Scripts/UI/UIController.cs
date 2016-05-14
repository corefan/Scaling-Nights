using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class UIController : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && !GameEvent.isPause) {
			GameEvent.Pause ();
			Messenger.Broadcast (GameEvent.SHOW_UI);
		} else if (Input.GetKeyDown (KeyCode.Escape) && GameEvent.isPause) {
			GameEvent.UnPause ();
			Messenger.Broadcast (GameEvent.HIDE_UI);
		}
	}

	void Awake ()
	{
		Messenger<int>.AddListener (GameEvent.NEAR_INTERACTIVE, ChangeCursor);
		Messenger.AddListener (GameEvent.FAR_INTERACTIVE, HideCursor);
	}

	void OnDestroy ()
	{
		Messenger<int>.RemoveListener (GameEvent.NEAR_INTERACTIVE, ChangeCursor);
		Messenger.RemoveListener (GameEvent.FAR_INTERACTIVE, HideCursor);
	}

	void ChangeCursor (int index)
	{
	}

	void HideCursor ()
	{
	}

}
