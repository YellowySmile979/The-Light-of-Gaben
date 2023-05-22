using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();

    public int Length { get => patrolPoints.Count; }

    [Header("Gizmos Parameters")]
    public Color pointsColour = Color.blue;
    public Color lineColour = Color.green;
    public float pointSize = 0.3f;

    //passes info to our AI
    public struct PathPoint
    {
        public int Index;
        public Vector2 Position;
    }
    //gets the closest path point for the enemy to want to move to
    public PathPoint GetClosestPathPoint(Vector2 enemyPosition)
    {
        var minDistance = float.MaxValue;
        var index = -1;
        //for all patrol points, calculates the distance between the enemy and the patrol point's position
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            var tempDistance = Vector2.Distance(enemyPosition, patrolPoints[i].position);
            //if the tempDistance is less than the minDistance, then it should be the closest point
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                index = i;
            }
        }
        //returns a new struct and sets the point that the enemy should move towards
        return new PathPoint { Index = index, Position = patrolPoints[index].position };
    }
    //gets the next path point
    public PathPoint GetNextPathPoint(int index)
    {
        //current index + 1 (next point in line) if it is >= the amount of patrolPoints, return zero
        //if it isnt, then return index + 1 (essentially checks to see if the point exists)
        var newIndex = index + 1 >= patrolPoints.Count ? 0 : index + 1;
        //returns the new, updated parameters
        return new PathPoint { Index = newIndex, Position = patrolPoints[newIndex].position };
    }

    //draws lines for us to see the path the enemy will take
    void OnDrawGizmos()
    {
        if (patrolPoints.Count == 0) return;
        for (int i = patrolPoints.Count - 1; i >= 0; i--)
        {
            if (i == -1 || patrolPoints[i] == null) return;

            Gizmos.color = pointsColour;
            Gizmos.DrawSphere(patrolPoints[i].position, pointSize);

            if (patrolPoints.Count == 1 || i == 0) return;

            Gizmos.color = lineColour;
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i - 1].position);
            if (patrolPoints.Count > 2 && i == patrolPoints.Count - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
            }
        }
    }
}
