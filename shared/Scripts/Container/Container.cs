using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using System;
using System.CodeDom.Compiler;
using UnityEngine.UI;

[RequireComponent (typeof(Collider))]
public class Container : AbstractContainer
{
	[SerializeField] 
	private RandomContainerFiller filler;

	// Use this for initialization
	void Start ()
	{
		// fill 
		filler = FindObjectOfType<RandomContainerFiller> ();
		items = filler.RandomFill ();
		// find the ui controller
		_uiController = GameObject.Find ("ContainerHUD").GetComponent <ContainerUIController> ();

	}

	public new void Open ()
	{
		_uiController._container = GetComponent <Container> ();
		base.Open ();
	}

		
}
