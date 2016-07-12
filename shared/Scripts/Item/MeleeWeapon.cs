using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class MeleeWeapon : Item
{
	[SerializeField]
	private Animator _animator;
	private bool attack;

	void Awake ()
	{
		tag = "Weapon";
	}

	// Use this for initialization
	void Start ()
	{
		_animator = GameObject.Find (gameObject.name).GetComponent <Animator> ();
		attack = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		attack = false;
		if (Input.GetButtonDown ("Fire1")) {
			attack = true;
		}
		_animator.SetBool ("Attack", attack);
	
	}

	public new void Use ()
	{
		if (transform.parent.gameObject.name.Equals ("InventoryHUD")) {
			Debug.Log ("The Weapon is in the inventory");
		}
	}

	private void Attack ()
	{
		
	}

}
