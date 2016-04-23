using UnityEngine;
using System.Collections;
using System.Configuration;

public class RayCaster : MonoBehaviour
{
	private Camera _camera;
	private bool raycasting;

	void Awake ()
	{
		Messenger.AddListener (GameEvent.NEAR_INTERACTIVE, ActivateRay);
		Messenger.AddListener (GameEvent.FAR_INTERACTIVE, ActivateRay);
	}


	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEvent.NEAR_INTERACTIVE, ActivateRay);
		Messenger.RemoveListener (GameEvent.FAR_INTERACTIVE, ActivateRay);
	}

	void ActivateRay ()
	{
		if (raycasting == false) {
			raycasting = true;
		} else {
			raycasting = false;
			Messenger.Broadcast (GameEvent.HIDE_CURSOR);
		}
	}

	// Use this for initialization
	void Start ()
	{
		_camera = GetComponentInChildren<Camera> ();
		raycasting = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (raycasting) {
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
			Ray ray = _camera.ScreenPointToRay (point);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				ContainerItem target = hitObject.GetComponent<ContainerItem> ();
				if (target != null) {
					Messenger <int>.Broadcast (GameEvent.SHOW_HAND_CURSOR, 0);
				} else {
					Messenger.Broadcast (GameEvent.HIDE_CURSOR);
				}
			}
		}

	}
}

