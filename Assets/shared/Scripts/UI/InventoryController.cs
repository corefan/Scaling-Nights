using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryController : MonoBehaviour
{
	[SerializeField]
	private Inventory _inventory;
	public GridLayoutGroup gridlayout;
	public GameObject item_prefab;
	// Use this for initialization
	void Start ()
	{
		transform.GetChild (0).gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void RemoveAll ()
	{
		for (int i = 0; i < gridlayout.transform.childCount; i++) {
			Destroy (gridlayout.transform.GetChild (i).gameObject);
		}
	}

	public void AddItem (GameObject item)
	{
		GameObject prefab = Instantiate (item_prefab);
		prefab.transform.SetParent (gridlayout.transform);
		Image prefabSprite = prefab.transform.GetChild (0).GetComponent<Image> ();
		ItemLabel itemlabel = prefab.GetComponent<ItemLabel> ();
		itemlabel.item = item;
		itemlabel.index = prefab.transform.parent.childCount - 1;
		prefabSprite.sprite = item.GetComponent<Image> ().sprite;
	}

	void Awake ()
	{
		Messenger.AddListener (GameEvent.SHOW_INVENTORY, Show);
		Messenger.AddListener (GameEvent.HIDE_INVENTORY, Hide);
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEvent.SHOW_INVENTORY, Show);
		Messenger.RemoveListener (GameEvent.HIDE_INVENTORY, Hide);
	}

	public void Show ()
	{
		foreach (GameObject item in _inventory.GetItems ()) {
			AddItem (item);
		}
		transform.GetChild (0).gameObject.SetActive (true);
	}

	public void Hide ()
	{
		RemoveAll ();
		transform.GetChild (0).gameObject.SetActive (false);
	}
}
