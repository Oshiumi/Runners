using UnityEngine;
using System.Collections;

public class GimicButton : MonoBehaviour {

	// gameobject
	public GameObject gate;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
			
	}

	// ボタンに触れた時
	void OnTriggerEnter2D(Collider2D col)
	{
        // コンポーネント取得
        var ga = gate.gameObject.GetComponent<StageGimic>();

        // ギミックが動いていないときのみ
        if (ga.isMove == false) {
            // プレイヤーのみ
            if (col.gameObject.tag == "Player") {
                ga.isUp = !ga.isUp;
		
                ga.MoveGimic ();
		
                gameObject.GetComponent<SpriteRenderer> ().enabled = false;
            }
        }
	}

	// ボタンから離れた時
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

}
