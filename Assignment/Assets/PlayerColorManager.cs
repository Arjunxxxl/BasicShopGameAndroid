using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static PlayerColorManager;

public class PlayerColorManager : MonoBehaviour
{
    public class RackColorData
    {
        public ColorManager.ColorData colorData;
        public GameObject spawnedRack;
    }

    [Header("Rack Data")]
    [SerializeField] private GameObject rackParent;
    [SerializeField] private Vector3 rackOffset;
    [SerializeField] private List<RackColorData> rackColorDatas;

    [Header("Rack Stacking Data")]
    [SerializeField] private float rackStackingDelay;
    [SerializeField] private float rackStackingTimeElapsed;
    [SerializeField] private bool isRackStackingActive;

    // Start is called before the first frame update
    void Start()
    {
        rackColorDatas = new List<RackColorData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRackStackingActive)
        {
            rackStackingTimeElapsed += Time.deltaTime;

            if(rackStackingTimeElapsed > rackStackingDelay)
            {
                rackStackingTimeElapsed = 0;
                StackRack();

                if(rackColorDatas.Count > 10)
                {
                    //isRackStackingActive = false;
                }
            }
        }
    }

    private void StackRack()
    {
        GameObject obj = ObjectPooler.Instance.SpawnFormPool("rack", rackParent.transform.position, Quaternion.identity);

        obj.transform.SetParent(rackParent.transform);
        obj.transform.localPosition += rackOffset * rackColorDatas.Count;

        obj.transform.rotation = rackParent.transform.rotation;

        obj.GetComponent<Renderer>().material.SetColor("_Color", ColorManager.Instance.GetCurrentColor().color);

        RackColorData rackColorData = new RackColorData();
        rackColorData.colorData = ColorManager.Instance.GetCurrentColor();
        rackColorData.spawnedRack = obj;

        rackColorDatas.Add(rackColorData);
    }

    public void RemoveRack()
    {
        rackColorDatas[rackColorDatas.Count -1].spawnedRack.SetActive(false);
        rackColorDatas.RemoveAt(rackColorDatas.Count -1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopArea"))
        {
            rackStackingTimeElapsed = 0;
            isRackStackingActive = true;
        }
        else if(other.CompareTag("CustomerFullfillmentArea"))
        {
            if(rackColorDatas.Count > 0 && CustomerManager.Instance.Customer != null && CustomerManager.Instance.Customer.isCustomerRequestSet)
            {
                bool rackHaveRequestedColor = false;

                for (int i = 0; i < rackColorDatas.Count; i++)
                {
                    if (rackColorDatas[i].colorData.color == ColorManager.Instance.GetCurrentColor().color)
                    {
                        rackHaveRequestedColor = true;
                    }
                }

                if (rackHaveRequestedColor)
                {
                    CustomerManager.Instance.Customer.CustomerOrderFullfilled();
                    RemoveRack();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ShopArea"))
        {
            rackStackingTimeElapsed = 0;
            isRackStackingActive = false;
        }
    }
}
