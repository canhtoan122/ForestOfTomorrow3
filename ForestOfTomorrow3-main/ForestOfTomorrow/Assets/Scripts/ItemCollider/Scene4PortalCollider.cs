using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4PortalCollider : MonoBehaviour
{
    public MapController mapController;
    public GameObject insidePortalPatern;
    public GameObject outsidePortalPatern;
    private void Update()
    {
        insidePortalPatern.transform.Rotate(0f, 0f, -100f * Time.deltaTime);
        outsidePortalPatern.transform.Rotate(0f, 0f, 100f * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        mapController.ActivateMap();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        mapController.DeactivateMap();
    }
}
