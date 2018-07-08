using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class y_setActive : MonoBehaviour {
	public static y_setActive instans;
	GameObject object_all;
	GameObject canvas;

	void Awake(){
		if (instans==null)
		{
			instans = this;
			DontDestroyOnLoad(this);
		}
		else {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		object_all = GameObject.Find ("object");
		canvas = GameObject.Find ("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "y_task") {
			canvas.SetActive (true);
			object_all.SetActive (true);
		}
	}
}
