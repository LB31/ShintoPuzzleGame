using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class Bucket : MonoBehaviour
{
    [SerializeField]
    private GameObject waterBlock;
    [SerializeField]
    private GameObject bucketGround;
    [SerializeField]
    private float waterBlockHeight;
    [SerializeField]
    private int startWaterAmount;

    private float offsetY;
    public int maxWaterAmount;
    public Stack<GameObject> waters = new Stack<GameObject>();
   
    void Start()
    { 
        offsetY = bucketGround.transform.position.y;
        
        for(int i = 0; i < startWaterAmount; i++)
        {
            GameObject newWaterBlock = Instantiate(waterBlock, new Vector3(waterBlock.transform.position.x, offsetY, waterBlock.transform.position.z), waterBlock.transform.rotation, this.transform);


            newWaterBlock.SetActive(true);
            newWaterBlock.transform.position = new Vector3(waterBlock.transform.position.x, offsetY, waterBlock.transform.position.z);
            Vector3 tempVec = newWaterBlock.transform.localScale;
            newWaterBlock.transform.localScale = new Vector3(tempVec.x * ((i * 0.025f) + 1), tempVec.y, tempVec.z);

            waters.Push(newWaterBlock);

            offsetY += waterBlockHeight;
        }
    }

    public int GetWaterAmount()
    {
        return waters.Count;
    }

    public bool IsFull()
    {
        return waters.Count == maxWaterAmount;
    }

    public bool IsEmpty()
    {
        return waters.Count == 0;
    }

    //remove water
    public void PopWater()
    {
        Destroy(waters.Pop());
        offsetY -= waterBlockHeight;
    }

    //add water
    public void PushWater(float temp)
    {
        if (IsFull())
        {
            throw new Exception("Bucket is already full");
        }
 
        GameObject newWaterBlock = Instantiate(waterBlock, new Vector3(waterBlock.transform.position.x, offsetY, waterBlock.transform.position.z), waterBlock.transform.rotation, this.transform);

        Vector3 tempVec = newWaterBlock.transform.localScale;

        newWaterBlock.transform.localScale = new Vector3(tempVec.x * temp, tempVec.y, tempVec.z);
        newWaterBlock.SetActive(true);
        waters.Push(newWaterBlock);
        offsetY += waterBlockHeight;
    }

    public void Activated(bool isActivated)
    {
       this.transform.Find("PointLight").gameObject.SetActive(isActivated);
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
