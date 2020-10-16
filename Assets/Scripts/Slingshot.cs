using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject prefabProjectile, projectile;
    public Vector3 launchPos;
    public float velocityMult = 8f;
    public bool aimingMode;
    
    private GameObject _launchPoint;
    private Rigidbody _projectileRigidbody;
    private Camera _camera;

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        _launchPoint = launchPointTrans.gameObject;
        _launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -_camera.transform.position.z;
        Vector3 mousePos3D = _camera.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = GetComponent<SphereCollider>().radius;
        
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            _projectileRigidbody.isKinematic = false;
            _projectileRigidbody.velocity = -mouseDelta * velocityMult;
            projectile = null;
        }
    }

    private void OnMouseEnter()
    {
        print("Slingshot Enter");
        _launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        print("Slingshot Exit");
        _launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile);
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        _projectileRigidbody = projectile.GetComponent<Rigidbody>();
        _projectileRigidbody.isKinematic = true;
    }
}
