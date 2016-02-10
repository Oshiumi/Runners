using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CautionManager : MonoBehaviour {

    public Image Caution;

    public float FlashInterval = 1.0f;
    public int FlashCount = 5;

    bool FlashFlag;

	// Use this for initialization
	void Start () {
        Caution.enabled = false;

        FlashFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void CautionEnter()
    {
        if (!FlashFlag)
            return;
        Caution.enabled = true;

        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        int count = 0;

        FlashFlag = true;

        while (count < FlashCount)
        {

            yield return new WaitForSeconds(FlashInterval);

            Caution.enabled = !Caution.enabled;

            count++;
        }

        if (Caution.enabled)
        {
            Caution.enabled = false;
        }

        FlashFlag = false;
    }
}
