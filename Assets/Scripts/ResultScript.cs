using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{

    public int count;
    public Text notifyEnd;
    // Use this for initialization
    void Start()
    {
        count = 0;
        notifyEnd.GetComponent<Text>().enabled = false;

        //実用時は呼び出さないこと。
        ResultData.LoadSample();

        //プロパティをセット
        for (int i = 0; i < ResultData.PlayerNum; ++i)
        {
            string num = (i + 1).ToString();

            var player = GameObject.Find("/Results/unit" + num + "/player" + num);
            player.GetComponent<Text>().text = ResultData.PlayerNumbers[i].ToString() + "P";

            GameObject.Find("/Results/unit" + num + "/image" + num).GetComponent<Image>().color = ResultData.Colors[i];

            var timeNum = ResultData.Times[i];
            var minTime = timeNum.Minute;
            var secTime = timeNum.Second;
            var misecTime = timeNum.Millisecond;
            GameObject.Find("/Results/unit" + num + "/time" + num).GetComponent<Text>().text = minTime.ToString("00") + ":" + secTime.ToString("00") + ":" + misecTime.ToString("00");
        }

        for (int i = ResultData.PlayerNum; i < 4; ++i)
        {
            string num = (i + 1).ToString();
            var player = GameObject.Find("/Results/unit" + num);
            player.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (count >= 450)
        {
            notifyEnd.GetComponent<Text>().enabled = count % 120 > 60;
            if (Input.GetKey(KeyCode.Space))
            {
                Application.LoadLevel("Title");
            }
        }
        else if (count == 10)
        {
            //gameObject.GetComponent<AudioSource>().Play();
        }

        ++count;
    }
}
