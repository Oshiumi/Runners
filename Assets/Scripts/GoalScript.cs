using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour
{

    public int PlayersNum { get; set; }
    int rearchedPlayerNum;
    float timer;
    float startTime;

    // SoundEffect
    public AudioSource objectsource;
    public AudioClip se_goal;

    // Use this for initialization
    void Start()
    {
        rearchedPlayerNum = 0;
        IsRearchedAllPlayers = false;
        timer = 0;
        objectsource = gameObject.GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void OnStartRace()
    {
        startTime = timer;
    }

    public bool IsRearchedAllPlayers { get; private set; }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            objectsource.clip = se_goal;
            objectsource.Play();

            var player = col.gameObject.GetComponent<CharacterMove>();

            //とりあえず時間0でゴール
            player.GetGoal(0);

            rearchedPlayerNum++;

            if (rearchedPlayerNum == PlayersNum)
            {
                IsRearchedAllPlayers = true;
            }
        }
    }
}
