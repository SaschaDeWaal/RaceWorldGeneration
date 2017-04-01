using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpecialCarry : MonoBehaviour {

    private SpecialActions carry = SpecialActions.none;

    public SpecialActions Carry {
        get { return carry; }
    }

    public void Clear() {
        carry = SpecialActions.none;
    }

    public void AddSpecial(SpecialActions special) {
        carry = special;
    }
}
