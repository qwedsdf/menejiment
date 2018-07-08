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

	public GameObject[] face;


	public GameObject[] back;
	public string[] distance;
	public GameObject[] coment;

	//CSV読み込み関係
	public List<string> file_name;
	char[] csvDatas=new char[10000];

	//課題の文言 All_text[種類][レベル][文言の番号]
	List<List<List<string>>> All_text = new List<List<List<string>>>();

	List<List<int>> size = new List<List<int>>();

	static public bool once=false;
	int num;
	static public int save_num;
	static public int save_lnum;
	int level;
	public Color ija;


	// Use this for initialization
	void Start () {
		if (!once) {
			DontDestroyOnLoad (this);
			once = true;
		}
		else {
			Destroy (this.gameObject);
		}
	}

	void OnEnable(){
		Debug.Log ("通ってる");
		format ();
	}

	void format(){
		if(!once)Input_text ();
		Instantiate (back [Get_result.relation], new Vector2 (0, 1.5f), Quaternion.identity);
		Instantiate (coment [Get_result.relation], new Vector2 (0, -0.5f), Quaternion.identity);
		GameObject.Find ("dis").GetComponent<Text> ().text = distance [Get_result.relation];

		//女の子をセレクト
		Instantiate (face[Get_result.relation/2],new Vector2(0, -2f), Quaternion.identity);

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
		tack.GetComponent<Text> ().text = "";
		if (Get_result.relation!=10) {
			do {
				txt = SetPos ();
			} while(txt == "");

		} 
		else {
			txt = "告白をしよう";
		}
		tack.GetComponent<Text> ().text += txt;
	}

	// Update is called once per frame
	void Update () {

	}

	//テキスト読み込みは確認ずみ
	void Input_text(){
		Debug.Log ("通ってる");
		foreach(string name in file_name){
			Load_Text (name, ref All_text);
		}
	}

	//CSV読み込み
	//一回通ると一つの性格分の文章を全部入れる
	void Load_Text(string file_name,ref List<List<List<string>>> all_text_box){
		string text="";
		List<List<string>> text_box = new List<List<string>>();
		List<int> _size = new List<int>();
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
						_size.Add (text_all.Count);
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
		size.Add (_size);
		all_text_box.Add (text_box);
	}

	void o(){
		this.tap.Play ();
	}

	//やったを押した場合
	public void push_yes(){
		Get_result.relation++;
		if (Get_result.relation == 11) {
			last_tap.Play ();
			StartCoroutine (lChecking (() => {
				SceneManager.LoadScene ("y_title");
				Destroy(this.gameObject);
				Destroy(GameObject.Find("Canvas").gameObject);
				once=false;
			}));
		}
		else {
			o ();
			StartCoroutine(Checking( ()=>{
				SceneManager.LoadScene ("y_load");
			} ));
		}
	}

	//やってない場合
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

	//音が鳴り終わったら戻る
	private IEnumerator lChecking (functionType callback) {
		while(true) {
			yield return new WaitForFixedUpdate();
			if (!last_tap.isPlaying) {
				callback();
				break;
			}
		}
	}

	//文言をテキストにセットする
	string SetPos(){
		int lnum;
		string txd;
		if (level == 0) {
			do {
				lnum = Random.Range (0, size [SITASIKUNAI] [0]);
			} while(save_lnum == lnum);
			save_lnum = lnum;
			txd = All_text [SITASIKUNAI] [0] [lnum];
			return txd;
		}

		switch (num) {
		//インドアかアウトドアか
		case INDOOR:
			if (Get_result.indoor) {
				do {
					lnum = Random.Range (0, size [INDOOR] [level - 1]);
				} while(save_lnum == lnum);
				save_lnum = lnum;
				txd = All_text [INDOOR] [level - 1] [lnum];
				return txd;
			} else {
				do {
					lnum = Random.Range (0, size [OUTDOOR] [level - 1]);
				} while(save_lnum == lnum);
				save_lnum = lnum;
				txd = All_text [OUTDOOR] [level - 1] [lnum];
				return txd;
			}
			break;

		//ネガティブかポジティブか
		case NEGATHIBU:
			if (Get_result.negative) {
				do {
					lnum = Random.Range (0, size [NEGATHIBU] [level - 1]);
				} while(save_lnum == lnum);
				save_lnum = lnum;
				txd = All_text [NEGATHIBU] [level - 1] [lnum];
				return txd;
			} else {
				do {
					lnum = Random.Range (0, size [POJITHIBU] [level - 1]);
				} while(save_lnum == lnum);
				save_lnum = lnum;
				txd = All_text [POJITHIBU] [level - 1] [lnum];
				return txd;
			}
			break;

		//デザインか実用性か
		case DEZAIN:
			if (Get_result.useful) {
				do {
					lnum = Random.Range (0, size [DEZAIN] [level - 1]);
				} while(save_lnum == lnum);
				save_lnum = lnum;
				txd = All_text [DEZAIN] [level - 1] [lnum];
				return txd;
			} else {
				do {
					lnum = Random.Range (0, size [ZITUYOUTEKI] [level - 1]);
				} while(save_lnum == lnum);
				save_lnum = lnum;
				txd = All_text [ZITUYOUTEKI] [level - 1] [lnum];
				return txd;
				break;
			}
		}
		return "error";
	}

}
