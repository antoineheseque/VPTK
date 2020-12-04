using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class OverridePositionTracker : SteamVR_Behaviour_Pose
{
    private float lerpPosition = 1f;
    private float lerpRotation = 1f;
    
    protected override void UpdateTransform()
    {
        CheckDeviceIndex();

        if (origin != null)
        {
            transform.position = Vector3.Lerp(transform.position, origin.transform.TransformPoint(poseAction[inputSource].localPosition), lerpPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, origin.rotation * poseAction[inputSource].localRotation, lerpRotation);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.position, poseAction[inputSource].localPosition, lerpPosition);
            transform.localRotation = Quaternion.Lerp(transform.rotation, poseAction[inputSource].localRotation, lerpRotation);
        }
        Debug.Log("ON ACTUALISE ICI");
    }
}
