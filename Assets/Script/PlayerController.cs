using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In mx^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRang = 20f;
    [Tooltip("In m")] [SerializeField] float yRang = 20f;
    [SerializeField] GameObject[] guns;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.3f;
    [Header("Screen-position")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -30f;

    [Header("Control-throw")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRowFactor = -20;

    Camera PlayerRig;


    float xThrow, yThrow;
    bool isControlEnabled = true;
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTraslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = 
                gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;

            var musicModule =
                gun.GetComponent<AudioSource>();
            musicModule.enabled = isActive;

        }
    }

    void OnPlayerDeath() // called by string reference
    {
        isControlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToControlThrow + pitchDueToPosition;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRowFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTraslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRang, xRang);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRang, yRang);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        // CrossPlatformInputManager.GetButton();
    }
}
