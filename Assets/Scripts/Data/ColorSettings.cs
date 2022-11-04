using System;
using UnityEngine;

[Serializable]
public struct ColorSettings
{
    [SerializeField] private Color color;
    [SerializeField] private int indexLayerMask;
    
    public Color Color => color;
    public int IndexLayerMask => indexLayerMask;

}
