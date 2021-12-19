using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [NonSerialized] public Transform desiredTransform;
    private Rigidbody rb;
    public float rotationAngle = 10f;
    float smoothTime = 1.0f;
    public float floatingTime;
    private float floatTime;
    public LineRenderer lineRenderer;
    Vector3 startPos;


    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        floatTime = floatingTime;
        StartCoroutine(nameof(MoveBalloons));
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        startPos = transform.position;

    }

    private void Update()
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, transform.GetChild(0).GetChild(0).position);

        #region Constrait Rotation
        if (transform.GetChild(0).transform.rotation.x >= -rotationAngle ||
            transform.GetChild(0).transform.rotation.x <= rotationAngle ||
            transform.GetChild(0).transform.rotation.y >= -rotationAngle ||
            transform.GetChild(0).transform.rotation.y <= rotationAngle ||
            transform.GetChild(0).transform.rotation.z >= -rotationAngle ||
            transform.GetChild(0).transform.rotation.z <= rotationAngle)
        {
            Quaternion desiredRotation = Quaternion.Euler(rotationAngle, rotationAngle, rotationAngle);
            transform.GetChild(0).transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothTime);
        }
        #endregion




    }


    IEnumerator MoveBalloons()
    {

        while (true)
        {
            if (floatTime >= 0)
            {
                floatTime -= Time.deltaTime;
                var rbPos = rb.transform.position;
                var desiredPos = desiredTransform.position;
                rb.MovePosition(Vector3.Lerp(rbPos, new Vector3((rbPos.x + desiredPos.x) / 2, desiredPos.y, (rbPos.z + desiredPos.z) / 2), Time.deltaTime * 5));
            }
            else
            {
                yield break;
            }
            yield return null;
        }
    }
}
