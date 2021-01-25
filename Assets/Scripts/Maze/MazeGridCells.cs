using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]  // Bitmask enum used for each individual cell. Starting value is 0000 1111 as all 4 walls are up and its not visited.
public enum CellState
{
    UP_WALL = (1 << 0),     //0001
    RIGHT_WALL = (1 << 1),  //0010
    DOWN_WALL = (1 << 2),   //0100
    LEFT_WALL = (1 << 3),   //1000

    VISITED = (1 << 4)      //0001 0000
}

// Used to keep track of current and neighbour cells.
public struct Position
{
    public int X;
    public int Y;
}

// Used to discover where the algorithm can go next.
public struct Neighbour
{
    public Position Position;
    public CellState SharedWall;
}
