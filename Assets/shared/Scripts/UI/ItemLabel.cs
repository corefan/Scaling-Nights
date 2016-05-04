using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
public class ItemLabel : MonoBehaviour
{
	public GameObject item;
	public int index;
	[SerializeField] private Inventory _inventory;
	// Use this for initialization
	void Start ()
	{
		_inventory = GameObject.Find ("Player").GetComponent <Inventory> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void MoveToInventory ()
	{
		
		if (_inventory.InsertItem (item)) {
			Destroy (gameObject.transform.parent.GetChild (index).gameObject);
		}
	}

}
