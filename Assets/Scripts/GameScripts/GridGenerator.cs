using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridGenerator 
{
    [SerializeField]private  byte width;
    [SerializeField]private  byte height;
    [SerializeField]private  Vector2 startPositionGrid;
    [SerializeField]private  Vector2 spaceCell;


    public List<Vector2> Generate()
    {
        var cellPositions = new List<Vector2>();
        
        for (byte i = 0; i < width; i++)
        {
            for (byte j = 0; j < height; j++)
            {
                cellPositions.Add(new Vector2((i + startPositionGrid.x)* spaceCell.x, (j + startPositionGrid.y)* spaceCell.y));
            }
        }
        return cellPositions;
    }
    
}
