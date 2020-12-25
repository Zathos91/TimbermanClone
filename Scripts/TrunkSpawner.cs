using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkSpawner : MonoBehaviour
{
    public static TrunkSpawner trunkSpawner;

    [SerializeField]
    float normalTrunkChance, leftTrunkChance, rightTrunkChance;

    [SerializeField]
    GameObject trunkPrefab, trunkLeftPrefab, trunkRightPrefab;

    enum TrunkType
    {
        Trunk,
        TrunkLeft,
        TrunkRight
    };

    List<GameObject> trunkList;

    TrunkType lastSpawnedTrunk;

    [SerializeField]
    int listSize = 5;

    Vector2 lastSpawnPosition;

    private void Start()
    {
        trunkSpawner = this;
        trunkList = new List<GameObject>();
        lastSpawnPosition = transform.position;
        SpawnNewList();
    }

    public void SpawnNewList()
    {
        if (trunkList.Count > 0)
        {
            foreach (GameObject trunk in trunkList)
            {
                Destroy(trunk);
                trunkList = new List<GameObject>();
                lastSpawnPosition = transform.position;
            }
        }
        for (int i = 0; i < listSize; i++)
        {
            SpawnNewTrunk();
        }
        UpdateSortingLayers();
    }

    TrunkType CalculateChance()
    {
        float random = Random.Range(0f, 100f);
        if (random < normalTrunkChance)
        {
            return TrunkType.Trunk;
        }
        else if (random > normalTrunkChance && random < leftTrunkChance)
        {
            return TrunkType.TrunkLeft;
        }

        return TrunkType.TrunkRight;
    }

    void SpawnNewTrunk()
    {
        GameObject newTrunk;
        TrunkType trunkType = CalculateChance();
        if ((trunkType == TrunkType.TrunkLeft || trunkType == TrunkType.TrunkRight) && (lastSpawnedTrunk == TrunkType.TrunkLeft || lastSpawnedTrunk == TrunkType.TrunkRight))
        {
            trunkType = TrunkType.Trunk;
        }
        if (trunkList.Count > 0)
        {
            lastSpawnPosition = trunkList[trunkList.Count - 1].transform.position + Vector3.up * 1.72f;
        }
        else
        {
            trunkType = TrunkType.Trunk;
        }

        lastSpawnedTrunk = trunkType;
        switch (trunkType)
        {
            case TrunkType.Trunk:
                newTrunk = GameObject.Instantiate(trunkPrefab, lastSpawnPosition, Quaternion.identity, transform);
                break;
            case TrunkType.TrunkLeft:
                newTrunk = GameObject.Instantiate(trunkLeftPrefab, lastSpawnPosition, Quaternion.identity, transform);
                break;
            case TrunkType.TrunkRight:
                newTrunk = GameObject.Instantiate(trunkRightPrefab, lastSpawnPosition, Quaternion.identity, transform);
                break;
            default:
                newTrunk = GameObject.Instantiate(trunkPrefab, lastSpawnPosition, Quaternion.identity, transform);
                break;
        }
        trunkList.Add(newTrunk);

    }

    public void RemoveNextTrunk()
    {
        Rigidbody2D body = trunkList[0].GetComponent<Rigidbody2D>();
        trunkList[0].GetComponent<Collider2D>().enabled = false;
        body.isKinematic = false;
        body.AddForce(new Vector2(1,1) * 15f,ForceMode2D.Impulse);
        Destroy(trunkList[0],2f);
        trunkList.RemoveAt(0);

        foreach (GameObject trunk in trunkList)
        {
            trunk.transform.position = new Vector3(trunk.transform.position.x, trunk.transform.position.y - 1.72f, trunk.transform.position.z);
        }
        SpawnNewTrunk();
        UpdateSortingLayers();
    }

    public bool CheckHitLeft()
    {
        if(trunkList[0].tag == "Obstacle" && trunkList[0].transform.localScale.x == 1)
        {
            return true;
        }
        return false;
    }

    public bool CheckHitRight()
    {
        if (trunkList[0].tag == "Obstacle" && trunkList[0].transform.localScale.x == -1)
        {
            return true;
        }
        return false;
    }

    void UpdateSortingLayers()
    {
        for (int i = 0; i < trunkList.Count - 1; i++)
        {
            trunkList[i].GetComponent<SpriteRenderer>().sortingOrder = i + 1;
        }
    }

}
