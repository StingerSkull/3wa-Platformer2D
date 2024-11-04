using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public StateMachineV3 player;
    public Sprite heartEmpty;
    public Sprite heartFull;
    public GameObject heartPrefab;
    public List<GameObject> hearts = new ();

    // Start is called before the first frame update
    void Start()
    {
        GenerateHeart(player.maxPlayerLife);
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<Image>().sprite = heartFull;
        }
    }

    public void GenerateHeart(int maxLife)
    {
        for (int i = 0; i < maxLife; i++)
        {
            hearts.Add(Instantiate(heartPrefab,transform));
        }
    }
    public void RemoveHeart()
    {
        hearts[player.playerLife].GetComponent<Image>().sprite = heartEmpty;
    }

    public void ResetHearts()
    {
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<Image>().sprite = heartFull;
        }
    }
}
