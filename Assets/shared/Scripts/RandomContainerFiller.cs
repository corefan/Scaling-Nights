using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class RandomContainerFiller : MonoBehaviour
{

	public GameObject[] prefabs;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public GameObject[] RandomFill ()
	{
		int number = (int)Random.Range (0.5f, 3f);
		GameObject[] selected = new GameObject[number];
		for (int i = 0; i < number; i++) {
			selected [i] = prefabs [(int)Random.Range (0f, prefabs.LongLength)];
		}
		return selected;
	}
}
