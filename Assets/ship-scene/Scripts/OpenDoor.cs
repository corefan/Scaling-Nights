using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour 
{

	private float doorOpenAngle = -90.0f;
	public float doorCloseAngle = 0.0f;
	public float doorAnimSpeed = 2.0f;
	private Quaternion doorOpen = Quaternion.identity;
	private Quaternion doorClose = Quaternion.identity;
	private Transform playerTransform = null; // transform per salvarmi dove trovo il player
	public bool doorStatus = false; 
	private bool doorGo = false; //per la  Coroutine
	void Start() {
		doorStatus = false; 
		//Inizializzo i quaternions
		doorOpen = Quaternion.Euler (0, doorOpenAngle, 0);
		doorClose = Quaternion.Euler (0, doorCloseAngle, 0);
		//trovo la posizione del player
		playerTransform = GameObject.Find ("Player").transform;
	}
	void Update() {
		//If press F key on keyboard
		if (Input.GetKeyDown(KeyCode.F) && !doorGo) {
			//distanza ra il player e la porta
			if (Vector3.Distance(playerTransform.position, transform.position) < 2.5f) {
				if (doorStatus) { 
					StartCoroutine(this.moveDoor(doorClose));
				} else { 
					StartCoroutine(this.moveDoor(doorOpen));
				}
			}
		}
	}
	public IEnumerator moveDoor(Quaternion dest) {
		doorGo = true;
		//transform.rotation = Quaternion.Inverse (dest);
		//Quaternion.angle restituisce l angolo in gradi tra la porta e i -90.0f in questo caso cioe i gradi della porta quando sarà aperta quindi tra 2 punti
		while (Quaternion.Angle(transform.localRotation, dest) > 4.0f) {
			transform.localRotation = Quaternion.Slerp(transform.localRotation,dest,Time.deltaTime * 2f);

			yield return null;
		}
		//cambio lo stato della porta
		doorStatus = !doorStatus;
		doorGo = false;
		yield return null;
	}

}