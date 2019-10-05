using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour {

	AudioSource[] sounds;

	void Start(){
		sounds = GetComponents<AudioSource> ();
	}

	public void PlayEnter(){
		sounds [1].Play ();
	}
	public void PlayExit(){
		sounds [2].Play ();
	}
}
