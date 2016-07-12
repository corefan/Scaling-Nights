using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;

public class RayShooter : MonoBehaviour
{

	private Camera _camera;
//	public Texture _aimTexture;
//	public 	int size = 20;

	// Use this for initialization
	void Start ()
	{
		_camera = GetComponent<Camera> ();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

//	void OnGUI ()
//	{
//		float posX = _camera.pixelWidth / 2 - size / 2;
//		float posY = _camera.pixelHeight / 2 - size / 2;
//		GUI.Label (new Rect (posX, posY, size, size), _aimTexture);
//	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()){
//			Vector3 Pointer = new Vector3( GetComponent<Camera>().pixelWidth/2, GetComponent<Camera>().pixelHeight/2,0);
//		}
//		else if (Input.GetMouseButtonDown (0)) {
//			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
//
//			Ray ray = _camera.ScreenPointToRay (point);
//			RaycastHit hit;
//			if (Physics.Raycast (ray, out hit)) {
////				Debug.Log ("Hit " + hit.point);
////					StartCoroutine(SphereIndicator(hit.point));
//				GameObject hitObject = hit.transform.gameObject;
//				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget> ();
//				MusicThrowObject music = hitObject.GetComponent<MusicThrowObject> ();
//				if (target != null) {
////					Debug.Log ("Target hit");
//					target.ReactToHit ();
//				} else if (music != null) {
//					music.ReactToHit ();
//				} else {
//					StartCoroutine (SphereIndicator (hit.point));
//				}
//			}
//		} 
//
	}

//	private IEnumerator SphereIndicator (Vector3 pos)
//	{
//		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//		sphere.GetComponent<SphereCollider> ().isTrigger = true;
//		sphere.transform.position = pos;
//		sphere.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
//
//		yield return new WaitForSeconds (1);
//
//		Destroy (sphere);
//	}
	
}

