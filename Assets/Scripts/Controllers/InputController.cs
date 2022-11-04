using UnityEngine;
using UnityEngine.EventSystems;

public class InputController:IExecute
{
    private readonly BaseThrowableBubble _throwableBubble;
    private readonly Aim _aim;

    public InputController(BaseThrowableBubble throwableBubble,Aim aim)
    {
        _throwableBubble = throwableBubble;
        _aim = aim;
    }

    public void Execute()
    {
        _throwableBubble.CheckCanShot(CheckClickOrTouch(),CheckIsPointerOverGameObject());
        _throwableBubble.Aim(_aim,Input.mousePosition,Input.GetMouseButton(0),CheckIsPointerOverGameObject()); 
        _throwableBubble.Shot(_aim,Input.GetMouseButtonUp(0),CheckIsPointerOverGameObject());
        _throwableBubble.Move();
    }

    private bool CheckIsPointerOverGameObject()
    {
        if (Input.touches.Length > 0&&EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            return true;
        }
        else if (Input.touches.Length > 0&&!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            return false;
        }
        else
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }

    private bool CheckClickOrTouch()
    {
        if (Input.touches.Length > 0&&Input.touches[0].phase == TouchPhase.Began)
        {
            return true;
        }
        else if (Input.touches.Length == 0 && Input.GetMouseButtonDown(0))
        {
            return true;
        }

        return false;
    }
    
}
