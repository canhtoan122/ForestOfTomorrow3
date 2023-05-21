using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerUI : Singleton<ControllerUI>
{
    public event UnityAction OnAttackTriggered;
    public event UnityAction OnJumpTriggered;
    public event UnityAction OnDashTriggered;
    public event UnityAction<bool> OnMoveLeftTriggered;
    public event UnityAction<bool> OnMoveRightTriggered;
    public event UnityAction OnOpenDoor;
    public event UnityAction<EInteractState> OnInteractTriggered;

    [SerializeField] private GameObject _movementUI;
    [SerializeField] private GameObject _btnAttack;
    [SerializeField] private GameObject _btnJump;
    [SerializeField] private GameObject _btnDash;
    [SerializeField] private GameObject _btnMove;
    [SerializeField] private GameObject _btnOpenDoor;
    [SerializeField] private GameObject _btnInteract;
    [SerializeField] private Image _imgHeal;

    private EInteractState _interactState = EInteractState.NONE;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetUIOnloadScene;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetUIOnloadScene;
    }

    private void ResetUIOnloadScene(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Contains("AP"))
        {
            _movementUI.SetActive(true);
        }    
        ActiveOpenDoorButton(false);
        ActiveAttackButton(true);
    }    

    public void Attack()
    {
        OnAttackTriggered?.Invoke();
    }

    public void Jump()
    {
        OnJumpTriggered?.Invoke();
    }

    public void Dash()
    {
        OnDashTriggered?.Invoke();
    }

    public void MoveLeftDown()
    {
        OnMoveLeftTriggered?.Invoke(true);
    }
    public void MoveLeftUp()
    {
        OnMoveLeftTriggered?.Invoke(false);
    }
    public void MoveRightDown()
    {
        OnMoveRightTriggered?.Invoke(true);
    }
    public void MoveRightUp()
    {
        OnMoveRightTriggered?.Invoke(false);
    }
    public void OpenDoor()
    {
        OnOpenDoor?.Invoke();
    }

    public void Interact()
    {
        OnInteractTriggered?.Invoke(_interactState);
    }

    public void SetInteractState(EInteractState state)
    {
        _interactState = state;
    }

    public void ActiveMovementUI(bool isActive)
    {
        _movementUI.SetActive(isActive);
    }

    public void ActiveAttackButton(bool isActive)
    {
        _btnAttack.SetActive(isActive);
    }

    public void ActiveDashButton(bool isActive)
    {
        _btnDash.SetActive(isActive);
    }

    public void ActiveJumpButton(bool isActive)
    {
        _btnJump.SetActive(isActive);
    }
    public void ActiveOpenDoorButton(bool isActive)
    {
        _btnOpenDoor.SetActive(isActive);
    }

    public void ActiveInteractButton(bool isActive)
    {
        _btnInteract.SetActive(isActive);
    }

    public void SetImageHealButton(Sprite sprite)
    {
        _imgHeal.sprite = sprite;
    }

    public void ActiveHealButton(bool isActive)
    {
        _imgHeal.gameObject.SetActive(isActive);
    }
}

public enum EInteractState
{
    NONE = 0,
    PICKUP = 1,
    OPEN = 2,
    ACTIVEMAP
}
