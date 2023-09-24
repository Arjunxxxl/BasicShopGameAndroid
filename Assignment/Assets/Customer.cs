using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Customer Data")]
    [SerializeField] private Vector3 customerStartPos;
    [SerializeField] private Vector3 customerEndPos;
    public bool isCustomerRequestSet;

    [Header("Customer Move Data")]
    [SerializeField] private bool moveCustomerIn;
    [SerializeField] private bool moveCustomerOut;
    [SerializeField] private float customerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCustomerIn();
        MoveCustomerOut();
    }

    public void SetUp(Vector3 customerStartPos, Vector3 customerEndPos)
    {
        this.customerStartPos = customerStartPos;
        this.customerEndPos = customerEndPos;

        transform.position = this.customerStartPos;

        moveCustomerIn = true;
        moveCustomerOut = false;

        isCustomerRequestSet = false;

        GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    public void CustomerOrderFullfilled()
    {
        moveCustomerIn = false;
        moveCustomerOut = true;

        GetComponent<Renderer>().material.SetColor("_Color", ColorManager.Instance.GetCurrentColor().color);
    }

    private void MoveCustomerIn()
    {
        if (moveCustomerIn)
        {
            transform.position = Vector3.MoveTowards(transform.position, customerEndPos, Time.deltaTime * customerSpeed);

            if(Vector3.Distance(transform.position, customerEndPos) < 0.1f)
            {
                SetCustomerWantingColor();
                
                isCustomerRequestSet = true;

                moveCustomerIn = false;
            }
        }
    }

    private void MoveCustomerOut()
    {
        if (moveCustomerOut)
        {
            transform.position = Vector3.MoveTowards(transform.position, customerStartPos, Time.deltaTime * customerSpeed);

            if (Vector3.Distance(transform.position, customerStartPos) < 0.1f)
            {
                moveCustomerOut = false;
                CustomerManager.Instance.ActivateCustomer();

                gameObject.SetActive(false);
            }
        }
    }

    private void SetCustomerWantingColor()
    {
        ColorManager.Instance.GetRandomColor();

        UiManager.Instance.SetCurrentColor(ColorManager.Instance.GetCurrentColor());
    }
}
