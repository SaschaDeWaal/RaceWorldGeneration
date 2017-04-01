using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;
using BBUnity.Conditions;

[Condition("Car/SpecialActionAvailable")]
public class SpecialActionAvailable : GOCondition {

    public override bool Check() {
        return (gameObject.GetComponent<SpecialCarry>().Carry != SpecialActions.none);
    }
}
