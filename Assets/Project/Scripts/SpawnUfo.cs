using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnUfo : MonoBehaviour
{
    [SerializeField] private GameObject ufoPrefab;
    [SerializeField] private GameObject _padVisual;

    [SerializeField] private UIInGame _ui;
    [SerializeField]private ARRaycastManager raycastManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    
    private void Awake()
    {
        _padVisual.SetActive(false);
    }

    private void Update()
    {
        if (raycastManager.enabled == false)
        {
            return;
        }
        if(raycastManager.Raycast(new Vector2(Screen.width/2,Screen.height/2),_hits,TrackableType.PlaneWithinPolygon))
        {
            var hit = _hits[0].pose;
            _padVisual.transform.SetPositionAndRotation(hit.position,hit.rotation);
            float angle=Vector3.Angle(Vector3.up,_padVisual.transform.up);
            if (angle > 45)
            {
                ToggleVisualAndUUI(false);
                return;
            }
            ToggleVisualAndUUI(true);
        }
        else
        {
            ToggleVisualAndUUI(false);
        }
    }

    private void ToggleVisualAndUUI(bool active)
    {
        _padVisual.SetActive(active);
        _ui.InteractFlyBtn(active);
    }

    public void SpawnUfos()
    {
        Instantiate(ufoPrefab, _padVisual.transform.position, _padVisual.transform.rotation);
        raycastManager.enabled = false;
        _padVisual.SetActive(false);
    }
}
