using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class y_pickup_2 : MonoBehaviour {
	const int SITASIKUNAI = 0;//親しくない
	const int INDOOR = 1;//インドア
	const int NEGATHIBU = 2;//ネガティブ
	const int DEZAIN = 3;//デザイン派
	const int OUTDOOR = 4;//アウトドア
	const int POJITHIBU = 5;//ポジティブ
	const int ZITUYOUTEKI = 6;//機能派
	const int LEVEL_MAX=3;

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

	//CSV読み込み関係
	public List<string> file_name;
	int height;
	char[] csvDatas=new char[10000];

	//課題の文言 All_text[種類][レベル][文言の番号]
	List<List<List<string>>> All_text = new List<List<List<string>>>();


	int num;
	static public int save_num;
	static public int save_lnum;
	int level;
	public Color ija;

	// Use this for initialization
	void Start () {
		/*Instantiate (back [Get_result.relation], new Vector2 (0, 1.5f), Quaternion.identity);
		Instantiate (coment [Get_result.relation], new Vector2 (0, -0.5f), Quaternion.identity);
		GameObject.Find ("dis").GetComponent<Text> ().text = distance [Get_result.relation];

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

		tack.GetComponent<Text> ().text = tack.GetComponent<Text> ().text + txt;*/
		Input_text ();
	}

	// Update is called once per frame
	void Update () {

	}

	//テキスト読み込みは確認ずみ
	void Input_text(){
		foreach(string name in file_name){
			Load_Text (name, ref All_text);
		}
	}

	//CSV読み込み
	//一回通ると一つの性格分の文章を全部入れる
	void Load_Text(string file_name,ref List<List<List<string>>> all_text_box){
		string text="";
		List<List<string>> text_box = new List<List<string>>();
		int loop_num = LEVEL_MAX;
		if (all_text_box.Count == 0)loop_num = 1;

		for (int i = 0; i < loop_num; i++) {
			string _file_name="CSV/" + file_name + i.ToString();
			TextAsset csv = Resources.Load( _file_name) as TextAsset;
			StringReader reader = new StringReader(csv.text);
			List<string> text_all = new List<string>();

			while (reader.Peek() > -1) {
				reader.ReadBlock (csvDatas,0,csvDatas.Length);
			}
				
			foreach(char moji in csvDatas){
				if (moji == '\n')continue;
				//文章の終わりに、リストに追加
				if (moji == '/'||moji == '+') {
					text_all.Add (text);
					text = "";
					//全ての文言が入っているリストに追加
					if (moji == '+') {
						text_box.Add (text_all);
						break;
					}
					continue;
				}
				else {
					text += moji;
				}
			}
		}
		all_text_box.Add (text_box);
	}

	/*void o(){
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
				SceneManager.LoadScene ("load");
			} ));
		}
	}


	public void push_no(){
		o ();
		StartCoroutine(Checking( ()=>{
			SceneManager.LoadScene ("load");
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
			txd=All_text[SITASIKUNAI][0] [lnum];
			return txd;
		}
		switch(num){
		//インドアかアウトドアか
		case INDOOR:
			if (Get_result.indoor) {
				List<List<string>> text_box = new List<List<string>>();
				do{
				///////////要素の最大値をどーやって求めるか考えよう///////////////
					lnum = Random.Range (0, INDOOR_1.Count);
				}while(save_lnum == lnum);
				save_lnum = lnum;
				txd=All_text[INDOOR][level-1] [lnum];
				return txd;
			}

			else {
				do{
					lnum = Random.Range (0, OUTDOOR.Count);
				}while(save_lnum == lnum);
				save_lnum = lnum;
				txd=All_text[OUTDOOR][level-1] [lnum];
				return txd;
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
	}*/

}
