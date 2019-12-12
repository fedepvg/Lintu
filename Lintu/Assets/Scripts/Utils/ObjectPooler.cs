using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject ObjectToPool;
    public int AmountToPool;
    public GameObject Parent;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<ObjectPoolItem> ItemsToPool;
    public List<GameObject> PooledObjects;

    public void Awake()
    {
        Instance = this;
        PooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in ItemsToPool)
        {
            for (int i = 0; i < item.AmountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                obj.SetActive(false);
                if(item.Parent!=null)
                    obj.transform.parent = item.Parent.transform;
                obj.name = item.ObjectToPool.name + i;
                PooledObjects.Add(obj);
            }
        }
    }

    public void Start()
    {
        //PooledObjects = new List<GameObject>();
        //
        //foreach (ObjectPoolItem item in ItemsToPool)
        //{
        //    for (int i = 0; i < item.AmountToPool; i++)
        //    {
        //        GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
        //        obj.SetActive(false);
        //        obj.transform.parent = item.Parent.transform;
        //        obj.name = item.ObjectToPool.name + i;
        //        PooledObjects.Add(obj);
        //    }
        //}
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == tag)
            {
                PooledObjects[i].SetActive(true);
                return PooledObjects[i];
            }
        }
        return null;
    }
    
    public GameObject GetRandomPooledObject(string tag)
    {
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == tag)
            {
                //PooledObjects[i].SetActive(true);
                //return PooledObjects[i];
                goList.Add(PooledObjects[i]);
            }
        }

        if (goList != null)
        {
            int randomObject = Random.Range(0, goList.Count);
            goList[randomObject].SetActive(true);
            return goList[randomObject];
        }

        return null;
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
