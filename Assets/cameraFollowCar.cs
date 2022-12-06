using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowCar : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotatoinSpeed;


    private void LateUpdate()
    {
        moveTheCamera();
        rotateTheCamera();
    }

    private void moveTheCamera()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void rotateTheCamera()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotatoinSpeed * Time.deltaTime);
    }
}
