using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : EnemyBase
{
    public GameObject[] waypoints;

    private int index = 0;
    public float minDistance = 1f;
    public float speed = 1f;

    public override void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[index].transform.position) < minDistance)
        {
            NextWaypoint();
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[index].transform.position, Time.deltaTime * speed);

        var targetPos = new Vector3(waypoints[index].transform.position.x, this.transform.position.y, waypoints[index].transform.position.z);
        transform.LookAt(targetPos);
    }

    public void NextWaypoint()
    {
        index++;

        if (index >= waypoints.Length)
        {
            index = 0;
        }
    }
}
