using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla
{
    public Types types;
    public Bonus bonus;
    public PowerUp powerUp;
    public Owner owner;
}

public enum Types
{
    None, Spawn, Basics
}

public enum Bonus
{
    None, x2
}

public enum PowerUp
{
    None, x2Speed
}

public enum Owner
{
    None, Blue, Red, Grean, Yellow
}
