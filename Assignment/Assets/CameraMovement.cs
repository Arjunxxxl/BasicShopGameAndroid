using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;

    [Header("Follow Data")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float followSpeed;
    private Vector3 followPos;
    private Vector3 followVel;

    [Header("Camera Local Data")]
    [SerializeField] private Vector3 cameraLocalPos;
    [SerializeField] private Vector3 cameraLocalRot;
    [SerializeField] private float localPosLerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetCameraRotation();
    }

    void Update()
    {
        Follow();
        UpdateCameraPos();
    }

    private void Follow()
    {
        followPos = target.transform.position + followOffset;

        transform.position = Vector3.SmoothDamp(transform.position, followPos, ref followVel, Time.deltaTime * followSpeed);
    }

    private void UpdateCameraPos()
    {
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, cameraLocalPos, Time.deltaTime * localPosLerpSpeed);
    }

    private void SetCameraRotation()
    {
        cam.transform.localRotation = Quaternion.Euler(cameraLocalRot);
    }
}
