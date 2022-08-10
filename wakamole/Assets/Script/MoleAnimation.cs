using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoleAnimation : MonoBehaviour
{
    [SerializeField]private Wakamol wakamol;
    [SerializeField]private Button btn;
    [SerializeField]private Button btn2;
    [SerializeField]private Button btn3;
    [SerializeField]private Button btn4;
    [SerializeField]private Button btn5;
    [SerializeField]private Button btn6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAnim() {
        yield return new WaitForSeconds(2);
    }

    public void Interactable() { 
        wakamol.moles[0].transform.GetChild(0).gameObject.SetActive(false);
        wakamol.moleObjectSagot[0].IsVisible = false;
    }
     public void Interactable2() {
        wakamol.moles[1].transform.GetChild(0).gameObject.SetActive(false);
        wakamol.moleObjectSagot[1].IsVisible = false;
    }
     public void Interactable3() {
        wakamol.moles[2].transform.GetChild(0).gameObject.SetActive(false);
        wakamol.moleObjectSagot[2].IsVisible = false;
    }
     public void Interactable4() {
        wakamol.moles[3].transform.GetChild(0).gameObject.SetActive(false);
        wakamol.moleObjectSagot[3].IsVisible = false;
    }
     public void Interactable5() {
        wakamol.moles[4].transform.GetChild(0).gameObject.SetActive(false);
        wakamol.moleObjectSagot[4].IsVisible = false;
    }
     public void Interactable6() {
        wakamol.moles[5].transform.GetChild(0).gameObject.SetActive(false);
        wakamol.moleObjectSagot[5].IsVisible = false;
    }
    //////////////////////////////////////////
    public void test() { 
        btn.interactable = true;
        wakamol.CheckIfDone();
        wakamol.moles[0].transform.GetChild(0).gameObject.SetActive(true);
    }
     public void test2() {
        btn2.interactable = true;
        wakamol.CheckIfDone();
        wakamol.moles[1].transform.GetChild(0).gameObject.SetActive(true);
    }
     public void test3() {
        btn3.interactable = true;
        wakamol.CheckIfDone();
        wakamol.moles[2].transform.GetChild(0).gameObject.SetActive(true);   
    }
     public void test4() {
        btn4.interactable = true;
        wakamol.CheckIfDone();
        wakamol.moles[3].transform.GetChild(0).gameObject.SetActive(true); 
    }
     public void test5() {
        btn5.interactable = true;
        wakamol.CheckIfDone();
        wakamol.moles[4].transform.GetChild(0).gameObject.SetActive(true);
    }
     public void test6() {
        btn6.interactable = true;
        wakamol.CheckIfDone();
        wakamol.moles[5].transform.GetChild(0).gameObject.SetActive(true);
    }
}
