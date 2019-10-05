using UnityEngine;

public class BGScroller : MonoBehaviour {

	Vector3 startPos;
	public float scrollSpeed = -1f;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
			float newPos = Mathf.Repeat (Time.time * scrollSpeed, 28f);
			transform.position = startPos + Vector3.forward * newPos;
	}
}
