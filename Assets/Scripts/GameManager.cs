using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    Image titleImage;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InactiveImage", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
