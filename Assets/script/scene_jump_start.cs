using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_jump_start : MonoBehaviour {
	public AudioSource tap;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void onCliclk(){
		this.tap.Play ();
		StartCoroutine(Checking( ()=>{
			SceneManager.LoadScene ("y_task");
		} ));

	}
	public delegate void functionType();
	private IEnumerator Checking (functionType callback) {
		while(true) {
			yield return new WaitForFixedUpdate();
			if (!tap.isPlaying) {
				callback();
				break;
			}
		}
	}
}
