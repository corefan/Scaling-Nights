using UnityEngine;
using System.Collections;

public class EnemiesSight : MonoBehaviour
{
	public bool playerInSight = false;
	public bool playerInArea = false;
	public float rotSpeed = 100f;

//	private NavMeshAgent nav;
//	private SphereCollider col;
	private Transform playerPosition;


	public Transform getPlayerPosition() { return playerPosition;}

	void Awake ()
	{
	
		// Setting up the references.;
//		nav = GetComponent<NavMeshAgent> ();
//		col = GetComponent<SphereCollider> ();
		playerPosition = FindObjectOfType<CharacterController>().transform;
	}


	void FixedUpdate ()
	{
		if (playerInArea) {
			RaycastHit hit;
			Ray ray = new Ray (transform.position, transform.forward);

			if (Physics.SphereCast (ray, 0.75f, out hit, 2.0f)) {
				GameObject hitObject = hit.transform.gameObject;

				if (hitObject.GetComponent<CharacterController> ()) {
					Debug.Log ("hit.collider.gameObject == player");
					// ... the player is in sight.
					playerInSight = true;
				}
			}
		}
	}


	void OnTriggerStay (Collider other)
	{
		// If the player has entered the trigger sphere...
		if (other.GetComponent<CharacterController> ()) {
			Debug.Log ("In AREA");
			// By default the player is not in sight.
			playerInSight = false;
			playerInArea = true;
			playerPosition.position = other.transform.position;

			Quaternion rotation = Quaternion.LookRotation (other.transform.position - transform.position);
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, rotSpeed * Time.deltaTime);

		}
	}


	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if (other.GetComponent<CharacterController> ()) {
			playerInSight = false;
			playerInArea = false;
		}
	}
}