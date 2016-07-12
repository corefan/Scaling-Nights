using UnityEngine;
using System.Collections;

public class AbstractContainer : MonoBehaviour
{
	public GameObject[] items;
	[HideInInspector]
	public AbstractPopUpController _uiController;

	public void Open ()
	{
		_uiController.Show ();
	}

	public void Close ()
	{
		_uiController.Hide ();
	}

	public bool InsertItem (GameObject item)
	{
		for (int i = 0; i < items.Length; i++) {
			if (items [i] == null) {
				items [i] = item;
				return true;
			}
		}
		return false;
	}

	public GameObject GetItem (int index)
	{
		return items [index];
	}

	public GameObject[] GetItems ()
	{
		return items;
	}

	public bool RemoveItem (GameObject item)
	{
		for (int i = 0; i < items.Length; i++) {
			if (item.Equals (items [i])) {
				items [i] = null;
				return true;
			}
		}
		return false;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

