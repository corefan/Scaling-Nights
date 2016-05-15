﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class DialogController : MonoBehaviour
{
	[SerializeField]
	private GameObject[] dialogs;
	private int current_dialog;

	void Awake ()
	{
		Messenger<int>.AddListener (GameEvent.ADD_ITEM, Show);
		Messenger.AddListener (GameEvent.REMOVE_ITEM, Hide);
	}


	void OnDestroy ()
	{
		Messenger<int>.RemoveListener (GameEvent.ADD_ITEM, Show);
		Messenger.RemoveListener (GameEvent.REMOVE_ITEM, Hide);
	}

	// Use this for initialization
	void Start ()
	{
		current_dialog = -1;
		for (int i = 0; i < dialogs.Length; i++) {
			dialogs [i].SetActive (false);
		}

	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Show (int index)
	{
		gameObject.SetActive (true);
		dialogs [index].SetActive (true);

	}

	public void Hide ()
	{
		gameObject.SetActive (false);
		if (current_dialog >= 0)
			dialogs [current_dialog].SetActive (false);
	}
}