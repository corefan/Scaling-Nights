using UnityEngine;
using System.Collections;
using UnityEditor.AnimatedValues;

[RequireComponent (typeof(Camera))]
public class FungusEffect : MonoBehaviour
{
	private Camera _camera;
	private bool near = true;
	public  float max = 70f;
	public  float min = 50f;

	public float fungusRate = 1.0f;
	public bool activated = false;

	// Use this for initialization
	void Start ()
	{
		_camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (Time.deltaTime);
		if (activated) {
			ApplyDrugs ();
		} else {
			if (_camera.fieldOfView != 60) {
				if (_camera.fieldOfView > 60) {
					_camera.fieldOfView = Mathf.Lerp (60, _camera.fieldOfView, fungusRate * Time.deltaTime);
				} else {
					_camera.fieldOfView = Mathf.Lerp (_camera.fieldOfView, 60, fungusRate * Time.deltaTime);
				}
			}
		}
	}

	private void ApplyDrugs ()
	{
		if (!near) {
			if (_camera.fieldOfView - 1 >= min) {
				_camera.fieldOfView = Mathf.Lerp (_camera.fieldOfView, min, fungusRate * Time.deltaTime);
			} else {
				near = true;
			}
		} else {
			if (_camera.fieldOfView + 1 <= max) {
				_camera.fieldOfView = Mathf.Lerp (_camera.fieldOfView, max, fungusRate * Time.deltaTime);
			} else {
				near = false;
			}
		}
	}


	public void Activate (float motionRate, float rate)
	{
		activated = true;
		fungusRate = rate;
		Time.timeScale = motionRate;
	}

	public void Deactivate ()
	{
		activated = false;
		Time.timeScale = 1f;
	}
}
