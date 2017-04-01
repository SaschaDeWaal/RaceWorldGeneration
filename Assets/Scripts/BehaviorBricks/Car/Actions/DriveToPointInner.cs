using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions {

    [Action("Car/DriveToPointInner")]
    [Help("Drive the ai to a target location")]
    public class DriveToPointInner : GOAction {

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
                Vector3 target = new Vector3(trackGenerator.waypoints[index].waypaoint.x * 0.9f, gameObject.transform.position.y, trackGenerator.waypoints[index].waypaoint.y * 0.9f);

                Quaternion q = Quaternion.LookRotation(target - gameObject.transform.position);
                gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, q, 250 * Time.deltaTime);

                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);

                float distance = Vector3.Distance(gameObject.transform.position, target);

                return distance < 5 ? TaskStatus.COMPLETED : TaskStatus.RUNNING;

            } else {
                return TaskStatus.FAILED;
            }

        }
    }
}

