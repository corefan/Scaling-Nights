using UnityEngine;
using System.Collections;

public class EnemiesSight : MonoBehaviour
{
	public bool playerInSight = false;
	public bool playerInArea = false;
	public float rotSpeed = 100f;


	private NavMeshAgent nav;
	private SphereCollider col;
	private Transform playerPosition;


	void Awake ()
	{
	
		// Setting up the references.;
		nav = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
	}


	void Update ()
	{
		if (playerInArea) {
			RaycastHit hit;
			Ray ray = new Ray (transform.position, transform.forward);

			if (Physics.SphereCast (ray, 0.75f, out hit)) {
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


	float CalculatePathLength (Vector3 targetPosition)
	{
		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath ();
		if (nav.enabled)
			nav.CalculatePath (targetPosition, path);

		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

		// The first point is the enemy's position.
		allWayPoints [0] = transform.position;

		// The last point is the target position.
		allWayPoints [allWayPoints.Length - 1] = targetPosition;

		// The points inbetween are the corners of the path.
		for (int i = 0; i < path.corners.Length; i++) {
			allWayPoints [i + 1] = path.corners [i];
		}

		// Create a float to store the path length that is by default 0.
		float pathLength = 0;

		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for (int i = 0; i < allWayPoints.Length - 1; i++) {
			pathLength += Vector3.Distance (allWayPoints [i], allWayPoints [i + 1]);
		}

		return pathLength;
	}
}