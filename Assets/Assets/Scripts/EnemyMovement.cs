using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject[] moveToPoints;
    Vector3 currentPoint;

    public float moveSpeed;

    int pointSelection;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //Starts to move the object towards the first "moveToPoint" you set in inspector
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentPoint, Time.deltaTime * moveSpeed);

        //check to see if the object is at the next "moveToPoint"
        if (transform.localPosition == currentPoint)
        {

            //if so it sets the next moveTo location
            pointSelection++;

            //if your object hits the last "moveToPoint it sends the object back to starting position to start the sequence over
            if (pointSelection == moveToPoints.Length)
            {
                pointSelection = 0;

            }

            //sets the destination of the "moveToPoint" destination
            currentPoint = moveToPoints[pointSelection].transform.localPosition;
        }

    }


}
