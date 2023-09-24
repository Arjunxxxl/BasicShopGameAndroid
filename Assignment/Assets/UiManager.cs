using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    #region Singleton
    public static UiManager Instance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentColor(ColorManager.ColorData colorData)
    {
        image.color = colorData.color;
        text.color = colorData.color;
        text.text = colorData.name;
    }
}
