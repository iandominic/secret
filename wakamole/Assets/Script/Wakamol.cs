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
    public GameObject congratsPanel;
    public GameObject timesUpPanel;
    public GameObject timerUi;
    public TMP_Text score;
    public int currentScore;
    public int requiredScore;

    [SerializeField]
    public float timeRemaining;
    public float maxTime;

    public bool timerIsRunning = false;
    public bool isDone = true;

    public float intervalMin = 0.4f;
    public float intervalMax = 1f;

    IEnumerator shuffleCoroutine;

    void Start()
    {
        questionPanel.gameObject.SetActive(true);
        blackOverlay.gameObject.SetActive(true);
        timeRemaining = maxTime;
        requiredScore = 3;
    }
    void Update()
    {
        score.text = currentScore.ToString() + "/" + requiredScore.ToString();

        if (timerIsRunning)
        {
            if (timeRemaining > 0 && !isDone)
            {
                timeRemaining -= Time.deltaTime;
                innerTimer.fillAmount = timeRemaining / maxTime;
            }
            else if (timeRemaining <= 0 && !isDone)
            {
                Debug.Log("Time has run out!");
                timesUpPanel.gameObject.SetActive(true);
                timerUi.gameObject.SetActive(false);

                DespawnAllMoles();

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
        } 
    }
    void RandomMoleDespawn() {
        int randomIndex = Random.Range(0, moles.Length);
        moles[randomIndex].gameObject.SetActive(false);
         
        Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));
    }
    void DespawnAllMoles() {
        for(int i = 0; i < moles.Length; i++) {
            moles[i].gameObject.SetActive(false);
        }

        CancelInvoke("RandomMoleSpawn");
        CancelInvoke("RandomMoleDespawn");
    }
    public void MoleClicked(int index) {
        currentScore++;

        if(currentScore == requiredScore) {
            Debug.Log("Completed");
            timerUi.gameObject.SetActive(false);
            congratsPanel.gameObject.SetActive(true);

            isDone = true;

            DespawnAllMoles();
        }
        
        moles[index].gameObject.SetActive(false);
    }
    public void MoleClickedWrong(int index) {
        timeRemaining -= 5f;

        moles[index].gameObject.SetActive(false);
    }
    public void StartGame() {
        questionPanel.gameObject.SetActive(false);
        blackOverlay.gameObject.SetActive(false);
        ready.gameObject.SetActive(true);

        StartCoroutine(Set());
    }
    public void NextLevel() {
        requiredScore += 1;
        currentScore = 0;

        StartCoroutine(Set());

        ready.gameObject.SetActive(true);
        congratsPanel.gameObject.SetActive(false);
    }
    public void RetryLevel() {
        requiredScore = requiredScore;
        currentScore = 0;

        StartCoroutine(Set());

        ready.gameObject.SetActive(true);
        timesUpPanel.gameObject.SetActive(false);

        Debug.Log(requiredScore);
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
                choices[i].transform.position = Vector3.Lerp(startPos[i], endPos[i], t);
            }
        }  

        // allow shuffling to occur again
        shuffleCoroutine = null;
    }
    IEnumerator Set() {
        yield return new WaitForSeconds(1.2f);

        ready.gameObject.SetActive(false);
        set.gameObject.SetActive(true);
        StartCoroutine(Go());
    }
    IEnumerator Go() {
        yield return new WaitForSeconds(1.6f);

        setSprites();
        StartShuffle();
        BeginTimer(20f);
        isDone = false;
        timerUi.gameObject.SetActive(true);
        
        timerIsRunning = true;

        Invoke("RandomMoleSpawn", Random.Range(intervalMin, intervalMax));
        Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));

        set.gameObject.SetActive(false);
        go.gameObject.SetActive(true);

        Debug.Log("Required score: " + requiredScore);

        StartCoroutine(Close());
    }
    IEnumerator Close() {
        yield return new WaitForSeconds(0.5f);

         go.gameObject.SetActive(false);
    }
}
