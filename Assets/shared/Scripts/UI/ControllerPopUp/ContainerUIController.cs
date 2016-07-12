using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngineInternal;

public class ContainerUIController : AbstractPopUpController
{

	// Use this for initialization
	void Start ()
	{
		transform.GetChild (0).gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

		
}
