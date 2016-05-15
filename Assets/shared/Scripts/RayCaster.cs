using UnityEngine;
using System.Collections;
using System.Configuration;

public class RayCaster : MonoBehaviour
{
	private Camera _camera;
	private GameObject hitObject;
	[SerializeField]
	private float activate_distance = 3.0f;
	private bool raycasting;
		
	// Use this for initialization
	void Start ()
	{
		_camera = GetComponentInChildren<Camera> ();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		raycasting = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (raycasting && !(GameEvent.isPause || GameEvent.isUiEnabled)) {
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
			Ray ray = _camera.ScreenPointToRay (point);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				hitObject = hit.transform.gameObject;
				float distance = Vector3.Distance (gameObject.transform.position, hitObject.transform.position);
				if (distance <= activate_distance) {
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
}

