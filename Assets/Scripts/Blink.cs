using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 40f;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] ParticleSystem handCastParticles;
    [SerializeField] ParticleSystem blinkPointParticles;
    [SerializeField] ParticleSystem climbableParticles;
    [SerializeField] AudioSource startBlinkAudio;
    [SerializeField] AudioSource executeBlinkAudio;
    [SerializeField] Transform blinkPointPosition;
    [SerializeField] CharacterController characterController;
    RaycastHit objectHit;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            startBlinkAudio.Play();
        }
        if (Input.GetMouseButton(0))
        {
            StartBlink();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ExecuteBlink();
        }
    }

    void StartBlink()
    {
        executeBlinkAudio.Stop();

        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, .25f);

        handCastParticles.Play();
        blinkPointParticles.Play();

        if (Physics.Raycast(rayOrigin.position, rayDirection * shootDistance, out objectHit, shootDistance))
        {
            blinkPointParticles.transform.position = objectHit.point + objectHit.normal;
            if (objectHit.transform.tag == "Climbable")
            {
                climbableParticles.Play();
                climbableParticles.transform.position = objectHit.point + objectHit.normal;
            }
            else
            {
                climbableParticles.Stop();
            }
        }
    }

    void ExecuteBlink()
    {
        characterController.enabled = false;

        startBlinkAudio.Stop();
        handCastParticles.Stop();
        blinkPointParticles.Stop();
        climbableParticles.Stop();
        
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, .25f);

        player.GetComponent<PlayerMovement>().gravity = -4f;

        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
        {
            transform.position = blinkPointParticles.transform.position;
            //transform.position = objectHit.point + objectHit.normal;
            executeBlinkAudio.Play();
            if (objectHit.transform.tag == "Climbable")
            {
                //transform.position += Vector3.up;
                //transform.position += Vector3.forward;
                //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
                //transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
            }
        }
        characterController.enabled = true;
    }
}
