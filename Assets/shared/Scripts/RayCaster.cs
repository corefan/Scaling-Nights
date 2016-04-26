using UnityEngine;
using System.Collections;
using System.Configuration;

public class RayCaster : MonoBehaviour
{
	private Camera _camera;
	[SerializeField]
	private float activate_distance = 3.0f;
	private bool raycasting;

	void Awake ()
	{

	}


	void OnDestroy ()
	{
	}

	void ActivateRay ()
	{
		if (raycasting == false) {
			raycasting = true;
		} else {
			raycasting = false;
			Messenger.Broadcast (GameEvent.FAR_INTERACTIVE);
		}
	}

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
		if (raycasting) {
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
			Ray ray = _camera.ScreenPointToRay (point);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				ContainerItem target = hitObject.GetComponent<ContainerItem> ();
				if (target != null && Vector3.Distance (gameObject.transform.position, target.transform.position) <= activate_distance) {
					Messenger <int>.Broadcast (GameEvent.NEAR_INTERACTIVE, 0);
				} else {
					Messenger.Broadcast (GameEvent.FAR_INTERACTIVE);
				}
			}
		}

	}
}

