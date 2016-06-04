using UnityEngine;
using System.Collections;
using UnityEditor;

public class ItemLabel : MonoBehaviour
{
	public GameObject item;
	[SerializeField]
	private AbstractPopUpController _source;
	[SerializeField]
	private AbstractPopUpController _destination;
	// Use this for initialization
	void Start ()
	{
		_destination = GameObject.Find ("InventoryHUD").GetComponent <AbstractPopUpController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void MoveTo ()
	{
		_source = transform.parent.parent.gameObject.GetComponent <AbstractPopUpController> ();
		if (_source.GetType () == typeof(ContainerUIController)) {
			if (Manager.inventory.InsertItem (item)) {
				Destroy (gameObject);
				_destination.RemoveAll ();
				_destination.UpdateItems ();
			} 
		} else if (_source.GetType () == typeof(InventoryController)) {
			Consumable consumable = item.GetComponent <Consumable> ();
			if (consumable != null) {
				consumable.Use ();
				Destroy (gameObject);
			}

		}
	}

}
