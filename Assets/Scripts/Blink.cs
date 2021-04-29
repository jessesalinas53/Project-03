using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 30f;
    [SerializeField] ParticleSystem handParticles;
    [SerializeField] ParticleSystem castPointParticles;
    //might not need[SerializeField] ParticleSystem castGroundPointParticles;
    [SerializeField] ParticleSystem landingParticles;
    [SerializeField] AudioClip startCastAudio = null;
    [SerializeField] AudioClip executeCastAudio = null;
    //isnt in dh[SerializeField] AudioClip landingAudio = null;

    RaycastHit objectHit; // Stores info about raycast hit
    bool blinkFinished = false;

    private void Update()
    {
        while (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCast();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ExecuteCast();
        }
    }

    void StartCast()
    {
        // Calculate direction to shoot the ray
        Vector3 rayDirection = cameraController.transform.forward;
        // Cast a debug ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);

        //handParticles.Play();
        //castPointParticles.Play();
        //AudioSource.PlayClipAtPoint(startCastAudio, this.gameObject.transform.position);
    }

    void ExecuteCast()
    {
        //AudioSource.PlayClipAtPoint(executeCastAudio, this.gameObject.transform.position);

        // Calculate direction to shoot the ray
        Vector3 rayDirection = cameraController.transform.forward;
        // Cast a debug ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);
        // Do the raycast
        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
        {
            this.gameObject.transform.position = objectHit.point;
        }

        //wait for seconds??
        //blinkFinished = true;
        if (blinkFinished = true)
        {
            //landingParticles.Play();
            //AudioSource.PlayClipAtPoint(landingAudio, this.gameObject.transform.position);
        }
    }
}
