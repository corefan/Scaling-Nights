using UnityEngine;
using System.Collections;

public class Inventory : AbstractContainer
{
	public int capacity = 3;
	// Use this for initialization
	void Start ()
	{
		// find the ui controller
		_uiController = GameObject.Find ("InventoryHUD").GetComponent <InventoryController> ();
		items = new GameObject[capacity];

	}

}
