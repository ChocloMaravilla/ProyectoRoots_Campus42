using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla
{
    public Types type;
    public Values value;
    public Owner owner;
    public bool CanBeSteppedOn()
    {
        switch (type)
        {
            case Types.Basics: return true;
            case Types.PowerUp: return true;
            default: return false;
        }
    }
    public bool CanBePlantedOn()
    {
        switch (type)
        {
            case Types.None: return false;
            case Types.Spawn: return false;
            default: return true;
        }
    }
}

public enum Types
{
    None, Spawn, Basics, PowerUp, Spike
}
public enum Values
{
    Zero, One, Two, Three, MinusOne, MinusTwo, MinusThree, Double
}
public enum Owner
{
    None, Blue, Red, Grean, Yellow
}
