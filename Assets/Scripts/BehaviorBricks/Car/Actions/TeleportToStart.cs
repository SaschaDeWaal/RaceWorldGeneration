using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

using BBUnity.Actions;

[Action("Car/TeleportToStart")]
[Help("MoveObjectToStart")]
public class TeleportToStart : GOAction {

    [InParam("index")]
    [OutParam("index")]
    public int index;

    private TrackGenerator trackGenerator;

    public override void OnStart() {
        trackGenerator = TrackGenerator.Instance;
    }

    public override TaskStatus OnUpdate() {

        if (trackGenerator.waypoints.Length > 0) {
            gameObject.transform.position = trackGenerator.waypoints[0].worldPosition;
            index = 1;
            return TaskStatus.COMPLETED;
        }else {
            return TaskStatus.RUNNING;
        } 
    }
}
