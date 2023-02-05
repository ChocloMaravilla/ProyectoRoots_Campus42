using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColors : MonoBehaviour
{
    public static Color GetColor(Owner owner)
    {
        switch (owner)
        {
            case Owner.Blue: return Color.HSVToRGB(195 / 360f, 0.75f, 1);
            case Owner.Red: return Color.HSVToRGB(35 / 360f, 0.75f, 1);
            case Owner.Grean: return Color.HSVToRGB(315 / 360f, 0.6f, 1);
            case Owner.Yellow: return Color.HSVToRGB(285 / 360f, 0.75f, 1);
            default: return Color.gray;
        }
    }
}