using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecursiveBacktrackerAlgorithm
{

    public static CellState[,] ApplyRecursiveBacktracker(CellState[,] mazeGrid, int width, int length) {

        // Creates instance of Random class for route selection.
        var rng = new System.Random();

        // A stack of visited positions. Can go back in one way since its a stack.
        var visitedStack = new Stack<Position>();

        // A random position within the mazeGrid to start the algorithm.
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, length) };

        // Marks the starting position as visited using bitwise AND operator |=.
        mazeGrid[position.X, position.Y] |= CellState.VISITED;

        // Pushes the position to the visited stack.
        visitedStack.Push(position);


        while (visitedStack.Count > 0) {
            
            // Pops current position out of stack, reporesents current position of algorithm.
            var current = visitedStack.Pop();

            // List<> of type Neighbour to determine next spot.
            var neighbours = GetUnvisitedNeighbours(mazeGrid, current, width, length);

            // If unvisited neighbours found.
            if (neighbours.Count > 0) {

                // pushes current position back to stack BEFORE going to neighbour.
                visitedStack.Push(current);

                // Randomly chooses an available neighbour
                var randomNeighbour = neighbours[rng.Next(0, neighbours.Count)];

                // nPosition = neighbours position
                var nPosition = randomNeighbour.Position;

                // Removes the wall FROM NEIGHBOUR that touches CURRENT cell. Then removes the opposing wall FROM CURRENT using GetOppositeWall();
                mazeGrid[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                mazeGrid[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

                // Marks the neighbour cell as visited.
                mazeGrid[nPosition.X, nPosition.Y] |= CellState.VISITED;

                // Places neighbour position on top of stack. Next loop it is popped and used as "current".
                visitedStack.Push(nPosition);
            }
        }

        // Returns the given mazeGrid of type CellState[,] after algorithm is applied.
        return mazeGrid;
    }

    private static CellState GetOppositeWall(CellState cell) {

        // This is used to find out which other wall of a neighbouring cell should be removed. Example: Cell[0,0] RIGHT wall removed means CELL[1,0] LEFT wall should be removed too.
        switch (cell) {
            case CellState.RIGHT_WALL: return CellState.LEFT_WALL;
            case CellState.LEFT_WALL: return CellState.RIGHT_WALL;
            case CellState.UP_WALL: return CellState.DOWN_WALL;
            case CellState.DOWN_WALL: return CellState.UP_WALL;
            // Default will never happen.
            default: return CellState.DOWN_WALL;
        }

        
    }

    private static List<Neighbour> GetUnvisitedNeighbours(CellState[,] mazeGrid, Position pos, int width, int length) {
        
        // List of Neighbour structs to fill with unvisited Neighbour structs.
        var unvisitedList = new List<Neighbour>();

        // UP - Checks the northern cell of current position for VISITED flag.
        if (pos.Y < length - 1) {
            if (!mazeGrid[pos.X, pos.Y + 1].HasFlag(CellState.VISITED)) {
                unvisitedList.Add(new Neighbour {
                    Position = new Position {
                        X = pos.X,
                        Y = pos.Y + 1
                    },
                    SharedWall = CellState.UP_WALL
                });
            }
        }

        // RIGHT - Checks the eastern cell of current position for VISITED flag.
        if (pos.X < width - 1) {
            if (!mazeGrid[pos.X + 1, pos.Y].HasFlag(CellState.VISITED)) {
                unvisitedList.Add(new Neighbour {
                    Position = new Position {
                        X = pos.X + 1,
                        Y = pos.Y
                    },
                    SharedWall = CellState.RIGHT_WALL
                }); ;
            }
        }

        // Down - Checks the southern cell of current position for VISITED flag.
        if (pos.Y > 0) {
            if (!mazeGrid[pos.X, pos.Y - 1].HasFlag(CellState.VISITED)) {
                unvisitedList.Add(new Neighbour {
                    Position = new Position {
                        X = pos.X,
                        Y = pos.Y - 1
                    },
                    SharedWall = CellState.DOWN_WALL
                });
            }
        }

        // Left - Checks the western cell of current position for VISITED flag.
        if (pos.X > 0) {
            if (!mazeGrid[pos.X - 1, pos.Y].HasFlag(CellState.VISITED)) {
                unvisitedList.Add(new Neighbour {
                    Position = new Position {
                        X = pos.X - 1,
                        Y = pos.Y
                    },
                    SharedWall = CellState.LEFT_WALL
                });
            }
        }
        
        // Returns a list of neighbours along with their position and shared walls for algorithm to visit. Empty list signifies a dead end.
        return unvisitedList;
    }
}
