using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerStadistics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text1, text2, text3, text4;
    private List<Porcentajes> jugadors;
    public List<TextMeshProUGUI> textMeshProUGUIs;
    private List<Porcentajes> porcentajesOrdes;

    void Update()
    {
        jugadors = new List<Porcentajes>();
        for (int i = 0; i < textMeshProUGUIs.Count; i++)
        {

            Porcentajes tmpPorcen = new Porcentajes((i + 1), int.Parse(textMeshProUGUIs[i].text));
            jugadors.Add(tmpPorcen);
        }

        porcentajesOrdes = jugadors.OrderByDescending(Porcentajes => Porcentajes.percentage).ToList();
        text1.SetText(porcentajesOrdes[0].percentage.ToString());
        text2.SetText(porcentajesOrdes[1].percentage.ToString());
        text3.SetText(porcentajesOrdes[2].percentage.ToString());
        text4.SetText(porcentajesOrdes[3].percentage.ToString());

    }

}

[System.Serializable]
public class Porcentajes{
    public int id;
    public int percentage;

    public Porcentajes(int id, int percentage)
    {
        this.id = id;
        this.percentage = percentage;
    }
}
