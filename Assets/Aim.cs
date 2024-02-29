using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    public float maxRange = 100f;
    public float fieldOfViewAngle = 45f;
    public InputActionReference aimActionReference; 

    private Transform closestEnemy;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation; 
        aimActionReference.action.performed += _ => AimInput();
        aimActionReference.action.canceled += _ => ResetRotation();
    }

    void AimInput()
    {
        FindClosestEnemy();
        AimAtClosestEnemy();
    }

    void ResetRotation()
    {
        transform.rotation = originalRotation; 
    }

    void FindClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange);

        closestEnemy = null;
        float closestDistSqr = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Vector3 directionToTarget = collider.transform.position - transform.position;
                float angle = Vector3.Angle(directionToTarget, transform.forward);

                if (angle < fieldOfViewAngle * 0.5f)
                {
                    float distSqrToTarget = directionToTarget.sqrMagnitude;

                    if (distSqrToTarget < closestDistSqr)
                    {
                        closestDistSqr = distSqrToTarget;
                        closestEnemy = collider.transform;
                    }
                }
            }
        }
    }

    void AimAtClosestEnemy()
    {
        if (closestEnemy != null)
        {
            Vector3 directionToEnemy = closestEnemy.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 360f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRange);

        Vector3 forward = transform.forward;
        Vector3 leftRayDirection = Quaternion.Euler(0, -fieldOfViewAngle * 0.5f, 0) * forward;
        Vector3 rightRayDirection = Quaternion.Euler(0, fieldOfViewAngle * 0.5f, 0) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftRayDirection * maxRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * maxRange);
    }
}