using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("Car/DriveToPointCenter")]
[Help("Drive the ai to a target location")]
public class DriveToPointCenter : GOAction {

    private TrackGenerator trackGenerator;

    [InParam("index")]
    public int index;

    [InParam("speed")]
    private float speed = 15;

    public override void OnStart() {
        trackGenerator = TrackGenerator.Instance;
    }

    public override TaskStatus OnUpdate() {

        if(index < trackGenerator.waypoints.Length) {
            Vector3 target = new Vector3(trackGenerator.waypoints[index].waypaoint.x, gameObject.transform.position.y, trackGenerator.waypoints[index].waypaoint.y);

            Quaternion q = Quaternion.LookRotation(target - gameObject.transform.position);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, q, 250 * Time.deltaTime);
            float distance = Vector3.Distance(gameObject.transform.position, target);

            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);

            return distance < 5 ? TaskStatus.COMPLETED : TaskStatus.RUNNING;

        } else {
            return TaskStatus.FAILED;
        }

    }
}

