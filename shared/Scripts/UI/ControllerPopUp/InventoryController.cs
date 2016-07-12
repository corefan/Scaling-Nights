using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryController : AbstractPopUpController
{

	// Use this for initialization
	void Start ()
	{
		transform.GetChild (0).gameObject.SetActive (false);
		_container = Manager.inventory;
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

