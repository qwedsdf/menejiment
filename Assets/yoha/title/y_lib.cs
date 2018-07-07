using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


struct Info{
	public int Level;
	public int type;
	public string task;
	public bool result;
};

public class y_lib : MonoBehaviour {
	//タイプ
	const int MAX = 6;

	List<Info> TaskDate = new List<Info>();
	public int[] level;
	public int[] type;
	public string[] task;


	// Use this for initialization
	void Start () {
		Info inf=new Info();
		for (int i = 0; i < MAX; i++) {
			inf.Level = level[i];
			inf.type = type[i];
			inf.result = false;
			inf.task += task [i];
			TaskDate.Add (inf);
		}
		

		//inf.task="unko";
		Debug.Log (TaskDate[0].result);
		Debug.Log (TaskDate[0].task);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
