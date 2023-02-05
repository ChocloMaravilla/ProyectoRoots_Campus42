using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entities
{
    int turnCountdown;
    Direction lastValidDir = Direction.none;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePadre();
        if (direction == Direction.none) { Move(); }
    }
    public override void UpdatePadre()
    {
        base.UpdatePadre();
    }
    public override void Move()
    {
        base.Move();
        if (turnCountdown == 0 || direction == Direction.none)
        {
            turnCountdown = Random.Range(2, 5);
            Direction[] checkDirs = new Direction[] { Direction.up, Direction.down, Direction.left, Direction.right };
            List<Direction> validDirs = new List<Direction>();
            foreach (Direction checkDir in checkDirs)
            {
                if (IsValidDir(checkDir))
                {
                    validDirs.Add(checkDir);
                }
            }
            switch (lastValidDir)
            {
                case Direction.up:
                    while (validDirs.Contains(Direction.down)) { validDirs.Remove(Direction.down); }
                    break;
                case Direction.down:
                    while (validDirs.Contains(Direction.up)) { validDirs.Remove(Direction.up); }
                    break;
                case Direction.left:
                    while (validDirs.Contains(Direction.right)) { validDirs.Remove(Direction.right); }
                    break;
                case Direction.right:
                    while (validDirs.Contains(Direction.left)) { validDirs.Remove(Direction.left); }
                    break;
            }
            direction = validDirs[Random.Range(0, validDirs.Count)];
            lastValidDir = direction;
        }
        else { turnCountdown--; }
    }
}