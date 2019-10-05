using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public List<GameObject> objectsInSpawnPoint = new List<GameObject> ();

	public static SpawnPoint spawnPoint;

	void Awake(){
		if (spawnPoint == null)
			spawnPoint = this;
	}

	void OnTriggerEnter(Collider other){
		objectsInSpawnPoint.Add (other.gameObject);
	}

	void OnTriggerExit(Collider other){
		objectsInSpawnPoint.Remove (other.gameObject);
	}

	public void DestroyAllInSpawnPoint(){
		foreach(GameObject obj in objectsInSpawnPoint){
			if(obj.CompareTag("Hazard") || obj.CompareTag("Enemy"))
				obj.GetComponent<OnDestruction>().playAnimation = false;
			Destroy (obj);
		}
		objectsInSpawnPoint.Clear ();
	}
}
