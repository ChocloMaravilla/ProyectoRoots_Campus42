using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla
{
    public Types types;
    public Owner owner;
}

public enum Types
{
    None, Spawn, Basics,PowerUp,Spike
}
public enum Owner
{
    None, Blue, Red, Grean, Yellow
}
