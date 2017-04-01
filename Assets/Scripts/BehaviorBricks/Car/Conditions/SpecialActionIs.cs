using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;
using BBUnity.Conditions;

[Condition("Car/SpecialActionIs")]
public class SpecialActionIs : GOCondition {

    [InParam("specialAction")]
    public SpecialActions specialAction;

    public override bool Check() {
        return (gameObject.GetComponent<SpecialCarry>().Carry == specialAction);
    }
}
