using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
	// The nav mesh agent's speed when patrolling.
	public float patrolSpeed = 3f;
	// The nav mesh agent's speed when chasing.
	public float chaseSpeed = 6f;
	// The amount of time to wait when the last sighting is reached.
	public float chaseWaitTime = 5f;
	// The amount of time to wait when the patrol way point is reached.
	public float patrolWaitTime = 1f;
	// An array of transforms for the patrol route.
	public Transform[] patrolWayPoints;
	// Debug Varialble.
	int controlVar = 1;

	private Animator anim;
	// Reference to the EnemySight script.
	private EnemySight enemySight;
	// Reference to the nav mesh agent.
	private NavMeshAgent nav;
	// Reference to the player's transform.
	private Transform playerPosition;
	// Reference to the PlayerHealth script.
	private Player player;
	// Reference to the last global sighting of the player.
	private LastPlayerSight lastPlayerSighting;
	// A timer for the chaseWaitTime.
	private float chaseTimer;
	// A timer for the patrolWaitTime.
	private float patrolTimer;
	// A counter for the way point array.
	private int wayPointIndex;

	public float timeBetweenAttacks = 0.5f;
	// The time in seconds between each attack.
	public int attackDamage = 10;
	// The amount of health taken away per attack.
	float timer;
	// Timer for counting up to the next attack.



	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator>();
		enemySight = GetComponent<EnemySight> ();
		nav = GetComponent<NavMeshAgent> ();
		playerPosition = GameObject.FindGameObjectWithTag (Tags.player).transform;
		player = playerPosition.GetComponent<Player> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSight> ();
	}


	void Update ()
	{
		// If the player is in sight and is alive...

	
		Debug.Log (enemySight.playerInSight);

		if (enemySight.playerInSight && player.health > 0f) {
			// ... shoot.
			Shooting ();
//			Debug.Log ("SHOOTING " + controlVar);
//			controlVar++;
		}

			// If the player has been sighted and isn't dead...
//		else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && player.getHealth () > 0f) {
			// ... chase.
		else if (enemySight.playerInArea && player.health > 0f) {
			Chasing ();
//			Debug.Log ("CHASING " + controlVar);
//			controlVar++;
		}
			// Otherwise...
		else {
			// ... patrol.
			Patrolling ();
//			Debug.Log ("PATROLLING " + controlVar);
//			controlVar++;
		}
	}


	void Shooting ()
	{
		// Stop the enemy where it is.
		nav.Stop ();
		anim.SetBool ("InSight", true);
		Shoot ();
	}


	void Chasing ()
	{

		anim.SetBool ("InSight", false);
		anim.SetBool ("Patrolling", false);
		anim.SetBool ("Chasing", true);

//		Debug.Log ("Step 1");

		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;

//		Debug.Log ("Step 2");
		// If the the last personal sighting of the player is not close...
		if (sightingDeltaPos.sqrMagnitude > 4f)
				// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
				nav.destination = enemySight.personalLastSighting;

//		Debug.Log ("Step 3");
		// Set the appropriate speed for the NavMeshAgent.
		nav.speed = chaseSpeed;

//		Debug.Log ("Step 4");
		// If near the last personal sighting...
		if (nav.remainingDistance < nav.stoppingDistance) {
			// ... increment the timer.
			chaseTimer += Time.deltaTime;

			Debug.Log ("Time");

			// If the timer exceeds the wait time...
			if (chaseTimer >= chaseWaitTime) {
				// ... reset last global sighting, the last personal sighting and the timer.
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
//				Debug.Log ("Step 5 if");
			}
		} else
				// If not near the last sighting personal sighting of the player, reset the timer.
				chaseTimer = 0f;
//		Debug.Log ("Step 5 else");
	}


	void Patrolling ()
	{
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = patrolSpeed;

		anim.SetBool ("InSight", false);
		anim.SetBool ("Patrolling", true);
		anim.SetBool ("Chasing", false);

		// If near the next waypoint or there is no destination...
		if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance) {
			// ... increment the timer.
			patrolTimer += Time.deltaTime;

			// If the timer exceeds the wait time...
			if (patrolTimer >= patrolWaitTime) {
				// ... increment the wayPointIndex.
				if (wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;

				// Reset the timer.
				patrolTimer = 0;
			}
		} else
				// If not near a destination, reset the timer.
				patrolTimer = 0;

		// Set the destination to the patrolWayPoint.
//		nav.destination = patrolWayPoints [wayPointIndex].position;
		nav.SetDestination (patrolWayPoints[wayPointIndex].position);
	}

	void Shoot ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if (timer >= timeBetweenAttacks && enemySight.playerInSight) {
			// ... attack.
			Damage ();
		}
	}

	void Damage ()
	{
		// Reset the timer.
		timer = 0f;

		// If the player has health to lose...
		if (player.health> 0) {
			// ... damage the player.
			player.TakeDamage(attackDamage);
		}
	}

}
