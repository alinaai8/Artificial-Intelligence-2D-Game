using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform checkpointPosition;
    [SerializeField] private Transform enemy1;
    [SerializeField] private Transform enemy2;
    [SerializeField] private Transform enemy3;
    [SerializeField] private Transform checkpoint1;
    [SerializeField] private Transform checkpoint2;
    [SerializeField] private Transform checkpoint3;
    [SerializeField] private Transform checkpointSuplimentar;
    [SerializeField] private Transform checkpointStart;
    [SerializeField] private Transform checkpointEnd;
    [SerializeField] private Transform checkpointStop;
    [SerializeField] private float moveSpeedProgram = 1f;
    [SerializeField] private GameObject background;
    Rigidbody2D body;
    [SerializeField] private float startTime;
    public float timeRewardMultiplier = 1f;
    private bool waiting = true;
    private float waitStartTime;
    private float waitDuration = 5f;

    private void Start()
    {

        body = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        waitStartTime = Time.time;

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(checkpointPosition.localPosition);
        sensor.AddObservation(checkpoint1.localPosition);
        sensor.AddObservation(checkpoint2.localPosition);
        sensor.AddObservation(checkpoint3.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(checkpointSuplimentar.localPosition);
        sensor.AddObservation(checkpointStart.localPosition);
        sensor.AddObservation(enemy1.localPosition);
        sensor.AddObservation(enemy2.localPosition);
        sensor.AddObservation(enemy3.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, moveY, 0) * Time.deltaTime * moveSpeedProgram;

        //body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(-5f, -1f, 0);
        checkpointPosition.gameObject.SetActive(true);
        checkpointSuplimentar.gameObject.SetActive(true);
        checkpointStart.gameObject.SetActive(true);
        checkpoint1.gameObject.SetActive(true);
        checkpoint2.gameObject.SetActive(true);
        checkpoint3.gameObject.SetActive(true);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continiousActions = actionsOut.ContinuousActions;
        continiousActions[0] = Input.GetAxisRaw("Horizontal");
        continiousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Map")) AddReward(-5f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Checkpoint"))
        {
            AddReward(100f);
            collision.gameObject.SetActive(false);
        }
        if (collision.tag.Equals("Checkpoint1")) {
            AddReward(200f);
            collision.gameObject.SetActive(false);
        };
        if (collision.tag.Equals("Checkpoint2")){
            AddReward(300f);
            collision.gameObject.SetActive(false);
        }
        if (collision.tag.Equals("Checkpoint3"))
        {
            collision.gameObject.SetActive(false);
            AddReward(400f);
        }
        if (collision.tag.Equals("Checkpoint4"))
        {
            collision.gameObject.SetActive(false);
            AddReward(500f);
        }
        if (collision.tag.Equals("Enemy"))
        {
            AddReward(-10f);
            transform.position = spawnPoint.transform.position;
        }
        if (collision.tag.Equals("EndZone"))
        {
            AddReward(600f);
        }
        if (collision.tag.Equals("Checkpoint5"))
        {
            AddReward(1000f);
            EndEpisode();
        }
    }


    private void Update()
    {
        if (GetCumulativeReward() < -0.5f)
            background.GetComponent<SpriteRenderer>().color = new Color(185f / 255, 178f/255, 101f/255);
        if (GetCumulativeReward() < -0.2f)
            background.GetComponent<SpriteRenderer>().color = new Color(76f/255,0,0);
        if (GetCumulativeReward() == 0f)
            background.GetComponent<SpriteRenderer>().color = Color.black;
        if (GetCumulativeReward() > 0f)
            background.GetComponent<SpriteRenderer>().color = new Color(2f / 255, 0, 101f / 255);
        if (GetCumulativeReward() > 1f)
            background.GetComponent<SpriteRenderer>().color = Color.green;
        if(transform.position == checkpointStart.position)
        {
            float timeTaken = Time.time - startTime;
            float timeReward = timeRewardMultiplier / timeTaken;
            AddReward(timeReward);
            transform.localPosition = new Vector3(-5f, -1f, 0);
            startTime = Time.time;
        }
        if (transform.position == checkpoint1.position)
        {
            float timeTaken = Time.time - startTime;
            float timeReward = timeRewardMultiplier / timeTaken;
            AddReward(timeReward);
            transform.localPosition = new Vector3(-5f, -1f, 0);
            startTime = Time.time;
        }
        if (transform.position == checkpoint2.position)
        {
            float timeTaken = Time.time - startTime;
            float timeReward = timeRewardMultiplier / timeTaken;
            AddReward(timeReward);
            transform.localPosition = new Vector3(-5f, -1f, 0);
            startTime = Time.time;
        }
        if (transform.position == checkpoint3.position)
        {
            float timeTaken = Time.time - startTime;
            float timeReward = timeRewardMultiplier / timeTaken;
            AddReward(timeReward);
            transform.localPosition = new Vector3(-5f, -1f, 0);
            startTime = Time.time;
        }
        if (transform.position == checkpointSuplimentar.position)
        {
            float timeTaken = Time.time - startTime;
            float timeReward = timeRewardMultiplier / timeTaken;
            AddReward(timeReward);
            transform.localPosition = new Vector3(-5f, -1f, 0);
            startTime = Time.time;
        }
        if (transform.position == checkpointEnd.position)
        {
            float timeTaken = Time.time - startTime;
            float timeReward = timeRewardMultiplier / timeTaken;
            AddReward(timeReward);
            transform.localPosition = new Vector3(-5f, -1f, 0);
            startTime = Time.time;
        }
    }
}
