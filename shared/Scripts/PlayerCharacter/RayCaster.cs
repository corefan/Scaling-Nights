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
			if (Physics.SphereCast (point, 1, out hit, activate_distance)) {
				hitObject = hit.transform.gameObject;
				if (hitObject.tag == "Lootable" || hitObject.tag == "Consumable" || hitObject.tag == "Weapon")
					Messenger <int>.Broadcast (GameEvent.SHOW_DIALOG, 0);
				if (Input.GetButtonDown ("Action")) {
					Action ();
				}
			} else {
				Messenger.Broadcast (GameEvent.HIDE_DIALOG);
			}
		}


	}

	private void Action ()
	{
		Container container = hitObject.GetComponent<Container> ();
		if (container != null) {
			container.Open ();
		}
		Consumable consumable = hitObject.GetComponent <Consumable> ();
		if (consumable != null) {
			if (Manager.inventory.InsertItem (consumable.gameObject)) {
				consumable.gameObject.SetActive (false);
				Manager.ShowDialogWithTimer (1, this);
			} else {
				Manager.ShowDialogWithTimer (2, this);
			}
		}
		MeleeWeapon melee = hitObject.GetComponent <MeleeWeapon> ();
		if (melee != null) {
			Transform hand = GameObject.Find ("RightHand").transform;
			if (hand.childCount == 0) {
				Manager.ShowDialogWithTimer (3, this);
				melee.transform.SetParent (hand);
				melee.GetComponent <Rigidbody> ().useGravity = false;
				melee.GetComponent <Rigidbody> ().isKinematic = true;
				melee.transform.localRotation = Quaternion.Euler (new Vector3 (52f, 260f, 113f));
				melee.transform.localPosition = new Vector3 (0.25f, -0.15f, 1.8f);
			} else {
				if (Manager.inventory.InsertItem (melee.gameObject)) {
					melee.gameObject.SetActive (false);
					Manager.ShowDialogWithTimer (1, this);
				} else {
					Manager.ShowDialogWithTimer (2, this);
				}
			}
		}
	}
}



