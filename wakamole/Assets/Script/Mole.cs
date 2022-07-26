using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole 
{
    GameObject obj;
    bool isVisible;
    public Mole(GameObject obj, bool isVisible) {
        this.obj = obj;
        this.isVisible = isVisible;
    }
    public GameObject Obj {
        get { return obj; }
        set { obj = value; }
    }
    public bool IsVisible {
        get { return isVisible; }
        set { isVisible = value; }
    }
}
