using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEditor.Animations;


[RequireComponent (typeof(Bloom))]
[RequireComponent (typeof(MotionBlur))]
public class CameraEffectController : MonoBehaviour
{
	private Bloom _bloom;
	private MotionBlur _blur;
	// Use this for initialization
	void Start ()
	{
		_bloom = GetComponent <Bloom> ();
		_blur = GetComponent <MotionBlur> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Activate ()
	{
		_bloom.bloomIntensity = 1f;
		_blur.blurAmount = 0.69f;
		_blur.extraBlur = true;
	}

	public void Deactivate ()
	{
		_bloom.bloomIntensity = 0.1f;
		_blur.blurAmount = 0.1f;
		_blur.extraBlur = false;
	}
}
