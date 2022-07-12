using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wakamol : MonoBehaviour
{
    public GameObject[] moles;
    public GameObject[] correctMoles;
    public GameObject[] wrongMoles;
    public Image[] choices;
    public Image[] correctChoices;
    public Image[] wrongChoices;
    public Sprite[] sprites;
    public Image blackOverlay;
    public GameObject questionPanel;
    public TMP_Text score;
    public Image innerTimer;
    int scoreCounter;
    [SerializeField]
    float timeUi;
    public Sprite[] correctSprites;
    public Sprite[] wrongSprites;
    public int intervalMin = 0;
    public int intervalMax = 1;
    // Start is called before the first frame update
    void Start()
    {
        questionPanel.gameObject.SetActive(true);
        blackOverlay.gameObject.SetActive(true);

        // Invoke("RandomMoleSpawn", Random.Range(intervalMin, intervalMax));
        // Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));
        // setChoices();
        // RandomPos();
        // setSprites();
    }

    void Update()
    {
        score.text = scoreCounter.ToString();
        innerTimer.fillAmount = timeUi;
    }
    void setSprites() {
        for(int i = 0; i < correctChoices.Length; i++) {
            correctChoices[i].sprite = correctSprites[Random.Range(0, correctChoices.Length)];
        }
        for(int i = 0; i < wrongChoices.Length; i++) {
            wrongChoices[i].sprite = wrongSprites[Random.Range(0, wrongChoices.Length)];
        }
    }
    void RandomMoleSpawn() {
        int randomIndex = Random.Range(0, moles.Length);
        moles[randomIndex].transform.SetSiblingIndex(Random.Range(0, moles.Length));
        moles[randomIndex].gameObject.SetActive(true);

        Invoke("RandomMoleSpawn", Random.Range(intervalMin, intervalMax));
    }
    void RandomPos() {
        for(int i = 0; i < choices.Length; i++) {
            choices[i].transform.SetSiblingIndex(Random.Range(0, choices.Length));

            // choices[i].transform.position = choices[Random.Range(0, choices.Length)].transform.position;

            // Vector3 tempPosition = object1.transform.position;
            // object1.transform.position = object2.transform.position;
            // object2.transform.position = tempPosition;
        }
    }
    void RandomMoleDespawn() {
        int randomIndex = Random.Range(0, moles.Length);
        moles[randomIndex].gameObject.SetActive(false);
         
        Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));
    }
    public void MoleClicked(int index) {
        // Debug.Log(choices[index].transform.GetChild(0).GetComponent<TMP_Text>().text);
        // string num = choices[index].transform.GetChild(0).GetComponent<TMP_Text>().text;
        // int number;

        // int.TryParse(num, out number);
        scoreCounter++;
        //setChoices();
        moles[index].gameObject.SetActive(false);
    }

    public void MoleClickedWrong(int index) {
        // Debug.Log(choices[index].transform.GetChild(0).GetComponent<TMP_Text>().text);
        // string num = choices[index].transform.GetChild(0).GetComponent<TMP_Text>().text;
        // int number;

        // int.TryParse(num, out number);
        timeUi -= 5;
        scoreCounter--;
        //setChoices();
        moles[index].gameObject.SetActive(false);
    }

    public void StartGame() {
        questionPanel.gameObject.SetActive(false);
        blackOverlay.gameObject.SetActive(false);

        Invoke("RandomMoleSpawn", Random.Range(intervalMin, intervalMax));
        Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));
        //setChoices();
        RandomPos();
        setSprites();

        BeginTimer(60f);
    }

    void BeginTimer(float time) {
        timeUi = time;
        timeUi -= 1;
    }
}

/*
    Correct moles
    Wrong moles

    Correct sprite => correct mole
    Wrong sprite => wrong mole

    Randomize items in array 
    Randomize mole positions

    time limit
    levels

*/