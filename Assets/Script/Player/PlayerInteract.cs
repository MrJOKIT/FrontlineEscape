using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private Camera cam;
    private PlayerUI _playerUI;
    private InputManager _inputManager;

    private void Start()
    {
        cam = gameObject.GetComponent<PlayerLook>().playerCam;
        _playerUI = gameObject.GetComponent<PlayerUI>();
        _inputManager = gameObject.GetComponent<InputManager>();
    }

    private void Update()
    {
        _playerUI.UpdateText(string.Empty);
        //create a ray at the center of the camera,shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction * distance,Color.black);
        RaycastHit hitInfo; //variable to store our collision information.
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                _playerUI.UpdateText(interactable.promptMessage);
                if (_inputManager._onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
