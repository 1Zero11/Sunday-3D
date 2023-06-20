using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxBreakTorque;
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public Button buttonForward;
    public Button buttonBackward;
    public Button buttonLeft;
    public Button buttonRight;

    float horizontal;
    float vertical;

    public Material carMaterial;

    //public Joystick joystick;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if(DataHoarder.color != Color.black)
            carMaterial.color = DataHoarder.color;
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * vertical;
        float breaking = maxBreakTorque * vertical;
        float steering = maxSteeringAngle * horizontal;



        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                //Debug.Log(axleInfo.leftWheel.rpm);
                if(Mathf.Sign(axleInfo.leftWheel.rpm) == -Mathf.Sign(motor) && Mathf.Abs(axleInfo.leftWheel.rpm) > 1)
                {
                    axleInfo.leftWheel.motorTorque = 0;
                    axleInfo.rightWheel.motorTorque = 0;
                    axleInfo.leftWheel.brakeTorque = Mathf.Abs(breaking);
                    axleInfo.rightWheel.brakeTorque = Mathf.Abs(breaking);


                    //Debug.Log("breaking" + breaking);
                }
                else
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;
                }
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (eventData.selectedObject == buttonForward.gameObject)
        {
            vertical = 0;
        }

        if (eventData.selectedObject == buttonBackward.gameObject)
        {
            vertical = 0;
        }

        if (eventData.selectedObject == buttonRight.gameObject)
        {
            horizontal = 0;
        }

        if (eventData.selectedObject == buttonLeft.gameObject)
        {
            horizontal = 0;
        }
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (eventData.selectedObject == buttonForward.gameObject)
        {
            vertical = 1;
        }

        if (eventData.selectedObject == buttonBackward.gameObject)
        {
            vertical = -1;
        }

        if (eventData.selectedObject == buttonRight.gameObject)
        {
            horizontal = 1;
        }

        if (eventData.selectedObject == buttonLeft.gameObject)
        {
            horizontal = -1;
        }
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
