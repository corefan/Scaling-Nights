using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using System;
using System.CodeDom.Compiler;
using UnityEngine.UI;

[RequireComponent (typeof(Collider))]
public class Container : MonoBehaviour
{
	public GameObject[] items;
	[SerializeField]
	private GameObject containerController;
	[SerializeField]
	private ContainerUIController uicontroller;
	[SerializeField]
	private RandomContainerFiller filler;

	// Use this for initialization
	void Start ()
	{
		filler = FindObjectOfType<RandomContainerFiller> ();
		containerController = FindObjectOfType<ContainerUIController> ().gameObject;
		uicontroller = containerController.GetComponent <ContainerUIController> ();
		items = filler.RandomFill ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Open ()
	{
		
		uicontroller.RemoveAll ();
		for (int i = 0; i < items.Length; i++) {
			uicontroller.AddItem (items [i]);
		}
		uicontroller.SetActive (true);

	}

	public void Close ()
	{
		uicontroller.SetActive (false);
	}



}
