using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	// Use this for initialization
	[SerializeField] public Light light;
	void Start () {
		if(light.enabled==true)
			light.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
