using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//logika za grab
public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        //grab funkcija prima gde je point a on je definisan gore tj u unitiju
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
    }
    public void Drop()
    {
        //isto sve samo krece gravitacija
        this.objectGrabPointTransform= null;
        objectRigidbody.useGravity= true;
    }
    private void FixedUpdate()
    {
        if(objectGrabPointTransform != null)
        {
            //ovo da ne bi mnogo seckalo pomeranje objekta
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
