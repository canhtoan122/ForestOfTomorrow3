using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCollider : MonoBehaviour
{
    public Level1Controller level1Controller;
    public GameObject portalBG;
    public GameObject insidePortalPatern;
    public GameObject outsidePortalPatern;

    private Animator animator;

    private bool isActive = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isActive)
        {
            insidePortalPatern.transform.Rotate(0f, 0f, -100f * Time.deltaTime);
            outsidePortalPatern.transform.Rotate(0f, 0f, 100f * Time.deltaTime);
        }
    }
    public void SpinPortal()
    {
        isActive = true;
        animator.SetBool("isOpen", false);
        portalBG.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        level1Controller.OpenTeleportPortal();
        if (collision.CompareTag("Player"))
        {
            if (!isActive)
            {
                ActivateCheckPoint(collision);
                animator.SetBool("isOpen", true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        level1Controller.CloseTeleportPortal();
    }
    public void ActivateCheckPoint(Collider2D collision)
    {
        Level1Controller.lastCheckPointPosition = collision.transform.position;
    }
}
