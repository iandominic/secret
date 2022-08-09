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
    public void Interactable() { 
        // btn.interactable = true;
        Debug.Log("zxczc");
        wakamol.CheckIfVisible();
    }
     public void Interactable2() {
        // btn2.interactable = true;
        wakamol.CheckIfVisible();
    }
     public void Interactable3() {
        // btn3.interactable = true;
        wakamol.CheckIfVisible();
    }
     public void Interactable4() {
        // btn4.interactable = true;
        wakamol.CheckIfVisibleWrong();
    }
     public void Interactable5() {
        // btn5.interactable = true;
        wakamol.CheckIfVisibleWrong();
    }
     public void Interactable6() {
        // btn6.interactable = true;
        wakamol.CheckIfVisibleWrong();
    }
    //////////////////////////////////////////
    public void test() { 
        // wakamol.CheckIfDone();
         btn.interactable = true;
         wakamol.CheckIfDone();
    }
     public void test2() {
        // wakamol.CheckIfDone();
         btn2.interactable = true;
        
        wakamol.CheckIfDone();
    }
     public void test3() {
    //    wakamol.CheckIfDone();
     btn3.interactable = true;
     wakamol.CheckIfDone();
        
    }
     public void test4() {
        // wakamol.CheckIfDone();
         btn4.interactable = true;
         wakamol.CheckIfDone();
        
    }
     public void test5() {
        // wakamol.CheckIfDone();
         btn5.interactable = true;
         wakamol.CheckIfDone();
        
    }
     public void test6() {
        // wakamol.CheckIfDone();
         btn6.interactable = true;
         wakamol.CheckIfDone();
        
    }
}
