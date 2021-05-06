using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 30f;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] ParticleSystem handCastParticles;
    [SerializeField] ParticleSystem blinkPointParticles;
    [SerializeField] ParticleSystem climbableParticles;
    [SerializeField] ParticleSystem groundPointParticles;
    [SerializeField] AudioSource startBlinkAudio;
    [SerializeField] AudioSource executeBlinkAudio;
    [SerializeField] Transform blinkPointStartPosition;
    [SerializeField] CharacterController characterController;
    RaycastHit objectHit;
    Transform groundPointPosition;
    

    void Start()
    {
        Cursor.visible = false;
        blinkPointStartPosition.transform.position = blinkPointParticles.transform.position;
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

        groundPointParticles.transform.position = blinkPointParticles.transform.position;
        Vector3 height = new Vector3(0, blinkPointParticles.transform.position.y + 2, 0);
        groundPointParticles.transform.position -= height;
        groundPointParticles.Play();

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
        else
            blinkPointParticles.transform.position = blinkPointStartPosition.transform.position;
    }

    void ExecuteBlink()
    {
        characterController.enabled = false;

        startBlinkAudio.Stop();
        handCastParticles.Stop();
        blinkPointParticles.Stop();
        climbableParticles.Stop();
        groundPointParticles.Stop();

        executeBlinkAudio.Play();

        transform.position = blinkPointParticles.transform.position;

        characterController.enabled = true;
    }
}
