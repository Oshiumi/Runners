using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove : Photon.MonoBehaviour
{
    // MultiPlayer用の変数
    public bool LocalMultiPlayer = false;
    public bool FirstPlayer = true;
    float speed1 = 0, speed2 = 0;
    const float DownSpeed = 0.05f;
    public int playerNum = -1;

    private PlayerData pd;
    private PunScript pun;

    // Moveで使う変数
    public float speed = 5.0f;
    public float MaxAccel = 20f;
    public float UpAccel = 1.0f;
    public float UpBrake = 1.0f;
    public int AccelTime = 60;
    public int BrakeTime = 30;
    float accel;
    float brake;
    float down = 0;
    int accelCount = 0;
    int brakeCount = 0;
    float turn;
    bool Running = true;
    public int BrakeWait = 30;
    int RunningCount = 0;

    // Jumpで使う変数
    bool isGround;
    public float JumpPower = 5f;
    public float GroundPlusY = 0;
    public LayerMask Ground;
    private GameObject havingItemUI;
    GameObject havingItem;

    // ブースト時の変数
    public float BoostJumpPower = 8f;
    public float BoostSpeed = 8.0f;
    bool isBoosting = false;

    // Damageで使う変数
    public Vector2 backwardForce = new Vector2(-4.5f, 2.0f);
    public float damagetime = 0.04f;
    bool isDamage = false;

    // Use this for initialization

    void Start() {
        havingItemUI = GameObject.Find("Image");
        Ground = 1 << LayerMask.NameToLayer("Ground");
        pun = (GameObject.Find ("PunManager")).GetComponent<PunScript> ();
        pd = GetComponent<PlayerData> ();
        havingItem = null;
        IsGoaled = false;
        accel = 0;
        brake = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamage)
        {
            return;
        }

        isBoosting = GetComponent<StaminaScript>().IsBoosting;

        if (LocalMultiPlayer)
        {
            MultiMove();
            MultiJump();
        }
        else
        {
            Move();
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            UseItem();
        }
    }

    void Move()
    {
        bool isOverlappingDashboard = GetComponent<CheckOverlapDashBoard>().IsOverlapping;

        float move = Input.GetAxis("Horizontal");

        if (Running && isGround && !isBoosting)
        {
            Accel(move);
            Brake(move);
        }
        
        if (isOverlappingDashboard)
        {
            accel = MaxAccel;
        }

        
        if (brake != 0 && brake >= Mathf.Abs(accel))
        {
            brake = 0;
            accel = 0;
            brakeCount = 0;
            Running = false;
        }

        if (!Running)
        {
            RunningCount++;
            if (RunningCount > BrakeWait)
            {
                RunningCount = 0;
                Running = true;
            }
        }

        float s = isBoosting ? BoostSpeed : speed;

        if (move != 0) turn = (move > 0 && accel < 0) || (move < 0 && accel > 0) ? -1.0f : 1.0f;

        if (isBoosting) transform.Translate(new Vector2(Mathf.Abs(move) * s * Time.deltaTime, 0));
        else if (Running) transform.Translate(new Vector2((Mathf.Abs(move) * s + (Mathf.Abs(accel) * turn) - brake) * Time.deltaTime, 0));


        if (Mathf.Abs(move) > 0)
        {
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);
        }
    }

    void MultiMove()
    {
        if (FirstPlayer)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (speed1 > -1.0f)
                {
                    speed1 -= 0.2f;
                }
            }
            else
            {
                if (speed1 < 0)
                {
                    speed1 += DownSpeed;
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (speed1 < 1.0f)
                {
                    speed1 += 0.2f;
                }
            }
            else
            {
                if (speed1 > 0)
                {
                    speed1 -= DownSpeed;
                }
            }

            if (speed1 > -0.2f && speed1 < 0.2f)
            {
                speed1 = 0;
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (speed2 > -1.0f)
                {
                    speed2 -= 0.2f;
                }
            }
            else
            {
                if (speed2 < 0)
                {
                    speed2 += DownSpeed;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (speed2 < 1.0f)
                {
                    speed2 += 0.2f;
                }
            }
            else
            {
                if (speed2 > 0)
                {
                    speed2 -= DownSpeed;
                }
            }

            if (speed2 > -0.2f && speed2 < 0.2f)
            {
                speed2 = 0;
            }
        }


        float move;

        if (FirstPlayer)
        {
            move = speed1;
        }
        else
        {
            move = speed2;
        }


        transform.Translate(new Vector2(Mathf.Abs(move) * speed * Time.deltaTime, 0));

        if (Mathf.Abs(move) > 0)
        {
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);
        }

    }

    void Accel(float move)
    {
        if (move >= 0.9f && accel >= 0)
        {
            if (accel < MaxAccel)
            {
                accelCount++;
                if (accelCount >= AccelTime)
                {
                    accelCount = 0;
                    accel += UpAccel;
                }
            }
        }
        else if (move <= -0.9f && accel <= 0)
        {
            if (accel > -MaxAccel)
            {
                accelCount++;
                if (accelCount >= AccelTime)
                {
                    accelCount = 0;
                    accel -= UpAccel;
                }
            }
        }
        else
        {
            if (accel > 0)
            {
                if (accelCount % AccelTime == 0) accel -= UpAccel;
                accelCount++;
            }
            else if (accel < 0)
            {
                if (accelCount % AccelTime == 0) accel += UpAccel;
                accelCount++;
            }

            if (accel == 0) accelCount = 0;
        }

        if (accel < UpAccel && accel > -UpAccel) accel = 0;
    }

    void Brake(float move)
    {
        if ((accel > 0 && move < 0) || (accel < 0 && move > 0))
        {
            brakeCount++;
            if (brakeCount >= BrakeTime)
            {
                brakeCount = 0;
                down++;
                brake += UpBrake / down;
            }
        }
        else if (brake != 0)
        {
            accel = 0;
            brake = 0;
            brakeCount = 0;
        }
    }

    void ZeroAccel()
    {
        accel = 0;
        accelCount = 0;
    }

    void Jump()
    {
        var jumpPower = isBoosting ? BoostJumpPower : JumpPower;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpPower));
            }
        }
    }

    void MultiJump()
    {
        var jumpPower = isBoosting ? BoostJumpPower : JumpPower;
        if (FirstPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                if (isGround)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpPower));
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isGround)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpPower));
                }
            }
        }


    }

    void FixedUpdate() // Jumpのための接地判定
    {
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - ((gameObject.GetComponent<BoxCollider2D>().size.y / 2 + GroundPlusY) * transform.localScale.y));
        Vector2 groundArea = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x / 2 * 0.49f, 0.05f);

        Debug.DrawLine(groundCheck + groundArea, groundCheck + new Vector2(-groundArea.x, groundArea.y)); // 調整用
        Debug.DrawLine(groundCheck - groundArea, groundCheck - new Vector2(-groundArea.x, groundArea.y));

        isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, Ground);
    }


    public void GetItem(GameObject item, int num)
    {
        if (num != pun.myNum)
            return;
        havingItem = item;
        havingItemUI.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        havingItemUI.GetComponent<Image>().enabled = true;
    }

    public void GetGoal(float time)
    {
        Debug.Log("Goaled");
        IsGoaled = true;

        //ゴールした時の処理（現状は動きを完全に停止する）
        var col = gameObject.GetComponent<BoxCollider2D>();
        var rigid = gameObject.GetComponent<Rigidbody2D>();
        var script = gameObject.GetComponent<CharacterMove>();
        rigid.isKinematic = true;
        col.enabled = false;
        gameObject.isStatic = true;
        script.enabled = false;
    }

    public bool IsGoaled
    {
        get; private set;
    }

    void UseItem()
    {
        if (havingItem == null)
            return;

        var script = havingItem.GetComponent<ItemScript>();
        if (script.Type == "RockShooter")
        {
            //ここで岩妨害を発動させる
            Debug.Log("Used RockShooter!");
            (GameObject.Find("PunManager")).GetComponent<PunScript>().fallStone(pd.PlayerNumber);
        }
        else if (script.Type == "Trance")
        {
            // スタミナゲージが一定時間減らなくなる
            GetComponent<StaminaScript>().GetTrance();
        }
        else if (script.Type == "WindShooter")
        {
            //ここで風妨害を発動させる。
            (GameObject.Find("PunManager")).GetComponent<PunScript>().windBlow(pd.PlayerNumber);
        }

        havingItemUI.GetComponent<Image>().enabled = false;
        Destroy(havingItem);
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "DamageObject" && !isDamage) {
            StartCoroutine ("Damage", col.gameObject.tag);
        } else if (col.gameObject.tag == "FallEnd") {
            StartCoroutine ("Damage",col.gameObject.tag);
        }
	}
	
	IEnumerator Damage(string tag)
	{
        isDamage = !isDamage;

		if (tag == "DamageObject") 
		{
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.right.x * backwardForce.x, transform.up.y * backwardForce.y);
		}
        ZeroAccel ();

        for (int i = 0; i < 8; i++) 
		{
            gameObject.GetComponent<SpriteRenderer> ().enabled = !gameObject.GetComponent<SpriteRenderer> ().enabled;
            yield return new WaitForSeconds (0.1f);
		}

		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		
		isDamage = !isDamage;
	}

    public bool GetIsGround()
    {
        return isGround;
    }

    public float GetCameraPlace()
    {
        if (accel > 0)
        {
            return Mathf.Abs(accel) - brake;
        }
        else
        {
            return 0;
        }
    }
}
