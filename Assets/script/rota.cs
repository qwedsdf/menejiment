﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rota : MonoBehaviour {
	int speed;
	float time;
	GameObject object_all;
	GameObject canvas;

	// Use this for initialization
	void Start () {
		object_all = GameObject.Find ("object");
		canvas = GameObject.Find ("Canvas");
		canvas.SetActive (false);
		object_all.SetActive (false);
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
