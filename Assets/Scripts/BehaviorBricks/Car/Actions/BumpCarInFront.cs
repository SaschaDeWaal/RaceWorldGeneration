using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("Car/BumpCarInFront")]
public class BumpCarInFront : GOAction {

    private const float MIN_DISTANCE = 2f;
    private const float MULIPLY = 2;

    [InParam("speed")]
    private float speed = 15;

    private GameObject otherCar = null;
    private float timer = 0f;

    public override void OnStart() {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");

        foreach(GameObject car in cars) {
            if(car != gameObject) {
                Vector3 dir = gameObject.transform.InverseTransformPoint(car.transform.position);

                if(dir.z > 0) {
                    otherCar = car;
                }
            }
        }
    }

    public override TaskStatus OnUpdate() {

        if(otherCar != null) {
            Vector3 target = otherCar.transform.position;
            target.y = gameObject.transform.position.y;

            Quaternion q = Quaternion.LookRotation(target - gameObject.transform.position);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, q, 250 * Time.deltaTime * MULIPLY);
            float distance = Vector3.Distance(gameObject.transform.position, target);

            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed * MULIPLY);

            if (distance < MIN_DISTANCE) {
                timer += Time.deltaTime;
            }

            return timer > 1f ? TaskStatus.COMPLETED : TaskStatus.RUNNING;
        } else {
            return TaskStatus.FAILED;
        }
    }

}