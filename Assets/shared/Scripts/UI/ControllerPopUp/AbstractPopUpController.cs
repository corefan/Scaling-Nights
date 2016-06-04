using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using UnityEditor;

public class AbstractPopUpController : MonoBehaviour
{
	[HideInInspector]
	public AbstractContainer _container;
	public GridLayoutGroup gridlayout;
	public GameObject item_prefab;

	// Use this for initialization
	void Start ()
	{
	}


	// Update is called once per frame
	void Update ()
	{
	}

	public void SetActive (Boolean value)
	{	
		transform.GetChild (0).gameObject.SetActive (value);
	}

	public void RemoveAll ()
	{
		for (int i = 0; i < gridlayout.transform.childCount; i++) {
			Destroy (gridlayout.transform.GetChild (i).gameObject);
		}
	}

	public void Show ()
	{
		if (transform.GetChild (0).gameObject.activeSelf) {
			Hide ();
		} else {
			UpdateItems ();
			Messenger<int>.Broadcast (GameEvent.SHOW_CURSOR, 0);
			transform.GetChild (0).gameObject.SetActive (true);
		}
	}

	public void UpdateItems ()
	{
		foreach (GameObject item in _container.items) {
			if (item != null) {
				ItemLabel label = Instantiate (item_prefab).GetComponent <ItemLabel> ();
				Image image = label.transform.GetChild (0).gameObject.GetComponent <Image> ();
				label.item = item;
				image.sprite = item.GetComponent <Item> ().sprite;
				label.transform.SetParent (gridlayout.transform);
			}
		}
	}

	public void RemoveItem (GameObject item)
	{
		
		_container.RemoveItem (item);
	}

	public void Hide ()
	{
		RemoveAll ();
		Messenger.Broadcast (GameEvent.HIDE_CURSOR);
		transform.GetChild (0).gameObject.SetActive (false);
	}
}

