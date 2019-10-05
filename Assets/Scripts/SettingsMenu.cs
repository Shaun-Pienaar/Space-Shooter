using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

	public AudioMixer mainMixer;

	public void ToggleHardMode(bool hardMode){
		GameManager.hardMode = hardMode;
	}

	public void SetFullScreen(bool isFullScreen){
		Screen.fullScreen = isFullScreen;
	}

	public void SetVolume(float volume){
		mainMixer.SetFloat ("Volume", volume);
	}

	public void SetGraphicsQuality(int qualityIndex){
		QualitySettings.SetQualityLevel (qualityIndex);
	}
}
