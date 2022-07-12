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
    public Sprite[] correctSprites;
    public Sprite[] wrongSprites;

    public Image blackOverlay;
    public Image innerTimer;
    public Image ready;
    public Image set;
    public Image go;

    public GameObject questionPanel;
    public TMP_Text score;
    public int scoreCounter;

    [SerializeField]
    public float timeRemaining;
    public float maxTime;

    public bool timerIsRunning = false;

    public float intervalMin = 0.4f;
    public float intervalMax = 1f;

    IEnumerator shuffleCoroutine;

    void Start()
    {
        questionPanel.gameObject.SetActive(true);
        blackOverlay.gameObject.SetActive(true);
        timeRemaining = maxTime;

        // Invoke("RandomMoleSpawn", Random.Range(intervalMin, intervalMax));
        // Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));
        // setChoices();
        // RandomPos();
        // setSprites();
    }
    void Update()
    {
        score.text = scoreCounter.ToString();

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                innerTimer.fillAmount = timeRemaining / maxTime;
                Debug.Log("Time: " + timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
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
        scoreCounter--;
        //setChoices();
        moles[index].gameObject.SetActive(false);
    }
    public void StartGame() {
        questionPanel.gameObject.SetActive(false);
        blackOverlay.gameObject.SetActive(false);
        //setChoices();
        // RandomPos();
        ready.gameObject.SetActive(true);
        StartCoroutine(Set());
    }
    void BeginTimer(float time) {
        maxTime = time;
        timeRemaining = maxTime;
    }
    public void StartShuffle() // call this on button click
    {
        if (shuffleCoroutine != null) return;

        shuffleCoroutine = DoShuffle();
        StartCoroutine(shuffleCoroutine);
    }
    IEnumerator DoShuffle()
    {
        List<Vector3> startPos = new List<Vector3>();
        List<Vector3> endPos = new List<Vector3>();
        foreach (Image mole in choices)
        {
            startPos.Add(mole.transform.position);
            endPos.Add(mole.transform.position);
        }

        // shuffle endPos
        for (int i = 0 ; i < endPos.Count ; i++) {
            Vector3 temp = endPos[i];
            int swapIndex = Random.Range(i, endPos.Count);
            endPos[i] = endPos[swapIndex];
            endPos[swapIndex] = temp;
        }

        float elapsedTime = 0f;

        while (elapsedTime < 1)
        {
            // wait for next frame
            yield return null;

            // move each letter
            elapsedTime  = Mathf.Min(1, elapsedTime+Time.deltaTime);
            float t = elapsedTime / 1;

            for (int i = 0 ; i < startPos.Count ; i++) {
                choices[i].transform.position = Vector3.Lerp(startPos[i],endPos[i],t);
            }
        }  

        // allow shuffling to occur again
        shuffleCoroutine = null;
    }
    IEnumerator Set() {
        yield return new WaitForSeconds(2f);

        ready.gameObject.SetActive(false);
        set.gameObject.SetActive(true);
        StartCoroutine(Go());
    }
    IEnumerator Go() {
        yield return new WaitForSeconds(3f);

        setSprites();
        StartShuffle();
        BeginTimer(30f);
        
        timerIsRunning = true;

        Invoke("RandomMoleSpawn", Random.Range(intervalMin, intervalMax));
        Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));

        set.gameObject.SetActive(false);
        go.gameObject.SetActive(true);

        StartCoroutine(Close());
    }
    IEnumerator Close() {
        yield return new WaitForSeconds(0.5f);

         go.gameObject.SetActive(false);
    }
    void CloseReady() {
        ready.gameObject.SetActive(false);
        set.gameObject.SetActive(false);
        go.gameObject.SetActive(false);
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