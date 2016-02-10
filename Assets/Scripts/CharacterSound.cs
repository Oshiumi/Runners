using UnityEngine;
using System.Collections;

public class CharacterSound : MonoBehaviour
{
    
    // 効果音再生用
    public AudioSource objectsource;
    public AudioClip se_jump;
    public AudioClip se_landing;
    public AudioClip se_damage;
    public AudioClip se_boost;
    public AudioClip se_col;
    public AudioClip se_get;
    public AudioClip se_use;
    public AudioClip se_dash;
    
    // charactermoveコンポーネント
    public CharacterMove charamove;
    
    // itemboxコンポーネント
    public ItemBox itembox;
    
    // 設置判定用
    private bool isGround;
    
    // アイテム用
    private bool havingItem;
    
    // ブースト用
    private float boostTimer;
    private bool isBoosting;
    
    // Use this for initialization
    void Start()
    {
        charamove = gameObject.GetComponent<CharacterMove>();
        objectsource = gameObject.GetComponent<AudioSource>();
        boostTimer = 0f;
        isBoosting = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (charamove.GetIsGround())
            {
                objectsource.clip = se_jump;
                objectsource.Play();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!havingItem)
            {
                return;
            }
            objectsource.clip = se_use;
            objectsource.Play();
            havingItem = false;
        }
        
        if (Input.GetKey(KeyCode.S) & isBoosting == false)
        {
            BoostSound();
        }
        
    }
    
    // プレイヤー衝突周りの音再生用
    public void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "DamageObject" | col.gameObject.tag == "FallEnd")
        {
            objectsource.clip = se_damage;
            objectsource.Play();
        }
        else if (col.gameObject.tag == "Ground" & !isGround)
        {
            objectsource.clip = se_landing;
            objectsource.Play();
            isGround = charamove.GetIsGround();
        }
        else if (col.gameObject.tag == "ItemBox")
        {
            objectsource.clip = se_get;
            objectsource.Play();
            havingItem = true;
        }
        else
        {
            objectsource.clip = se_col;
            objectsource.Play();
        }
        
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = charamove.GetIsGround();
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DashBoard")
        {
            objectsource.clip = se_dash;
            objectsource.Play();
        }
    }
    
    void BoostSound()
    {
        StartCoroutine("_BoostSound");
    }
    
    IEnumerator _BoostSound()
    {
        isBoosting = true;
        
        objectsource.clip = se_boost;
        objectsource.Play();
        yield return new WaitForSeconds(0.5f);
        
        isBoosting = false;
    }
    
}
