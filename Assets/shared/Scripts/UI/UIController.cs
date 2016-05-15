using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class UIController : MonoBehaviour
{
	private bool _inventory;
	// Use this for initialization
	void Start ()
	{
		_inventory = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameEvent.gameOver) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				GameEvent.Pause ();
			} else if (Input.GetKeyDown (KeyCode.I)) {
				if (!_inventory) {
					_inventory = true;
					GameEvent.Pause ();
					Messenger.Broadcast (GameEvent.SHOW_INVENTORY);
				} else {
					_inventory = false;
					GameEvent.Pause ();
					Messenger.Broadcast (GameEvent.HIDE_INVENTORY);
				}
			}
		}
	}

}
