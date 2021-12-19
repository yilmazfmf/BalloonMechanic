using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class BalloonCreator : MonoBehaviour
{
    public GameObject balloon;
    public Transform DesireTransform;
    public Material[] materials;
    private Material randomMaterial;



    private void Update()
    {
           

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Select Random Material
            randomMaterial = materials[UnityEngine.Random.Range(0, 4)];
            balloon.GetComponentInChildren<MeshRenderer>().material = randomMaterial;
           
            //Instantiate the Balloons at the BalloonCreator transform's position
            var tempBalloon = Instantiate(balloon, transform.position, transform.rotation);

            //Make the balloons go to the desiredPosition 
            tempBalloon.GetComponent<Balloon>().desiredTransform = DesireTransform;


        }
    }
}
