using UnityEngine;
public class ObjectGrabbable : MonoBehaviour {
    private Rigidbody _objectRigidbody;
    private Transform _objectGrabPointTransform;
    private const float _lerpSpeed = 10f;

    private void Awake() {
        _objectRigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform grabPointTransform) {
        _objectGrabPointTransform = grabPointTransform;
        _objectRigidbody.useGravity = false;
    }

    public void Drop() {
        _objectGrabPointTransform = null;
        _objectRigidbody.useGravity = true;
    }

    private void FixedUpdate() {
        if (_objectGrabPointTransform != null) {
            Vector3 newPosition = Vector3.Lerp(transform.position, _objectGrabPointTransform.position, Time.deltaTime * _lerpSpeed);
            _objectRigidbody.MovePosition(newPosition);
        }
    }
}
