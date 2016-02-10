using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{

    public int Stamina;

    public int MAX_STAMINA = 1000;
    public Slider staminaBarUI;
    public bool IsBoosting { get; private set; }
    public int Increment;
    public int Decrement;
    private int tranceCount;
    public int TranceTime;
    public bool IsTrance { get; private set; }
    private ParticleSystem[] Aura = new ParticleSystem[2];
    private bool isPlaying = false;

    // Use this for initialization
    void Start()
    {
        staminaBarUI = GameObject.Find("/UI/StatusUI/Status/StaminaBar").GetComponent<Slider>();
        Aura [0] = this.gameObject.transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
        Aura [1] = this.gameObject.transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
        Stamina = 0;
        IsBoosting = false;
        Increment = 1;
        Decrement = 2;
        gameObject.name = "Player" + (GameObject.Find ("PunManager")).GetComponent<PunScript> ().countPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        IsBoosting = false;

        if (IsTrance)
        {
            IsBoosting = Input.GetKey(KeyCode.S);
            ++tranceCount;
            if (tranceCount == TranceTime)
            {
                IsTrance = false;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Stamina > 0)
            {
                IsBoosting = true;

                if (!IsTrance)
                {
                    Stamina = Mathf.Max(Stamina - Decrement, 0);
                }
            }
        }
        else
        {
            Stamina = Mathf.Min(Stamina + Increment, MAX_STAMINA);
        }


        if (staminaBarUI != null)
        {
            staminaBarUI.value = Stamina;
        }

        if (IsBoosting) {
            (GameObject.Find("PunManager")).GetComponent<PunScript>().boostAura(this.GetComponent<PlayerData>().PlayerNumber);
        }
        else
        {
            if(Aura[0].isPlaying){
                for (int i = 0; i < Aura.Length; ++i)
                {
                    Aura[i].Stop();
                }
            }
        }
    }

    public void StartAura(){
        for (int i = 0; i < Aura.Length; ++i)
        {
            Aura[i].Play();
        }
    }

    public void Ruin()
    {
        Stamina = 0;
        staminaBarUI.value = Stamina;
    }

    public void GetTrance()
    {
        tranceCount = 0;
        IsTrance = true;
    }

    public void ChangeMax(int max)
    {
        MAX_STAMINA = max;
        staminaBarUI.maxValue = MAX_STAMINA;
    }
}