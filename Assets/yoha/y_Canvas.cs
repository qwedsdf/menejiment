using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Canvas : MonoBehaviour {
	static public bool flg = false;

	// Use this for initialization
	void Start () {
		if (!flg)
		{
			DontDestroyOnLoad(this);
			flg = true;
		}
		else {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
