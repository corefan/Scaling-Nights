using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using System;
using System.CodeDom.Compiler;

[RequireComponent (typeof(Collider))]
public class ContainerItem : MonoBehaviour
{
	public GameObject[] items;
	public GameObject containerui;

	//public RandomContainerFiller generator;
	private RandomContainerFiller filler;
	// Use this for initialization
	void Start ()
	{
		filler = FindObjectOfType<RandomContainerFiller> ();
		items = filler.RandomFill ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


}
