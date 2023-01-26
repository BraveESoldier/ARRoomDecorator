using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Transform _objectPlace;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _container;

    private ARRaycastManager _arRaycastManager;
    private GameObject _installObject;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Start()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        UpdatePlacePose();

        if (Input.touchCount == 2)
            SetObject();

    }

    private void UpdatePlacePose()
    {
        Vector2 screenCentre = _camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        var ray = _camera.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            SetObjectPosition(raycastHit.point);
        }
        else if (_arRaycastManager.Raycast(screenCentre, _hits, TrackableType.PlaneWithinPolygon))
        {
            SetObjectPosition(_hits[0].pose.position);
        }
    }

    private void SetObjectPosition(Vector3 position)
    {
        _objectPlace.position = position;

        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRotation = new Vector3(cameraForward.x,0,cameraForward.z);
        //_objectPlace.rotation = Quaternion.Euler(cameraRotation);
    }

    private void SetObject()
    {
        _installObject.GetComponent<Collider>().enabled = true;
        _installObject.transform.parent = _container.transform;
        _installObject = null;
    }

    public void SetInstallObject(ItemData itemData)
    {
        if(_installObject != null)
            Destroy(_installObject);
        _installObject = Instantiate(itemData.Prefab, _objectPlace);
        _installObject.GetComponent<Collider>().enabled = false;

    }
}
