using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Player))]
public class Inventory : MonoBehaviour
{
	public GameObject[] items;
	public int pack_size = 3;
	// Use this for initialization
	void Start ()
	{
		items = new GameObject[pack_size];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool InsertItem (GameObject item)
	{
		for (int i = 0; i < pack_size; i++) {
			if (items [i] == null) {
				items [i] = item;
				return true;
			}
		}
		return false;
	}

	public bool RemoveItem (GameObject item)
	{
		for (int i = 0; i < pack_size; i++) {
			if (item.Equals (items [i])) {
				items [i] = null;
				return true;
			}
		}
		return false;
	}

	public void IncresePackSize (int size)
	{
		pack_size = size;
	}
}
