using System.Collections.Generic;
using UnityEngine;

public class CheckBubble
{
    private List<GameObject> CheckNeighboringBubbles(Vector2 position, Vector2[] checkDirections, float rayDistance, int layerMask)
    {
        var hitBubbles = new List<GameObject>();
        var allHits = new List<RaycastHit2D>();
        var hits2D = CheckMatch(position, checkDirections, rayDistance,layerMask);
        allHits.AddRange(hits2D);

        while (hits2D.Count != 0)
        {
            var newHits =  new List<RaycastHit2D>();
            newHits.AddRange(hits2D);
            hits2D.Clear();
            foreach (var hit in newHits)
            {
                position = hit.collider.transform.position;
                hits2D.AddRange(CheckMatch(position, checkDirections, rayDistance, layerMask));
                allHits.AddRange(hits2D);
            }
        }
        foreach (var hit in allHits)
        {
           hitBubbles.Add(hit.collider.gameObject);
        }
        return hitBubbles;
    }

    public void FindMatch(GameObject bubble,Vector2 position, Vector2[] checkDirections, float rayDistance, int layerMask)
    {
        var hitBubbles = CheckNeighboringBubbles(position, checkDirections, rayDistance, layerMask);
        if (hitBubbles.Count>2)
        {
            foreach (var hitBubble in hitBubbles)
            {
                hitBubble.SetActive(false);
            }
            bubble.SetActive(false);
        }
        else
        {
            foreach (var hitBubble in hitBubbles)
            {
                hitBubble.GetComponent<CircleCollider2D>().enabled = true;
            }
           
            var bubbleRigidbody = bubble.GetComponent<Rigidbody2D>();
            bubbleRigidbody.simulated = true;
            bubbleRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private RaycastHit2D CheckRaycast(Vector2 position, Vector2 direction,float distance)
    {
        var hit2D = Physics2D.Raycast(position, direction,distance);
        Debug.DrawRay(position, direction, Color.white, 0.5f, false);
        return hit2D;
    }

    private List<RaycastHit2D> CheckMatch(Vector2 position,Vector2[] directions,float distance,int layerMask)
    {
        var hits = new List<RaycastHit2D>();
        
        foreach (var direction in directions)
        {
            var hit2D = CheckRaycast(position, direction, distance);
            if (hit2D.collider!=null&&layerMask == hit2D.collider.gameObject.layer)
            {
                hit2D.collider.enabled = false;
                hits.Add(hit2D);
            }
          
        }

        return hits;
    }
}