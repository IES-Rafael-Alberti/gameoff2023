using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw_Movement: MonoBehaviour
{
    public float speed;
    public int startingPoint;
    // Array of points to go to.
    public Transform[] points;

    // Array index.
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            // Checks for last point.
            if (i == points.Length)
            {
                i = 0;
            }
        }
        // Moves platform to the point indicated by the index.
        transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
