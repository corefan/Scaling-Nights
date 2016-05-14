using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
public class ItemLabel : MonoBehaviour
{
	public GameObject item;
	public int index;
	[SerializeField] private GameObject _source;
	[SerializeField] private GameObject _destination;
	// Use this for initialization
	void Start ()
	{
		_source = transform.parent.parent.parent.gameObject;
		if (_source.name.Equals ("InventoryPopUp")) {
			_destination = GameObject.Find ("ContainerPopUp");
		} else {
			_destination = GameObject.Find ("Player");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void MoveTo ()
	{
		
	}

}
