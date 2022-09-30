using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")] 
    [SerializeField] private float controlSpeed = 30f;
    [Tooltip("how far the player movevs horizontally"), SerializeField] private float xRange = 12f;
    [Tooltip("how far the player movevs vertically"), SerializeField] private float yRange = 9f;

    [Header("Laser gun array")]
    [Tooltip("Add all the playaer lasers here")]
    [SerializeField] private ParticleSystem[] lasers;

    [Header("Screen position based turning")]
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -10f;
    [SerializeField] private float positionYawFactor = 2f;
    [SerializeField] private float controlRollFactor = -20f;

    [Header("New Unity Input System Action for the player movement and firing ")]
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction fire;

    private float xThrow;
    private float yThrow;

    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos,
                                              clampedYPos,
                                              transform.localPosition.z);

        // Debug.Log("HorizontalThrow: " + xThrow);
        //Debug.Log("VerticalThrow: " + yThrow);

        /*
        float horizontalThrow = Input.GetAxis("Horizontal");
        Debug.Log("HorizontalThrow: " + horizontalThrow);

        float verticalThrow = Input.GetAxis("Vertical");
        Debug.Log("VerticalThrow: " + verticalThrow);
        */
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float rollDueToControlThrow = xThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        // if pushiing firing button, 
        // then print shooting
        // else don't print "shooting"
        if (fire.ReadValue<float>() > 0.5f)
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }

    public void SetLaserActive(bool isActive)
    {
        foreach (ParticleSystem laser in lasers)
        {
            var emissionModule = laser.emission;
            emissionModule.enabled = isActive;
        }
    }

}
