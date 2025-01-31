using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    private Camera _mainCamera;

    private Vector3 _mousePosition;

    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollow();
    }

    private void MouseFollow(){

        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = _mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
