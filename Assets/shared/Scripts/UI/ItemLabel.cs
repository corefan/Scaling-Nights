using UnityEngine;
using System.Collections;
using UnityEditor;

[RequireComponent (typeof(Collider2D))]
public class ItemLabel : MonoBehaviour
{
	public GameObject item;
	[SerializeField]
	private AbstractPopUpController _source;
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
				_source.RemoveItem (item);
				_destination.RemoveAll ();
				_destination.UpdateItems ();
			} 
		}
	}

}
