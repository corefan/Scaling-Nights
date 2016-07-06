using UnityEngine;
using System.Collections;

public class FPSInput1 : MonoBehaviour
{
	private float speed;
	public float startSpeed;
	public float accel;
	public float walk;
	private CharacterController _charController;
	private float gravity = -9.8f;

	// Use this for initialization
	void Start ()
	{
		_charController = GetComponent<CharacterController> ();
		speed = 8.0f;
		accel = 2.5f;
		walk = 2.0f;
		startSpeed = 8.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);

		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = startSpeed * accel;
		} else if (Input.GetKey (KeyCode.C)) {
			speed = startSpeed / walk;
		} else {
			speed = startSpeed;
		}


		movement = Vector3.ClampMagnitude (movement, speed);
		movement.y = gravity;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_charController.Move (movement);
//		transform.Translate (deltaX * Time.deltaTime, deltaZ * Time.deltaTime, 0);
	}
}
