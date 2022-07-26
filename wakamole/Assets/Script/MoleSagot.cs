using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSagot
{
    GameObject mole;
    Sprite obj;
    bool isRight;
    bool isVisible;
    public MoleSagot(GameObject mole, Sprite obj, bool isRight, bool isVisible) {
        this.mole = mole;
        this.obj = obj;
        this.isRight = isRight;
        this.isVisible = isVisible;
    }
    public GameObject Mole {
        get { return mole; }
        set { mole = value; }
    }
    public Sprite Obj {
        get { return obj; }
        set { obj = value; }
    }
    public bool IsRight {
        get { return isRight; }
        set { isRight = value; }
    }
    public bool IsVisible {
        get { return isVisible; }
        set { isVisible = value; }
    }
}
