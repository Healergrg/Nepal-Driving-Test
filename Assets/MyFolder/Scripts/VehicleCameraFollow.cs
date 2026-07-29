using UnityEngine;

public class VehicleCameraFollow : MonoBehaviour 
{ 
    [SerializeField] private Transform target; 
    [SerializeField] private float height = 8f; 
    [SerializeField] private float distanceBehind = 12f; 
    [SerializeField] private float followSpeed = 8f; 
    [SerializeField] private float rotationSpeed = 8f; 

    void LateUpdate() 
    { 
        if (target == null) 
        { 
            return; 
        } 

        // CHANGED: We now subtract target.forward so the camera trails BEHIND the car
        Vector3 desiredPosition = 
            target.position 
            - target.forward * distanceBehind 
            + Vector3.up * height; 

        transform.position = Vector3.Lerp( 
            transform.position, 
            desiredPosition, 
            followSpeed * Time.deltaTime 
        ); 

        Quaternion desiredRotation = Quaternion.LookRotation( 
            target.position - transform.position, 
            Vector3.up 
        ); 

        transform.rotation = Quaternion.Slerp( 
            transform.rotation, 
            desiredRotation, 
            rotationSpeed * Time.deltaTime 
        ); 
    } 
}