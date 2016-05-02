using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContainerUIController : MonoBehaviour
{
	public GridLayoutGroup gridlayout;
	public GameObject item_prefab;
	// Use this for initialization
	void Start ()
	{
		AddItem ();
		AddItem ();
		AddItem ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void AddItem ()
	{
		GameObject item = Instantiate (item_prefab);
		item.transform.SetParent (gridlayout.transform);
	}
}
