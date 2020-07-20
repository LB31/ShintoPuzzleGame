using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField]
    private GameObject waterBlock;
    [SerializeField]
    private GameObject bucketGround;
    [SerializeField]
    private float waterBlockHeight;

    public int maxWater;
    public List<GameObject> waters;
   
    void Start()
    {
        float offsetY = bucketGround.transform.position.y;
        Vector3 waterBlockSize = waterBlock.transform.localScale;
        
        for(int i = 0; i < maxWater; i++)
        {
            GameObject newWaterBlock = Instantiate(waterBlock, new Vector3(waterBlock.transform.position.x, offsetY, waterBlock.transform.position.z), bucketGround.transform.rotation, this.transform);

            newWaterBlock.SetActive(true);
            waters.Add(newWaterBlock);

            offsetY += waterBlockHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
