using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    public bool isGrappling = false;

    public PlayerController playerController;
    public InputHandler inputHandler;

    public bool canPress;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        canPress = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerController.weaponID == 1 || inputHandler.FireTriggered)
        {
            StartGrapple();
            isGrappling = true;
        }
        else if (Input.GetMouseButtonUp(0) || !inputHandler.FireTriggered)
        {
            StopGrapple();
            isGrappling = false;
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable) && playerController.weaponID == 1)
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

           
            joint.maxDistance = distanceFromPoint * 0f;
            joint.minDistance = distanceFromPoint * 0f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
      
        if (!joint && playerController.weaponID == 1) return;

        if(playerController.weaponID == 1)
        {
            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, currentGrapplePosition);
        }
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
