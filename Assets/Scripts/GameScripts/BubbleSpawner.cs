using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class BubbleSpawner
{
    private readonly Transform _spawnPoint;
    private readonly Pool<ThrowableBubble> _bubblePool;
    private  Pool<LevelBubble> _levelBubblePool;
    private List<Vector2> _spawnPositionsBubbles;
    public BubbleSpawner(ThrowableBubble prefab, Transform spawnPoint,Transform throwableBubbleContainer,List<Vector2> spawnPositionsBubbles,LevelBubble levelBubblePrefab,
        Transform levelBubbleContainer, int startCountBubble)
    {
        _spawnPoint = spawnPoint;
        _spawnPositionsBubbles = spawnPositionsBubbles;
        _bubblePool = new Pool<ThrowableBubble>(throwableBubbleContainer, startCountBubble, prefab);
        _levelBubblePool = new Pool<LevelBubble>(levelBubbleContainer, spawnPositionsBubbles.Count, levelBubblePrefab);
    }

    public ThrowableBubble SpawnBubble()
    {
        var bubble = _bubblePool.GetFreeObject();
        bubble.transform.position = _spawnPoint.position;
        return bubble;
    }

    public void SpawnRandomLevel()
    {
        ;
        foreach (var spawnPosition in _spawnPositionsBubbles)
        {
            var bubble = _levelBubblePool.GetFreeObject();
            bubble.transform.position = spawnPosition;
        }
    }

    public GameObject SpawnCreatedLevel(GameObject createdLevel)
    {
        var level =  Object.Instantiate(createdLevel);
        return level;
    }

    public void DisablePoolObjects()
    {
        foreach (var obj in _bubblePool.Objects)
        {
            obj.gameObject.SetActive(false);
        }

        foreach (var obj in _levelBubblePool.Objects)
        {
            obj.gameObject.SetActive(false);
        }
        
    }

}
