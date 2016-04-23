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
	
	}

	void Awake ()
	{
		Messenger<int>.AddListener (GameEvent.SHOW_HAND_CURSOR, ChangeCursor);
		Messenger.AddListener (GameEvent.HIDE_CURSOR, HideCursor);
	}

	void OnDestroy ()
	{
		Messenger<int>.RemoveListener (GameEvent.SHOW_HAND_CURSOR, ChangeCursor);
		Messenger.RemoveListener (GameEvent.HIDE_CURSOR, HideCursor);
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
