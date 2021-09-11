using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [Header  ("Arif Light Control Settings")]
    [Tooltip("this is where you can change Arif's movement! ")]
    [SerializeField] float controlSpeed = 1f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;
    [SerializeField] float yDownRange = 5f;
    [SerializeField] GameObject[] lasers;
     
    

    //pitch veriables
    [Header("Pitch")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -2f;

    //yaw veriables
    [Header("Yaw")]
    [SerializeField] float rotationYawFactor = 2f;
    [SerializeField] float controlYawFactor = 2f;

    //roll veriables
    [Header("Roll")]
    [SerializeField] float rotationRollFactor = 2f;
    [SerializeField] float controlRollFactor = 2f;

    float yThrow, xThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToRotation = transform.localRotation.x * rotationYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;

        float rollDueToRotation = transform.localRotation.x * rotationRollFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;



        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToRotation + yawDueToControlThrow;
        float roll = rollDueToRotation + rollDueToControlThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, -yDownRange, yRange);


        transform.localPosition = new Vector3
            (clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetLasersActive(true);
        }
        else 
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissonModule = laser.GetComponent<ParticleSystem>().emission;
            emissonModule.enabled = isActive;
        }
    }
}
