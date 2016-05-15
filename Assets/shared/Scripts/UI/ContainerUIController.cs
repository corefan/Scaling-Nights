using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ContainerUIController : MonoBehaviour
{
	private Container _container;
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

	void Awake ()
	{
		Messenger.AddListener (GameEvent.HIDE_DIALOG, Hide);
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEvent.HIDE_DIALOG, Hide);
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

	public void Show ()
	{
		transform.GetChild (0).gameObject.SetActive (true);
	}

	public void Hide ()
	{
		transform.GetChild (0).gameObject.SetActive (false);
	}
}
