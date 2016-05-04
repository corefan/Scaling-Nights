using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
	public Image cursor;
	public Sprite[] cursor_sprites;
	// Use this for initialization
	void Start ()
	{
		cursor.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameEvent.isPause || GameEvent.isUiEnabled) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
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

	void ChangeCursor (int sprite_index)
	{
		cursor.enabled = true;
		cursor.sprite = cursor_sprites [sprite_index];
	}

	void HideCursor ()
	{
		cursor.enabled = false;
	}

}
