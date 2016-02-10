using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TemplateCharacter : MonoBehaviour
{

    // パラメータ調整用の変数
    // パラメータグラフとか表示するときにも使う
    public float Speed = 5f;
    public int Boost = 5;
    public int Accel = 5;

    // 自分かどうか
    public bool isMe = false;

    // MaxAccelをセットする関数SetSpeed
    // AccelTimeをセットする関数SetAccelのために使う
    CharacterMove Move;

    // PlayerNumberをセットする関数SetNumberのために使う
    PlayerData Data;

    // MAX_STAMINAをセットする関数SetStaminaのために使う
    StaminaScript Stamina;

    // 三角アイコンの色を変える関数IconColorのために使う
    public Image Icon;

    Animator anim;
    Rigidbody2D rig;

    public bool OnAnim = false;

    // Use this for initialization
    void Start()
    {
        Data = gameObject.GetComponent<PlayerData>();

        if (OnAnim)
        {
            anim = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
        }
        if (isMe)
        {
            Move = gameObject.GetComponent<CharacterMove>();
            Stamina = gameObject.GetComponent<StaminaScript>();
            
            // パラメータを1~5に調整
            Speed = MaxSetting(Speed);
            Boost = MaxSetting(Boost);
            Accel = MaxSetting(Accel);
            
            // 最高速度を変更
            SetSpeed(25f + 5f * Speed);
            // 加速までの時間を変更
            SetAccel(30 - 2 * Accel);
            // スタミナの最大値を変更
            Stamina.ChangeMax(500 + 100 * Boost);
        }
        // 三角アイコンの色を変更
        IconColor(GetPlayerNumber());
        }
	
	// Update is called once per frame
	void Update()
    {
        if (Icon.color == Color.white) {
            IconColor(GetPlayerNumber());
        }
        if (isMe && OnAnim)
        {
            // アニメーションのためにAnimatorに値を送信する
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", rig.velocity.y);
            anim.SetBool("isGround", Move.GetIsGround());
        }
    }

    public void SetNumber(int number)
    {
        if(number >= 1 && number <= 4)
        {
            Data.PlayerNumber = number;
            IconColor(GetPlayerNumber());
        }
    }

    float MaxSetting(float value)
    {
        if (value > 5f) value = 5f;
        if (value< 1f) value = 1f;

        return value;
    }

    void SetSpeed(float speed)
    {
        Move.MaxAccel = speed;
    }

    void SetAccel(int accel)
    {
        Move.AccelTime = accel;
    }

    int MaxSetting(int value)
    {
        if (value > 5) value = 5;
        if (value< 1) value = 1;

        return value;
    }

    int GetPlayerNumber()
    {
        return Data.PlayerNumber;
    }

    //　頭上のアイコンの色を変更
    void IconColor(int number)
    {
        Color color;

        switch (number) {
            case 1:
                color = Color.green;
                break;
            case 2:
                color = Color.yellow;
                break;
            case 3:
                color = Color.blue;
                break;
            case 4:
                color = Color.red;
                break;
            default:
                color = Color.white;
                break;
        }

        Icon.GetComponent<PlayerIcon>().SetPlayerNumber(number);

        Icon.color = color;
    }
}