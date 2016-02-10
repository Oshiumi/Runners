using UnityEngine;
using System.Collections;

public class StageGimic : MonoBehaviour
{

    // 移動一回あたりの単位
    public float MoveDistance = 0f;

    // 移動回数、MoveDistanceを何回行うか
    public float MoveCount = 0f;

    // オートギミックの動作の間隔
    public float AutoInterval = 0f;

    // カウンター
    float i = 0f;
    float j = 0f;

    // ギミックの真偽、falseで下降している位置
    public bool isUp = false;

    // ギミックが移動中かどうか
    public bool isMove = false;

    // ギミックがオートで動くものかどうか
    public bool isAuto = false;

    // 効果音再生用
    public AudioSource SE_source;
    public AudioClip SE_clip;

    // Use this for initialization
    void Start()
    {
        SE_source.clip = SE_clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAuto & i % AutoInterval == 0)
        {
            MoveGimic();
            isUp = !isUp;
        }
        i++;
    }

    // ギミック移動用
    public void MoveGimic()
    {
        StartCoroutine("_MoveGimic");
        SE_source.Play();
    }

    // ギミック移動用コルーチン
    IEnumerator _MoveGimic()
    {
        isMove = true;

        if (isMove)
        {

            if (isUp)
            {
                while (j < MoveCount)
                {
                    transform.Translate(0, MoveDistance, 0);
                    j++;
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                while (j < MoveCount)
                {
                    transform.Translate(0, -MoveDistance, 0);
                    j++;
                    yield return new WaitForSeconds(0.01f);
                }
            }

            j = 0;

            isMove = false;
        }
    }

}
