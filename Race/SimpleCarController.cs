using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;


public class SimpleCarController : Agent
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public GameObject end;
    float dis;

    Rigidbody rBody;
    Vector3 startingLoc; 
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        startingLoc = this.transform.localPosition;
        dis = Vector3.Distance(end.transform.localPosition, this.transform.localPosition);
    }


    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = startingLoc;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        dis = Vector3.Distance(end.transform.localPosition, this.transform.localPosition);
        Debug.Log("New Cycle");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent localPositions
        sensor.AddObservation(end.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z); 
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        float motor = maxMotorTorque * vectorAction[0];
        float steering = maxSteeringAngle * vectorAction[1];
        Debug.Log(vectorAction[0] + " " + vectorAction[1]);
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }

        if (rBody.velocity.z < 0)
        {
            AddReward(-0.1f);
        }
        else
        {
            AddReward(0.1f);
        }
        if (this.transform.localPosition.y < 0)
        {
            Debug.Log("-5");
            AddReward(-5);
            EndEpisode();
        }
        if (Vector3.Distance(end.transform.localPosition, this.transform.localPosition) < dis)
        {
            dis = Vector3.Distance(end.transform.localPosition, this.transform.localPosition);
            AddReward(0.1f);
            //Debug.Log("0.1");
        }
        else
        {
            //Debug.Log("-0.1");
            AddReward(-0.1f);
        }
        if (Vector3.Distance(end.transform.localPosition, this.transform.localPosition) < 1)
        {
            SetReward(10);
            EndEpisode();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            //Debug.Log(collision.collider.name);
            //            movement.enabled = false; 
            Debug.Log("Colli");
            SetReward(-5);
            EndEpisode();
        }
    }

    public override void Heuristic(float[] action)
    {
        action[0] = Input.GetAxis("Vertical");
        action[1] = Input.GetAxis("Horizontal");
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}