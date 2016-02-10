using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{

    public class CharacterSpec
    {
        public int Speed;
        public int Boost;
        public int Accel;
        public Color Color;
        public string Name;

        public CharacterSpec()
        {
            Speed = Boost = Accel;
            Name = "";
            Color = new Color();
        }

        public CharacterSpec(int speed, int boost, int accel, string name, Color color)
        {
            Speed = speed;
            Boost = boost;
            Accel = accel;
            Name = name;
            Color = color;
        }
    };

    int selectingCharacter;
    int CharacterNum = 4;
    public Text notifyText;
    public Text selectNotify;
    public Text characterNameText;
    public GameObject displayingUnitychan;

    public Slider SpeedBar;
    public Slider BoostBar;
    public Slider AccelBar;

    bool selected;
    float time;
    CharacterSpec[] characterSpecs = new CharacterSpec[4];
    public static CharacterSpec SelectedCharacter;

    // Use this for initialization
    void Start()
    {
        selectingCharacter = 0;
        selected = false;
        time = 0;

        characterSpecs[0] = new CharacterSpec(3, 4, 3, "Balanced Unitychan", new Color(1, 1, 1));
        characterSpecs[1] = new CharacterSpec(2, 3, 5, "Power Unitychan", new Color(0.9f, 0.4f, 0.4f));
        characterSpecs[2] = new CharacterSpec(5, 2, 3, "Sonic Unitychan", new Color(0.4f, 0.4f, 0.9f));
        characterSpecs[3] = new CharacterSpec(3, 5, 2, "Endurant Unitychan", new Color(0.4f, 0.9f, 0.4f));

        ToggleDisplay();
    }

    void ToggleDisplay()
    {
        displayingUnitychan.GetComponent<SpriteRenderer>().color = characterSpecs[selectingCharacter].Color;
        AccelBar.value = characterSpecs[selectingCharacter].Accel;
        BoostBar.value = characterSpecs[selectingCharacter].Boost;
        SpeedBar.value = characterSpecs[selectingCharacter].Speed;
        characterNameText.text = characterSpecs[selectingCharacter].Name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectingCharacter == 0) selectingCharacter = CharacterNum - 1;
            else selectingCharacter--;

            ToggleDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectingCharacter == CharacterNum - 1) selectingCharacter = 0;
            else selectingCharacter++;

            ToggleDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //キャラクター決定
            selected = true;
            selectNotify.GetComponent<Text>().enabled = false;
            SelectedCharacter = characterSpecs[selectingCharacter];
        }

        if (selected)
        {
            time += Time.deltaTime;
            if (time > 0.4f)
            {
                time = 0;
                notifyText.GetComponent<Text>().enabled = !notifyText.GetComponent<Text>().enabled;
            }

            Application.LoadLevel("LevelDesignTest");
        }
    }
}
