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
	public GameObject containerController;

	//public RandomContainerFiller generator;
	private RandomContainerFiller filler;

	void Awake ()
	{
		Messenger.AddListener (GameEvent.FAR_INTERACTIVE, Close);
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEvent.FAR_INTERACTIVE, Close);
	}
	// Use this for initialization
	void Start ()
	{
		filler = FindObjectOfType<RandomContainerFiller> ();
		containerController = FindObjectOfType<ContainerUIController> ().gameObject;
		items = filler.RandomFill ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Open ()
	{
		ContainerUIController uicontroller = containerController.GetComponent <ContainerUIController> ();
		uicontroller.RemoveAll ();
		for (int i = 0; i < items.Length; i++) {
			Sprite sprite = items [i].GetComponent<Image> ().sprite;
			uicontroller.AddItem (sprite);
		}
		containerController.SetActive (true);

	}

	public void Close ()
	{
		containerController.SetActive (false);
	}



}
