using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
	public float fieldOfViewAngle = 110f;
	// Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight = false;

	public bool playerInArea = false;
	// Whether or not the player is currently sighted.
	public Vector3 personalLastSighting;
	// Last place this enemy spotted the player.
	int controlVar = 1;

	private NavMeshAgent nav;
	// Reference to the NavMeshAgent component.
	private SphereCollider col;
	// Reference to the sphere collider trigger component.

	public float deadZone = 5f;

	private LastPlayerSight lastPlayerSighting;
	// Reference to last global sighting of the player.
	private Animator anim;
	private Transform player;
	// Reference to the player.
	//	private Animator playerAnim;                    // Reference to the player's animator component.
	private Player playerHealth;
	// Reference to the player's health script.
	//	private HashIDs hash;                           // Reference to the HashIDs.
	private Vector3 previousSighting;
	// Where the player was sighted last frame.


	void Awake ()
	{
	
		// Setting up the references.
		anim = GetComponent <Animator>();
		nav = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSight> ();
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
//		playerAnim = player.GetComponent<Animator>();
		playerHealth = player.GetComponent<Player> ();
//		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

		// Set the personal sighting and the previous sighting to the reset position.
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}


	void Update ()
	{
//		Debug.Log ("Update");
		// If the last global sighting of the player has changed...
		if (lastPlayerSighting.position != previousSighting)
			// ... then update the personal sighting to be the same as the global sighting.
			personalLastSighting = lastPlayerSighting.position;


		// Set the previous sighting to the be the sighting from this frame.
		previousSighting = lastPlayerSighting.position;

		// If the player is alive...
//		if (playerHealth.getHealth () > 0) {
//			anim.SetBool ("Attacking", playerInSight);
//		} else {
//			anim.SetBool ("Attacking", false);
//		}
	}


	void OnTriggerStay (Collider other)
	{
		playerInArea = false;
		// If the player has entered the trigger sphere...
		if (other.GetComponent<CharacterController> ()) {
//			Debug.Log ("IN zone"); 
			// By default the player is not in sight.
			playerInSight = false;
			playerInArea = true;

			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

//			Debug.Log ("ONTRIGGERSTAY");
			// If the angle between forward and where the player is, is less than half the angle of view...
			if (angle < fieldOfViewAngle * 0.5f) { 
				RaycastHit hit;
//				Debug.Log ("RAYCASTHIT");
				// ... and if a raycast towards the player hits something...
//				if (Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, col.radius)) {
//					Debug.Log ("IFPHYSICS.RAYCAST");
					// ... and if the raycast hits the player...
//					if (hit.collider.gameObject == player) {
//					Debug.Log (hit.collider);

				Ray ray = new Ray (transform.position, transform.forward);

				if (Physics.SphereCast (ray, 0.75f, out hit)) {
					GameObject hitObject = hit.transform.gameObject;
					if(hitObject.GetComponent<CharacterController> ()) {
						Debug.Log ("hit.collider.gameObject == player");
						// ... the player is in sight.
						playerInSight = true;

						// Set the last global sighting is the players current position.
						lastPlayerSighting.position = player.transform.position;
					}

					NavSetup ();
				}
			}
				
			// ... and if the player is within hearing range...
			if (CalculatePathLength (player.transform.position) <= col.radius)
//					 ... set the last personal sighting of the player to the player's current position.
					personalLastSighting = player.transform.position;
		}

//		Debug.Log ("IN SIGHT?" + playerInSight + controlVar);
//						controlVar++;
	}


	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if (other.gameObject == player)
			playerInSight = false;
		playerInArea = false;
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


	void NavSetup ()
	{
		// Create the parameters to pass to the helper function.
		float speed;
		float angle;

//		// If the player is in sight...
//		if (playerInSight) {
//			// ... the enemy should stop...
//			speed = 0f;
//
//			// ... and the angle to turn through is towards the player.
//			angle = FindAngle (transform.forward, player.position - transform.position, transform.up);
//		} else {
//			// Otherwise the speed is a projection of desired velocity on to the forward vector...
//			speed = Vector3.Project (nav.desiredVelocity, transform.forward).magnitude;
//
//			// ... and the angle is the angle between forward and the desired velocity.
//			angle = FindAngle (transform.forward, nav.desiredVelocity, transform.up);
//		animSetup.Setup (speed, angle);
//
//
//		}

		if (playerInSight) {
			// ... the enemy should stop...
//			speed = 0f;

			// ... and the angle to turn through is towards the player.
			angle = FindAngle (transform.forward, player.position - transform.position, transform.up);
		} else {
			// Otherwise the speed is a projection of desired velocity on to the forward vector...
			speed = Vector3.Project (nav.desiredVelocity, transform.forward).magnitude;

			// ... and the angle is the angle between forward and the desired velocity.
			angle = FindAngle (transform.forward, nav.desiredVelocity, transform.up);

			// If the angle is within the deadZone...
			if (Mathf.Abs (angle) < deadZone) {
				// ... set the direction to be along the desired direction and set the angle to be zero.
				transform.LookAt (transform.position + nav.desiredVelocity);
				angle = 0f;

			}
		}


		// Call the Setup function of the helper class with the given parameters.
//				animSetup.Setup (speed, angle);
//		Debug.Log (speed);


		Vector3 eulerAngles = new Vector3 (0, angle, 0);
		transform.Rotate (eulerAngles);}


	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		// If the vector the angle is being calculated to is 0...
		if (toVector == Vector3.zero)
			// ... the angle between them is 0.
			return 0f;

		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle (fromVector, toVector);

		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross (fromVector, toVector);

		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign (Vector3.Dot (normal, upVector));

		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;

		return angle;
	}
}