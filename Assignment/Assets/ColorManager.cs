using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [System.Serializable]
    public class ColorData
    {
        public string name;
        public Color color;
    }

    #region Singleton
    public static ColorManager Instance;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private ColorData currentActiveColor;

    [Space]
    [SerializeField] private List<ColorData> colorDatas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ColorData GetRandomColor()
    {
        currentActiveColor = colorDatas[Random.Range(0, colorDatas.Count)];
        return currentActiveColor;
    }

    public ColorData GetCurrentColor()
    {
        if(currentActiveColor == null)
        {
            GetRandomColor();
        }
        return currentActiveColor;
    }
}
