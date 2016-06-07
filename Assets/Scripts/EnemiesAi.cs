using UnityEngine;
using System.Collections;
using System;

public class EnemiesAi : MonoBehaviour
{
	public float patrolSpeed = 3.5f;
	public float chaseSpeed = 7f;
	public Transform[] patrolWayPoints;

	private Animator anim;
	private EnemiesSight enemySight;
	private NavMeshAgent nav;
	private Player player;

	//	Boolean for stopping and resume the NavMeshAgent.
	private bool isStopped;
	// A timer for the chaseWaitTime.
	private float chaseTimer;
	// A timer for the patrolWaitTime.
	private float patrolTimer;
	// A counter for the way point array.
	private int wayPointIndex;
	// The time in seconds between each attack.
	public float timeBetweenAttacks = 1f;
	// The amount of health taken away per attack.
	public int attackDamage = 10;
	// Timer for counting up to the next attack.
	float timer;

	//DEBUG VARIABLE//
	private int destPoint = 0;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		enemySight = GetComponent<EnemiesSight> ();
		nav = GetComponent<NavMeshAgent> ();
		player = enemySight.getPlayerPosition ().GetComponent<Player> ();
	}

	void Start ()
	{

		nav.autoBraking = false;
		isStopped = false;
		nav.stoppingDistance = 0.5f;

		Patrolling ();
	}


	void FixedUpdate ()
	{
		if (enemySight.playerInSight && player.getHealth () > 0) {
			Shooting ();
		} else if (enemySight.playerInArea && player.getHealth () > 0) {
			Chasing ();
		} else {
			Patrolling ();
		}
	}


	void Shooting ()
	{
		Debug.Log ("Shoot");
		if (!isStopped) {
			nav.Stop ();
			isStopped = true;
		}

		anim.SetInteger ("State", 2);
		Shoot ();
		anim.SetInteger ("State", 4);
		anim.SetInteger ("State", 2);
	}

	void Shoot ()
	{
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
		if (player.getHealth ()	>	0) {
			player.TakeDamage (attackDamage);
		}
	}


	void Chasing ()
	{
		if (isStopped) {
			nav.Resume ();
			isStopped = false;
		}

		anim.SetInteger ("State", 1);

		nav.destination = enemySight.getPlayerPosition().position;

		nav.speed = chaseSpeed;

//		if (nav.remainingDistance < nav.stoppingDistance) {}
	}

	void Patrolling ()
	{
		if (patrolWayPoints.Length == 0)
			return;
		
		if (isStopped) {
			nav.Resume ();
			isStopped = false;
		}

		nav.speed = patrolSpeed;

		anim.SetInteger ("State", 0);

		// If near the next waypoint or there is no destination...
//		if (nav.destination == enemySight.getPlayerPosition () || nav.remainingDistance < nav.stoppingDistance) {
		if (nav.remainingDistance < nav.stoppingDistance) {
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
		
			if (wayPointIndex == patrolWayPoints.Length - 1)
				wayPointIndex = 0;
			else
				wayPointIndex++;
			
		}

		nav.SetDestination (patrolWayPoints [wayPointIndex].position);
	}

	///		DEBUGGING FUNCTIONS		///

	void GotoNextPoint ()
	{
		// Returns if no points have been set up
		if (patrolWayPoints.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		nav.destination = patrolWayPoints [destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % patrolWayPoints.Length;
	}


	void Patroling ()
	{
		if (nav.remainingDistance < nav.stoppingDistance)
			GotoNextPoint ();
	}
}
	
