using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;
using BBUnity.Conditions;

[Condition("Car/IsCloseToPoint")]
public class IsCloseToPoint : GOCondition {

    [InParam("track")]
    [Help("Script that created the track")]
    public TrackGenerator trackGenerator;

    [InParam("index")]
    public int index;

    public override bool Check() {
        if(trackGenerator.waypoints.Length > 0) {
            Vector3 waypointPos = new Vector3(trackGenerator.waypoints[index].waypaoint.x, gameObject.transform.position.y, trackGenerator.waypoints[index].waypaoint.y);

            return (Vector3.Distance(gameObject.transform.position, waypointPos) < 5f);
        }else {
            return false;
        }
    }
}
