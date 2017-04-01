using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

using BBUnity.Actions;

[Action("Car/FindNextPoint")]
public class FindNextPoint : GOAction {

    [InParam("index")]
    [OutParam("index")]
    public int index;

    private TrackGenerator trackGenerator;

    public override void OnStart() {
        trackGenerator = TrackGenerator.Instance;

        base.OnStart();
    }

    public override TaskStatus OnUpdate() {

        if(trackGenerator.waypoints.Length > 0) {
            index++;

            if (index >= trackGenerator.waypoints.Length) {
                index = 0;
            }

            return TaskStatus.COMPLETED;
        } else {
            return TaskStatus.RUNNING;
        }
    }
}
