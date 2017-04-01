using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;
using BBUnity.Conditions;

[Condition("Car/IsBehingACar")]
public class IsBehingACar : GOCondition {

    private const float MIN_DISTANCE = 20;

    public override bool Check() {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");

        foreach(GameObject car in cars) {
            if (car != gameObject && Vector3.Distance(car.transform.position, gameObject.transform.position) < MIN_DISTANCE) {
                Vector3 dir = gameObject.transform.InverseTransformPoint(car.transform.position);

                if(dir.z > 0)
                    return true;
            }
        }

        return false;
    }
}
