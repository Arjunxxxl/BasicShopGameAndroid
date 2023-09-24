using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Data")]
    [SerializeField] private Transform customerStartPos;
    [SerializeField] private Transform customerEndPos;
    [SerializeField] private Customer customer;
    public Customer Customer {  get { return customer; } }

    #region Singleton
    public static CustomerManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ActivateCustomer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateCustomer()
    {
        GameObject obj = ObjectPooler.Instance.SpawnFormPool("customer", customerStartPos.position, Quaternion.identity);
        customer = obj.GetComponent<Customer>();
        customer.SetUp(customerStartPos.position, customerEndPos.position);
    }
}
