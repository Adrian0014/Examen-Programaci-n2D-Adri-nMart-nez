using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int coins = 0;

    [SerializeField]Text coinsText;
    
 
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

   public void LoadLevel(int sceneIndex)
   {
        SceneManager.LoadScene(sceneIndex);
   }

   public void AddCoin(GameObject moneda)
   {
        coins++;
        coinsText.text = coins.ToString();
        Destroy(moneda);
   }

  
    
}
