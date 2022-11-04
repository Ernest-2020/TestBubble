using UnityEngine;

public class Aim
{
    private readonly LineRenderer _aim,_reflectionAim;
    private readonly Pool<LineRenderer> _aimPool;

    private readonly float _maxDistanceReflectionRay;
    private const float ReflectionOffset = 1.0001f;
    private const int AreaLayerMask = 3;


    public Aim(LineRenderer prefabAim,int startCount, float maxDistanceReflectionRay)
    {
        _aimPool = new Pool<LineRenderer>(null,startCount,prefabAim);
        _aim = _aimPool.GetFreeObject();
        _reflectionAim = _aimPool.GetFreeObject();
        _maxDistanceReflectionRay = maxDistanceReflectionRay;
    }
    
    public void DrawLine(Vector2 startPosition,Vector2 direction)
    {
        _aim.gameObject.SetActive(true);
        _reflectionAim.gameObject.SetActive(true);
       
        var position = startPosition;
        var hit2D = Physics2D.Raycast(position, direction);

        if (!hit2D.collider)return;

        SetPositionsLine(_aim,position,hit2D.point);
        
        position = hit2D.point/ReflectionOffset;
        direction = Vector3.Reflect(direction, hit2D.normal);
        Vector3 finishPosition = position + direction * _maxDistanceReflectionRay;
        if (hit2D.collider.gameObject.layer!= AreaLayerMask)
        {
            _reflectionAim.gameObject.SetActive(false);
            return;
        }
        SetPositionsLine(_reflectionAim, position,finishPosition);
       
        hit2D = Physics2D.Raycast(position, direction,_maxDistanceReflectionRay);
        
        if (hit2D.collider)
        {
            finishPosition = hit2D.point;
            SetPositionsLine(_reflectionAim,position,finishPosition);
        }

        
    }

    public void DisableAim()
    {
        _aim.gameObject.SetActive(false);
        _reflectionAim.gameObject.SetActive(false);
    }

    private void SetPositionsLine(LineRenderer aim,Vector3 startPosition,Vector3 finishPosition)
        
    {
        aim.SetPosition(0,startPosition);
        aim.SetPosition(1,finishPosition);
    }
    
}