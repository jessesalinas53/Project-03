using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private RaycastHit lastRaycastHit;
    [SerializeField] ParticleSystem handCastParticles;
    [SerializeField] ParticleSystem blinkPointParticles;
    [SerializeField] ParticleSystem climbableParticles;
    [SerializeField] AudioClip startCastAudio;
    [SerializeField] AudioClip releaseCastAudio;

    private GameObject GetLookedAtObject()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Camera.main.transform.forward;
        float range = 100;
        if (Physics.Raycast(origin, direction, out lastRaycastHit, range))
            return lastRaycastHit.collider.gameObject;
        else
            return null;
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void TeleportToLookAt()
    {
        transform.position = lastRaycastHit.point + lastRaycastHit.normal * 2;
        if (startCastAudio != null)
            AudioSource.PlayClipAtPoint(startCastAudio, transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (GetLookedAtObject() != null)
                TeleportToLookAt();
    }

    /*[SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    CharacterController player;

    private void Start()
    {
        Cursor.visible = false;
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cast();
        }
    }

    void Cast()
    {
        RaycastHit hit;

        Vector3 rayDirection = cameraController.transform.forward;
        //Vector3 p1 = transform.position + player.center;

        float distanceToObstacle = 0;
        Debug.DrawRay(rayOrigin.position, rayDirection * distanceToObstacle, Color.blue, 1f);

        if (Physics.Raycast(rayOrigin.position, rayDirection, out hit, distanceToObstacle))
        {
            distanceToObstacle = hit.distance;
            player.transform.position = hit.transform.position;
        }
    }*/
}
