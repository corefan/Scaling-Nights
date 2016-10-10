using UnityEngine;
using System.Collections;

public class FPSInput : MonoBehaviour
{
	public float speed;
	public float walkSpeed = 2.0f;
	public float runSpeed = 8.0f;
	[SerializeField] private Animator _animator;
	private CharacterController _charController;
	private float gravity = -9.8f;

	private AudioSource footSound;
	public float walkAudioSpeed = 0.4f;
	public float runAudioSpeed = 0.35f;

	private float walkAudioTimer = 0.0f;
	private float runAudioTimer = 0.0f;
	// Use this for initialization
	void Start ()
	{
		speed = walkSpeed;
		_charController = GetComponent<CharacterController> ();	
		footSound = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!(GameEvent.isPause || GameEvent.isUiEnabled)) {
			if (Input.GetButton ("Run")) {
				speed = runSpeed;
			} else {
				speed = walkSpeed;
			}
			float deltaX = Input.GetAxis ("Horizontal") * speed;
			PlayFootStep ();
			float deltaZ = Input.GetAxis ("Vertical") * speed;
			Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
			_animator.SetFloat ("Speed", movement.magnitude);
			movement = Vector3.ClampMagnitude (movement, speed);
			movement.y = gravity;
			movement *= Time.deltaTime;
			movement = transform.TransformDirection (movement);

			_charController.Move (movement);
		}
	}

	void PlayFootStep ()
	{
		if (Input.GetAxis ("Horizontal") != 0.0f || Input.GetAxis ("Vertical") != 0.0f) {
			if (speed == walkSpeed) {
				if (walkAudioTimer > walkAudioSpeed) {
					footSound.Stop ();
					footSound.Play ();
					walkAudioTimer = 0.0f;
				}
			} else {
				if (runAudioTimer > runAudioSpeed) {
					footSound.Stop ();
					footSound.Play ();
					runAudioTimer = 0.0f;
				}
			}

		} else {
			footSound.Stop ();
		}
		walkAudioTimer += Time.deltaTime;
		runAudioTimer += Time.deltaTime;
	}
}
