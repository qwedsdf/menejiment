using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rota : MonoBehaviour {
	int speed;
	float time;
	// Use this for initialization
	void Start () {
		speed = -15;
		time = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, 1),speed*time);
		time+=Time.deltaTime;
		if(time>=1f){
			SceneManager.LoadScene ("y_task");
		}
	}
}
