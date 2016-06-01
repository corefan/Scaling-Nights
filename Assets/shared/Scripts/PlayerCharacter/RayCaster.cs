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

	private CharacterController _character;
	private GameObject hitObject;

	// Use this for initialization
	void Start ()
	{
		_character = GetComponent <CharacterController> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!(GameEvent.isPause || GameEvent.isUiEnabled)) {
			Vector3 point = transform.position + _character.center;
			if (Physics.SphereCast (point, _character.height / 2, transform.forward, out hit, activate_distance)) {
				hitObject = hit.transform.gameObject;
				if (hitObject.tag == "Lootable")
					Messenger <int>.Broadcast (GameEvent.SHOW_DIALOG, 0);
				if (Input.GetButtonDown ("Action")) {
					Container container = hitObject.GetComponent<Container> ();
					if (container != null) {
						container.Open ();
						return;
					}
				}
			} else {
				Messenger.Broadcast (GameEvent.HIDE_DIALOG);
			}
		}


	}
}

