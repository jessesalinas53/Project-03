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
    [SerializeField] AudioClip startBlinkAudio;
    [SerializeField] AudioClip executeBlinkAudio;
    [SerializeField] Transform blinkPointPosition;

    RaycastHit objectHit;

    [SerializeField] CharacterController characterController;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
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
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);

        handCastParticles.Play();
        blinkPointParticles.Play();
        //AudioSource.PlayClipAtPoint(startBlinkAudio, this.gameObject.transform.position);

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

        handCastParticles.Stop();
        blinkPointParticles.Stop();
        climbableParticles.Stop();
        //AudioSource.PlayClipAtPoint(executeBlinkAudio, this.gameObject.transform.position);

        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);

        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
        {
            transform.position = objectHit.point + objectHit.normal * 1.5f;

            if (objectHit.transform.tag == "Climbable")
            {
                transform.position += Vector3.up;
                transform.position += Vector3.forward;
                //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
                //transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
            }
        }
        characterController.enabled = true;

    }
}
