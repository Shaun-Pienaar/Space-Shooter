using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Mute : MonoBehaviour {

	public AudioMixer mixer;

	float currentVol;
	bool muted;

	public void MuteOrUnmute(){
		if(!muted){
			mixer.GetFloat ("Volume", out currentVol);
			mixer.SetFloat ("Volume", -80f);
			muted = true;
		}
		else{
			mixer.SetFloat ("Volume", currentVol);
			muted = false;
		}
	}
}
