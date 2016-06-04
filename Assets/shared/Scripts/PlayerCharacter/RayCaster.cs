using UnityEngine;
using System.Collections;
using System.Configuration;
using System.Xml;
using UnityEditor;

[RequireComponent (typeof(CharacterController))]
public class RayCaster : MonoBehaviour
{
	public RaycastHit hit;

	[SerializeField]
	private float activate_distance = 5.0f;

	private Camera _camera;
	private CharacterController _character;
	private GameObject hitObject;

	// Use this for initialization
	void Start ()
	{
		_character = GetComponent <CharacterController> ();
		_camera = transform.GetChild (0).GetComponent <Camera> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!(GameEvent.isPause || GameEvent.isUiEnabled)) {
			Ray point = _camera.ScreenPointToRay (new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0f));
			if (Physics.SphereCast (point, _character.height / 2, out hit, activate_distance)) {
				hitObject = hit.transform.gameObject;
				if (hitObject.tag == "Lootable" || hitObject.tag == "Consumable")
					Messenger <int>.Broadcast (GameEvent.SHOW_DIALOG, 0);
				if (Input.GetButtonDown ("Action")) {
					Container container = hitObject.GetComponent<Container> ();
					if (container != null) {
						container.Open ();
					}
					Item item = hitObject.GetComponent <Item> ();
					if (item != null) {
						Manager.inventory.InsertItem (item.gameObject);
						item.gameObject.SetActive (false);
					}
				}
			} else {
				Messenger.Broadcast (GameEvent.HIDE_DIALOG);
			}
		}


	}
}

