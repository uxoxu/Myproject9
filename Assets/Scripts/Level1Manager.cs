using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    public GameObject mainImage;//�摜������GameObject
    public GameObject panel;//�p�l��
    public GameObject restartButton;//RESTRAT�{�^��

    Image titleImage;

  

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InactiveImage", 2.0f);
        panel.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameover")
        {
            panel.SetActive(true);
        }
        else if(PlayerController.gameState == "playing")
        {
            //�Q�[����
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    
}

