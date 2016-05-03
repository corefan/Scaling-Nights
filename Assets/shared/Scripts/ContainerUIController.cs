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

	public void AddItem (Sprite img)
	{
		GameObject item = Instantiate (item_prefab);
		item.transform.SetParent (gridlayout.transform);
		Image sprite = item.transform.GetChild (0).GetComponent<Image> ();
		sprite.sprite = img;
	}
}
