using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

using BBUnity.Actions;

[Action("Car/SelectClosestPoint")]
public class SelectClosestPoint : GOAction {

    private TrackGenerator trackGenerator;

    [InParam("index")]
    [OutParam("index")]
    public int index;

    public override void OnStart() {
        trackGenerator = TrackGenerator.Instance;
    }

    public override TaskStatus OnUpdate() {
        if(trackGenerator.waypoints.Length > 0) {

            int closedPoint = -1;
            float closedDistance = 1000;// Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), trackGenerator.waypoints[0].point);

            for(int i = 0; i < trackGenerator.waypoints.Length; i++) {

                Vector2 target = trackGenerator.waypoints[i].point;
                Vector2 objPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

                float dis = Vector2.Distance(objPos, target);

                Vector3 directionToTarget = new Vector3(objPos.x, 0, objPos.y) - new Vector3(target.x, 0, target.y);
                float angel = Vector3.Angle(gameObject.transform.forward, directionToTarget);

                if (dis < closedDistance) {
                    closedPoint = i;
                    closedDistance = dis;
                }

            }

            index = closedPoint;

            return TaskStatus.COMPLETED;
        } else {
            return TaskStatus.RUNNING;
        }
    }
 }
