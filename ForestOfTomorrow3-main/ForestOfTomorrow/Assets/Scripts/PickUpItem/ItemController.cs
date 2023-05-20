using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private GameObject step6;
    [SerializeField]
    private GameObject step7;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ControllerUI.Instance.ActiveAttackButton(false);
            ControllerUI.Instance.SetInteractState(EInteractState.PICKUP);
            ControllerUI.Instance.ActiveInteractButton(true);
            step6.SetActive(false);
            step7.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ControllerUI.Instance.ActiveAttackButton(true);
            ControllerUI.Instance.SetInteractState(EInteractState.NONE);
            ControllerUI.Instance.ActiveInteractButton(false);
        }
    }
}
