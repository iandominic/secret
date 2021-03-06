using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wakamol : MonoBehaviour
{
    public List<Mole> moleObject = new List<Mole>();
    public List<MoleSagot> moleObjectSagot = new List<MoleSagot>();
    public GameObject[] moles;
    public GameObject[] correctMoles;
    public GameObject[] wrongMoles;
    public Animator[] moleAnim;
    public Image[] choices;
    public Image[] correctChoices;
    public Image[] wrongChoices;
    public Sprite[] sprites;
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
    void Start()
    {
        moleObjectSagot.Add(new MoleSagot(moles[0], sprites[0], true, false));
        moleObjectSagot.Add(new MoleSagot(moles[1], sprites[1], true, false));
        moleObjectSagot.Add(new MoleSagot(moles[2], sprites[2], true, false));
        moleObjectSagot.Add(new MoleSagot(moles[3], sprites[3], false, false));
        moleObjectSagot.Add(new MoleSagot(moles[4], sprites[4], false, false));
        moleObjectSagot.Add(new MoleSagot(moles[5], sprites[5], false, false));

        Debug.Log(moleObjectSagot.Count);
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
    IEnumerator WaitSpawn() {
        yield return new WaitForSeconds(1);
        RandomMoleDespawn();
    }
    IEnumerator WaitAnimation() {
        yield return new WaitForSeconds(1);
        CheckIfVisible();
    }
    void CheckIfVisible() {
        for(int i = 0; i < moleObjectSagot.Count; i++) {
            if(moleObjectSagot[i].IsVisible == false) {
                int randomIndex = Random.Range(0, moleObjectSagot.Count);
                choices[i].sprite = moleObjectSagot[randomIndex].Obj;
                moles[i].transform.GetChild(1).GetComponent<TMP_Text>().text = moleObjectSagot[randomIndex].IsRight.ToString();
                moles[i].transform.GetChild(1).name = moleObjectSagot[randomIndex].IsRight.ToString();
            }
            else {
                continue;
            }
        }
    }
    void setSprites() {
        int index = Random.Range(0, moleObjectSagot.Count);
        for(int i = 0; i < moleObjectSagot.Count; i++) {
            choices[i].sprite = moleObjectSagot[index].Obj;
        }
    }
    void RandomMoleSpawn() {
        int randomIndex = Random.Range(0, moles.Length);
        moleAnim[randomIndex].gameObject.SetActive(true);
        moleAnim[randomIndex].SetBool("Spawn", true);
        moleAnim[randomIndex].SetBool("Despawn", false);

        moleObjectSagot[randomIndex].IsVisible = true;

        Invoke("RandomMoleSpawn", Random.Range(intervalMin, intervalMax));
    }
    void RandomMoleDespawn() {
        int randomIndex = Random.Range(0, moles.Length);
        moleAnim[randomIndex].SetBool("Spawn", false);
        moleAnim[randomIndex].SetBool("Despawn", true);

        moleObjectSagot[randomIndex].IsVisible = false;

        StartCoroutine(WaitAnimation());

        Invoke("RandomMoleDespawn", Random.Range(intervalMin, intervalMax));
    }
    void DespawnAllMoles() {
        for(int i = 0; i < moles.Length; i++) {
            moleAnim[i].SetBool("Despawn", true);
            moleAnim[i].SetBool("Spawn", false);
        }

        CancelInvoke("RandomMoleSpawn");
        CancelInvoke("RandomMoleDespawn");
    }
    public void MoleClicked(int index) {
        if(moles[index].transform.GetChild(1).GetComponent<TMP_Text>().text == "True") {
            currentScore++;
            Debug.Log(currentScore);
            if(currentScore == requiredScore) {
                    Debug.Log("Completed");
                    timerUi.gameObject.SetActive(false);
                    congratsPanel.gameObject.SetActive(true);

                    isDone = true;

                    DespawnAllMoles();
            }
        } else {
            currentScore--;
            Debug.Log(currentScore);
        }

        moleAnim[index].SetBool("Despawn", true);
        moleAnim[index].SetBool("Spawn", false);
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
    IEnumerator Set() {
        yield return new WaitForSeconds(1.2f);

        ready.gameObject.SetActive(false);
        set.gameObject.SetActive(true);
        StartCoroutine(Go());
    }
    IEnumerator Go() {
        yield return new WaitForSeconds(1.6f);

        setSprites();
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
