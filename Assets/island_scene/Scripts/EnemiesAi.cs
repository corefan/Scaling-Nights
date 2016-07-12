using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemiesAi : MonoBehaviour
{
	public float patrolSpeed = 3.5f;
	public float chaseSpeed = 7f;
	public Transform[] patrolWayPoints;

	private Animator anim;
	private EnemiesSight enemySight;
	private NavMeshAgent nav;
	private Player player;
	private WayPointsStore wayPointStored;

	//	Boolean for stopping and resume the NavMeshAgent.
	private bool isStopped;
	// A timer for the chaseWaitTime.
	private float chaseTimer;
	// A timer for the patrolWaitTime.
	private float patrolTimer;
	// A counter for the way point array.
	private int wayPointIndex = 0;
	// The time in seconds between each attack.
	public float timeBetweenAttacks = 1f;
	// The amount of health taken away per attack.
	public int attackDamage = 10;
	// Timer for counting up to the next attack.
	float timer;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		enemySight = GetComponent<EnemiesSight> ();
		nav = GetComponent<NavMeshAgent> ();
		wayPointStored = GameObject.Find ("PatrolController").transform.GetComponent<WayPointsStore> ();
		player = enemySight.getPlayerPosition ().GetComponent<Player> ();

		wayPoints ();
	}

	void Start ()
	{
		nav.autoBraking = false;
		isStopped = false;
		nav.stoppingDistance = 2.0f;

		Patrolling ();
	}


	void FixedUpdate ()
	{
		if (enemySight.playerInSight && player.health > 0) {
			Shooting ();
		} else if (!enemySight.playerInSight && enemySight.playerInArea && player.health > 0) {
			Chasing ();
		} else {
			Patrolling ();
		}

//		Debug.Log (player.getHealth ());
	}


	void Shooting ()
	{
//		Debug.Log ("Shoot");
		if (!isStopped) {
			//Debug.Log ("Stopped = true");
			//nav.Stop ();
			isStopped = true;

		}
		anim.SetInteger ("State", 2);
		nav.destination = transform.position;		
		Shoot ();
	}

	void Shoot ()
	{	
//		anim.SetInteger ("State", 4);
		// Add the time since Update was last called to the timer.
		timer	+=	Time.deltaTime;

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if (timer >= timeBetweenAttacks && enemySight.playerInSight) {
			Damage ();
		}
	}

	void Damage ()
	{
		
		// Reset the timer.
		timer = 0f;

		// If the player has health to lose...
		if (player.health >	0) {
			player.TakeDamage (attackDamage);
		}
	}


	void Chasing ()
	{
//		Debug.Log ("Chasing");
		if (isStopped) {
//			nav.Resume ();
			isStopped = false;
		}

		anim.SetInteger ("State", 1);

		nav.destination = enemySight.getPlayerPosition ().position;

		nav.speed = chaseSpeed;

//		if (nav.remainingDistance < nav.stoppingDistance) {}
	}

	void Patrolling ()
	{
		if (patrolWayPoints.Length == 0)
			return;
		
		if (isStopped) {
//			nav.Resume ();
			isStopped = false;
		}

		nav.speed = patrolSpeed;

		anim.SetInteger ("State", 0);

		nav.SetDestination (patrolWayPoints [wayPointIndex].position);

		// If near the next waypoint or there is no destination...
//		if (nav.destination == enemySight.getPlayerPosition () || nav.remainingDistance < nav.stoppingDistance) {
		if (nav.remainingDistance < nav.stoppingDistance) {

			if (wayPointIndex == patrolWayPoints.Length - 1)
				wayPointIndex = 0;
			else
				wayPointIndex++;
			
		}

		nav.SetDestination (patrolWayPoints [wayPointIndex].position);
	}

	//	Function for assign the wayPoints.
	void wayPoints ()
	{
		//FIXME
		//	Number of wayPoints to assign and the starting wayPoint.
		int hashWayPoints = Random.Range (1, wayPointStored.getWayStored ().Length + 1);

		Debug.Log (hashWayPoints);

		//	Array to remember which wayPoints have I used for this enemy and succesive declaration.
		int[] memento = new int[hashWayPoints];

		for (int i = 0; i < memento.Length; i++) {
			memento [i] = -1;
		}

		//	First assignment of the memento array.
		memento [0] = hashWayPoints - 1;

		//	Declaration of patrolWayPoints array.
		patrolWayPoints = new Transform[hashWayPoints];

		//	First assignment of patrolwayPoints array.	
		patrolWayPoints [0] = wayPointStored.getOneWayStored (hashWayPoints - 1);

		//	Loop for assign all patrolWayPoints array spot, checking to not assign double wayPoints.
		for (int i = 1; i < patrolWayPoints.Length; i++) {

			hashWayPoints = Random.Range (0, wayPointStored.getWayStored ().Length);

			while (assigned (hashWayPoints, memento)) {
				hashWayPoints = Random.Range (0, wayPointStored.getWayStored ().Length);
//				Debug.Log ("WHILE");
			}

			patrolWayPoints [i] = wayPointStored.getOneWayStored (hashWayPoints);

			memento [i] = hashWayPoints;

		}
	}

	//	Function for checking is the wayPoint is already in use.
	bool assigned (int number, int[] array)
	{
		for (int i = 0; i < array.Length; i++) {
			if (array [i] == number)
				return true;
		}
		return false;
	}
}
	
