using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectGrabbing : MonoBehaviour {

    [SerializeField] private Transform _grabPosition;
    private bool _isGrabbing = false;
    private Camera _mainCamera;

    private ObjectGrabbable _objectGrabbable;

    private void Awake() {
        _mainCamera = Camera.main;
    }

    public void OnUse(InputAction.CallbackContext context) {
        if (context.performed) {
            HandleObjectGrabbing();
        }
    }

    private void HandleObjectGrabbing() {
        if (_isGrabbing) {
            _objectGrabbable.Drop();
            _isGrabbing = false;
            return;
        }
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit hit, 10f)) {
            if (hit.transform.TryGetComponent<ObjectGrabbable>(out var objectGrabbable)) {
                objectGrabbable.Grab(_grabPosition);
                _isGrabbing = true;
                _objectGrabbable = objectGrabbable;
            }
        }

    }
}
