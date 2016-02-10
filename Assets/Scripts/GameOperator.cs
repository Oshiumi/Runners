using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOperator : MonoBehaviour
{

    List<GameObject> playerInstanceList;
    public int PlayerNum;
    public GameObject goal;
    public GameObject[] Starts;
    int goalCount;
    ulong count;

    // Use this for initialization
    void Start()
    {
        goal.GetComponent<GoalScript>().PlayersNum = PlayerNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (goal != null && goal.GetComponent<GoalScript>().IsRearchedAllPlayers)
        {
            ++goalCount;
        }

        if (goalCount == 240)
        {
            Application.LoadLevel("Result");
        }

        ++count;
    }
}
