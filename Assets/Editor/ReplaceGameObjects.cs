using UnityEngine;
using UnityEditor;
using System.Collections;

// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010
// Modifed by Antonio Fortino 9/27/2016 - Added Other button function and minor bug fix
using System;

public class ReplaceGameObjects : ScriptableWizard
{
	public bool copyValues = true;
	public GameObject oldGameObjectSelector;
	public GameObject newGameObject;
	public GameObject oldGameObjectsParent;
	private Transform[] Replaces;

	[MenuItem ("Custom/Replace GameObjects")]


	static void CreateWizard ()
	{
		ScriptableWizard.DisplayWizard ("Replace GameObjects", typeof(ReplaceGameObjects), "Replace", "Delete old ones"); 
	}

	void OnWizardCreate ()
	{
		Replaces = oldGameObjectsParent.GetComponentsInChildren<Transform> ();
		for (int i = 1; i < Replaces.Length; i++) {
			if (Replaces [i].name.Equals (oldGameObjectSelector.name)) {
				GameObject newObject;
				newObject = (GameObject)EditorUtility.InstantiatePrefab (newGameObject);
				newObject.transform.SetParent (Replaces [i].parent);
				newObject.transform.position = Replaces [i].position;
				newObject.transform.rotation = Replaces [i].rotation;
				newObject.transform.localScale = Replaces [i].localScale;
			}

		}
	}

	void OnWizardOtherButton ()
	{
		Replaces = oldGameObjectsParent.GetComponentsInChildren<Transform> ();
		string selector = oldGameObjectSelector.name;
		oldGameObjectSelector = null;
		for (int i = 1; i < Replaces.Length; i++) {
			if (Replaces [i].name.Equals (selector)) {
				DestroyImmediate (Replaces [i].gameObject, false);
			}
		}
	}
}