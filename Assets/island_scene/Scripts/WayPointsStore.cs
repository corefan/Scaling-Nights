using UnityEngine;
using System.Collections;

public class WayPointsStore : MonoBehaviour {
	[SerializeField] private Transform[] wayStored;

	public Transform[] getWayStored() {
		return wayStored;	
	}

	public Transform getOneWayStored(int i) {
		if (i >= wayStored.Length)
			return null;
		
		return wayStored[i];	
	}
}
