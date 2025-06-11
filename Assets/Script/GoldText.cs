using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    public TextMeshProUGUI CountGoldText;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = new GameManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
