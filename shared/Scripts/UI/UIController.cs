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
		if (!GameEvent.gameOver) {
			if (Input.GetButtonDown ("Cancel")) {
				GameEvent.Pause ();
			} else if (Input.GetButtonDown ("Inventory")) {
				Manager.inventory.Open ();
			}
		}
	}

}
