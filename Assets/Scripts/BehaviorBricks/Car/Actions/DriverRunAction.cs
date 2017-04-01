using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("Car/DriverRunAction")]
public class DriverRunAction : GOAction {

    private SpecialCarry specialCarry;

    public override void OnStart() {
        specialCarry = gameObject.GetComponent<SpecialCarry>();
        base.OnStart();
    }

    public override TaskStatus OnUpdate() {


        if(specialCarry.Carry != SpecialActions.none) {
            RunSpecialAction.instance.Run(gameObject, specialCarry.Carry);
            specialCarry.Clear();

            return TaskStatus.COMPLETED;
        } else {
            return TaskStatus.FAILED;
        }

    }
}
