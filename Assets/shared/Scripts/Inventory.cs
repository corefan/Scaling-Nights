using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Player))]
public class Inventory : MonoBehaviour
{
	public GameObject[] equip;
	public GameObject[] pack;
	public int pack_size = 3;
	// Use this for initialization
	void Start ()
	{
		pack = new GameObject[pack_size];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool InsertItem (GameObject item)
	{
		for (int i = 0; i < pack_size; i++) {
			if (pack [i] == null) {
				pack [i] = item;
				return true;
			}
		}
		return false;
	}

	public GameObject[] GetItems ()
	{
		return pack;
	}

	public bool RemoveItem (GameObject item)
	{
		for (int i = 0; i < pack_size; i++) {
			if (item.Equals (pack [i])) {
				pack [i] = null;
				return true;
			}
		}
		return false;
	}

	public void IncrementPackSize (int size)
	{
		pack_size = size;
	}
}
