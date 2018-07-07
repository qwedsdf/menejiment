using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Get_result : MonoBehaviour {
	public ToggleGroup toggleGroup;
	public ToggleGroup  toggleGroup2;
	public ToggleGroup  toggleGroup3;
	public ToggleGroup  toggleGroup4;
	public ToggleGroup  toggleGroup5;
	public ToggleGroup  toggleGroup6;

	public static int relation; 
	public static int status;

	public static bool indoor;
	public static bool negative;
	public static bool active;
	public static bool useful;
	public AudioSource tap;

	// Use this for initialization
	void Start () {
		relation = 0;
		status = 0;
	}

	// Update is called once per frame
	void Update () {

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
	public void onClick(){
		this.tap.Play ();
		string selectedLabel = toggleGroup.ActiveToggles()
			.First().GetComponentsInChildren<Text>()
			.First(t => t.name == "Label").text;
		string selectedLabel2 = toggleGroup2.ActiveToggles()
			.First().GetComponentsInChildren<Text>()
			.First(t => t.name == "Label").text;

		string selectedLabel3 = toggleGroup3.ActiveToggles()
			.First().GetComponentsInChildren<Text>()
			.First(t => t.name == "Label").text;
		string selectedLabel4 = toggleGroup4.ActiveToggles()
			.First().GetComponentsInChildren<Text>()
			.First(t => t.name == "Label").text;

		string selectedLabel5 = toggleGroup5.ActiveToggles()
			.First().GetComponentsInChildren<Text>()
			.First(t => t.name == "Label").text;

		string selectedLabel6 = toggleGroup6.ActiveToggles()
			.First().GetComponentsInChildren<Text>()
			.First(t => t.name == "Label").text;

		reusult (selectedLabel, selectedLabel2, selectedLabel3, selectedLabel4, selectedLabel5, selectedLabel6);
	
	}

	void  reusult(string res1,string res2,string res3,string res4,string res5,string res6){
		if (res1 == "友達") {
			relation = 2;
		} else if (res1 == "知り合い") {
			relation = 1;
		} else if (res1 == "他人") {
			relation = 0;
		} 

		if (res2 == "非常に良い") {
			status = 0;
		} else if (res2 == "まあまあ良い") {
			status = 1;
		} else if (res2 == "あまり良くない") {
			status = 2;
		}
		else if(res2=="良くない"){
			status = 3;
		}


		if (res3 == "4") {
			indoor = true;
		} else if (res3 == "3") {
			indoor = true;
		} else if (res3 == "2") {
			indoor = false;
		} else if (res3 == "1") {
			indoor = false;
		}

		if (res4 == "4") {
			negative = true;
		} else if (res4 == "3") {
			negative = true;
		} 
		else if(res4 == "2"){
			negative = false;
		}
		else if (res4 == "1") {
			negative = false;
		} 


		if (res5== "4") {
			active = true;
		} else if (res5 == "3") {
			active = true;
		} 
		else if(res5== "2"){
			active = false;
		}
		else if (res5 == "1") {
			active = false;
		} 


		if (res6 == "4") {
			useful = true;
		} else if (res6 == "3") {
			useful = true;
		} 
		else if(res6 == "2"){
			useful = false;
		}
		else if (res6 == "1") {
			useful = false;
		} 
		this.tap.Play ();
		StartCoroutine(Checking( ()=>{
			SceneManager.LoadScene ("N_ready_target");
		} ));
	}
}

