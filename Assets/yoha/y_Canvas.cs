using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Canvas : MonoBehaviour {
	static public y_Canvas instans;

	// Use this for initialization
	void Start () {
		if (instans==null)
		{
			instans = this;
			DontDestroyOnLoad(this);
		}
		else {
			Destroy (this.gameObject);
		}
	}

}
