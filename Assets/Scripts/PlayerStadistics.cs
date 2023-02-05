using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStadistics : MonoBehaviour
{

    [SerializeField] private GameObject playerScore;
    [SerializeField] private Transform grid;
    private List<Porcentajes> jugadors;
    public List<TextMeshProUGUI> textMeshProUGUIs;
    public List<Image> Imges;

    private void Start()
    {
        ScorePrint();
    }

    public void ScorePrint()
    {
        jugadors = new List<Porcentajes>();
        for (int i = 0; i < textMeshProUGUIs.Count; i++)
        {
            int value = int.Parse(textMeshProUGUIs[i].text);
            Porcentajes tmpPorcen = new Porcentajes((i + 1), value, Imges[i]);
            jugadors.Add(tmpPorcen);
        }
        jugadors = jugadors.OrderByDescending(Porcentajes => Porcentajes.percentage).ToList();

        for (int i = 0; i < textMeshProUGUIs.Count; i++)
        {
            GameObject tmpObject = Instantiate(playerScore, grid.transform);
            tmpObject.GetComponentInChildren<TextMeshProUGUI>().text = jugadors[i].percentage.ToString();
            tmpObject.GetComponentInChildren<Image>().sprite = jugadors[i].sprite.sprite;
        }

    }

}

[System.Serializable]
public class Porcentajes{
    public int id;
    public int percentage;
    public Image sprite;

    public Porcentajes(int id, int percentage, Image sprite)
    {
        this.id = id;
        this.percentage = percentage;
        this.sprite = sprite;
    }
}
