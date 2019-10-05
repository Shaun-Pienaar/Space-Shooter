using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public static List<Rigidbody> rbs = new List<Rigidbody>();
	public static int childCount;

	void Start(){
		transform.GetComponentsInChildren<Rigidbody>(rbs);
		childCount = transform.childCount;
	}

	void FixedUpdate () {
		if(transform.childCount > childCount){
			transform.GetComponentsInChildren<Rigidbody>(rbs);
			childCount = transform.childCount;
		}
		float inputHorizontal = Input.GetAxis ("Horizontal");
		float inputVertical = Input.GetAxis ("Vertical");
		Vector3 inputTotal = new Vector3 (inputHorizontal, 0.0f, inputVertical);

		foreach (Rigidbody rb in rbs) {
			rb.velocity = inputTotal.normalized * 9.0f;
			rb.position = new Vector3 (Mathf.Clamp (rb.position.x, 
													-6.0f + (rb.transform.GetSiblingIndex() * 1.5f), 
													6.0f - ((childCount - (rb.transform.GetSiblingIndex() + 1f)) * 1.5f)
													), 
										0.0f, 
										Mathf.Clamp (rb.position.z, -4.0f, 8.0f)
										);
			rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -4.0f);
		}
	}
}
