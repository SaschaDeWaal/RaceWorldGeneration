using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase

[Condition("Car/NoTargetSelected")]
public class NoTargetSelected : ConditionBase {

    [InParam("index")]
    public int index;

    public override bool Check() {
        return (index == 0);
    }
}
