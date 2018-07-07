using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class y_pickup : MonoBehaviour {
	public GameObject tack;

	public AudioSource tap;
	public AudioSource last_tap;

	public GameObject one;
	public GameObject two;
	public GameObject three;
	public GameObject four;
	public GameObject five;
	public GameObject last;

	public GameObject[] back;
	public string[] distance;
	public GameObject[] coment;

	const int SITASIKUNAI = 0;//親しくない
	const int INDOOR = 1;//インドア
	const int NEGATHIBU = 2;//ネガティブ
	const int DEZAIN = 3;//デザイン派

	public List<string> SITASIKUNAI_1 = new List<string>();
	public List<string> INDOOR_1 = new List<string>();
	public List<string> INDOOR_2 = new List<string>();
	public List<string> INDOOR_3 = new List<string>();
	public List<string> OUTDOOR_1 = new List<string>();
	public List<string> OUTDOOR_2 = new List<string>();
	public List<string> OUTDOOR_3 = new List<string>();
	public List<string> NEGATHIBU_1 = new List<string>();
	public List<string> NEGATHIBU_2 = new List<string>();
	public List<string> NEGATHIBU_3 = new List<string>();
	public List<string> POJITHIBU_1 = new List<string>();
	public List<string> POJITHIBU_2 = new List<string>();
	public List<string> POJITHIBU_3 = new List<string>();
	public List<string> DEZAIN_1 = new List<string>();
	public List<string> DEZAIN_2 = new List<string>();
	public List<string> DEZAIN_3 = new List<string>();
	public List<string> ZITUYOUTEKI_1 = new List<string>();
	public List<string> ZITUYOUTEKI_2 = new List<string>();
	public List<string> ZITUYOUTEKI_3 = new List<string>();

	int num;
	static public int save_num;
	static public int save_lnum;
	int level;
	public Color ija;

	// Use this for initialization
	void Start () {

		if (Get_result.relation == 10) {
			Instantiate (back [Get_result.relation], new Vector2 (0, 1.5f), Quaternion.identity);
			Instantiate (coment [Get_result.relation], new Vector2 (0, -0.5f), Quaternion.identity);
			GameObject.Find ("dis").GetComponent<Text> ().text = distance [Get_result.relation];
		}
		else {
			Instantiate (back [Get_result.relation], new Vector2 (0, 1.5f), Quaternion.identity);
			Instantiate (coment [Get_result.relation], new Vector2 (0, -0.5f), Quaternion.identity);
			GameObject.Find ("dis").GetComponent<Text> ().text = distance [Get_result.relation];
		}

		//挿絵をセレクト
		switch(Get_result.relation/2){
		case 0:
			Instantiate (one,new Vector2(0, -2f), Quaternion.identity);
			break;
		case 1:
			Instantiate (two,new Vector2(0, -2f), Quaternion.identity);
			break;
		case 2:
			Instantiate (three,new Vector2(0, -2f), Quaternion.identity);
			break;
		case 3:
			Instantiate (four,new Vector2(0, -2f), Quaternion.identity);
			break;
		case 4:
			Instantiate (five,new Vector2(0, -2f), Quaternion.identity);
			break;
		case 5:
			Instantiate (last,new Vector2(0, -2f), Quaternion.identity);
			break;
		}

		//出す課題の番号をランダムで選ぶ
		do{
			num = Random.Range (1,4);
		}while(save_num == num);
		save_num = num;

		if (Get_result.relation < 2)level = 0;
		else if (Get_result.relation >= 2&&Get_result.relation < 5)level = 1;
		else if (Get_result.relation >= 5&&Get_result.relation < 8)level = 2;
		else level = 3;

		string txt;
		if (Get_result.relation!=10) {
			do {
				txt = SetPos ();
			} while(txt == "");

		} 
		else {
			txt = "告白をしよう";
		}

		tack.GetComponent<Text> ().text = tack.GetComponent<Text> ().text + txt;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void o(){
		this.tap.Play ();
	}

	public void push_yes(){
		
		Get_result.relation++;
		//DontDestroyOnLoad (GameObject.Find("unko"));
		if (Get_result.relation == 11) {
			last_tap.Play ();
			StartCoroutine (lChecking (() => {
				SceneManager.LoadScene ("title");
			}));
		}
		else {
			o ();
			StartCoroutine(Checking( ()=>{
				SceneManager.LoadScene ("y_load");
			} ));
		}
	}


	public void push_no(){
		o ();
		StartCoroutine(Checking( ()=>{
			SceneManager.LoadScene ("y_load");
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

	private IEnumerator lChecking (functionType callback) {
		while(true) {
			yield return new WaitForFixedUpdate();
			if (!last_tap.isPlaying) {
				callback();
				break;
			}
		}
	}

	string SetPos(){
		int lnum;
		string txd;
		if (level == 0) {
			do{
				lnum = Random.Range (0, SITASIKUNAI_1.Count);
			}while(save_lnum == lnum);
			save_lnum = lnum;
			txd=SITASIKUNAI_1 [lnum];
			SITASIKUNAI_1 [lnum] = "";
			return txd;
		}
		switch(num){
		//インドアかアウトドアか
		case INDOOR:
			if (Get_result.indoor) {
				switch(level){
				case 1:
					do{
						lnum = Random.Range (0, INDOOR_1.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=INDOOR_1 [lnum];
					INDOOR_1 [lnum] = "";
					return txd;
				case 2:
					do{
						lnum = Random.Range (0, INDOOR_2.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=INDOOR_2 [lnum];
					INDOOR_2 [lnum] = "";
					return txd;
				case 3:
					do{
						lnum = Random.Range (0, INDOOR_3.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=INDOOR_3 [lnum];
					INDOOR_3 [lnum] = "";
					return txd;
				}
			}

			else {
				switch(level){
				case 1:
					do{
						lnum = Random.Range (0, OUTDOOR_1.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=OUTDOOR_1 [lnum];
					OUTDOOR_1 [lnum] = "";
					return txd;
				case 2:
					do{
						lnum = Random.Range (0, OUTDOOR_2.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=OUTDOOR_2 [lnum];
					OUTDOOR_2 [lnum] = "";
					return txd;
				case 3:
					do{
						lnum = Random.Range (0, OUTDOOR_3.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=OUTDOOR_3 [lnum];
					OUTDOOR_3 [lnum] = "";
					return txd;
				}
			}
			break;

			//ネガティブかポジティブか
		case NEGATHIBU:
			if (Get_result.negative) {
				switch(level){
				case 1:
					do{
						lnum = Random.Range (0, NEGATHIBU_1.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=NEGATHIBU_1 [lnum];
					NEGATHIBU_1 [lnum] = "";
					return txd;
				case 2:
					do{
						lnum = Random.Range (0, NEGATHIBU_2.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;
					txd=NEGATHIBU_2 [lnum];
					NEGATHIBU_2 [lnum] = "";
					return txd;
				case 3:
					do{
						lnum = Random.Range (0, NEGATHIBU_3.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=NEGATHIBU_3 [lnum];
					NEGATHIBU_3 [lnum] = "";
					return txd;
				}
			}
			else {
				switch(level){
				case 1:
					do{
						lnum = Random.Range (0, POJITHIBU_1.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=POJITHIBU_1 [lnum];
					POJITHIBU_1 [lnum] = "";
					return txd;
				case 2:
					do{
						lnum = Random.Range (0, POJITHIBU_2.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=POJITHIBU_2 [lnum];
					POJITHIBU_2 [lnum] = "";
					return txd;
				case 3:
					do{
						lnum = Random.Range (0, POJITHIBU_3.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=POJITHIBU_3 [lnum];
					POJITHIBU_3 [lnum] = "";
					return txd;
				}
			}
			break;

			//デザインか実用性か
		case DEZAIN:
			if (Get_result.useful) {
				switch(level){
				case 1:
					do{
						lnum = Random.Range (0, ZITUYOUTEKI_1.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=ZITUYOUTEKI_1 [lnum];
					ZITUYOUTEKI_1 [lnum] = "";
					return txd;
				case 2:
					do{
						lnum = Random.Range (0, ZITUYOUTEKI_2.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=ZITUYOUTEKI_2 [lnum];
					ZITUYOUTEKI_2 [lnum] = "";
					return txd;
				case 3:
					do{
						lnum = Random.Range (0, ZITUYOUTEKI_3.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=ZITUYOUTEKI_3 [lnum];
					ZITUYOUTEKI_3 [lnum] = "";
					return txd;
				}
			}
			else {
				switch(level){
				case 1:
					do{
						lnum = Random.Range (0, DEZAIN_1.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=DEZAIN_1 [lnum];
					DEZAIN_1 [lnum] = "";
					return txd;
				case 2:
					do{
						lnum = Random.Range (0, DEZAIN_2.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=DEZAIN_2 [lnum];
					DEZAIN_2 [lnum] = "";
					return txd;
				case 3:
					do{
						lnum = Random.Range (0, DEZAIN_3.Count);
					}while(save_lnum == lnum);
					save_lnum = lnum;

					txd=DEZAIN_3 [lnum];
					DEZAIN_3 [lnum] = "";
					return txd;
				}
			}
			break;
		}
		return "unko";
	}
		
}
