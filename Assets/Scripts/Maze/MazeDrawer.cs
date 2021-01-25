using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDrawer : MonoBehaviour
{

    // Reference to the MazeData scriptable object which holds all maze data.
    [SerializeField] public MazeData data;

    // Local variables that are set to values out of the scriptable object.
    private GameObject horizontalWall, verticalWall, floorPrefab, startPlatform, endPlatform;
    private int width, length;
    private float size;


    // Since the data of the maze is stored in a scriptable object, there's a function to get all of it before generating.
    private void GetSettings() {
        horizontalWall = data.horizontalWall;
        verticalWall = data.verticalWall;
        floorPrefab = data.floor;
        endPlatform = data.endPlatform;
        startPlatform = data.startPlatform;
        width = data.width;
        length = data.length;
        size = data.size;
    }


    public void GenerateMaze() {

        // Destroys old maze, if it exists.
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Updates settings variables incase there were changes
        GetSettings();

        // Generates maze grid and applies algorithm.
        var mazeGrid = GenerateGrid(width, length);

        // Instantiates walls and floor based on maze grid.
        DrawMaze(mazeGrid);
    }


    private CellState[,] GenerateGrid(int width, int length) {

        // Creates a new multidimensional array of type CellState as canvas for maze.
        CellState[,] mazeGrid = new CellState[width, length];

        // Defines initial CellState for each maze cell with all 4 walls up.
        CellState initial = CellState.UP_WALL | CellState.RIGHT_WALL | CellState.DOWN_WALL | CellState.LEFT_WALL;

        // Loops through all columns and rows to set each value to initial CellState (All 4 walls up). Returns a CellState[,] with each cell having four walls.
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                mazeGrid[i, j] = initial;
            }
        }

        // Returns the mazeGrid after it's been changed with a chosen algorithm.
        return RecursiveBacktrackerAlgorithm.ApplyRecursiveBacktracker(mazeGrid, width, length);
    }


    private void DrawMaze(CellState[,] mazeGrid) {

        // Instantiates the floor and adjusts its size and position along with its BoxCollider.
        var floor = Instantiate(floorPrefab, transform);
        var floorSize = new Vector3((width * size + width/2), 0.1f, (length * size + length/2));
        var floorCollider = floor.GetComponent<BoxCollider>();

        floor.transform.localScale = floorSize;
        floorCollider.transform.localScale = floorSize;
        floor.transform.position = new Vector3(-width / 2 + (width/2) * size, -1f, -length / 2 + (length/2) * size);


        // Random place along a row for start and end positions.
        var startPosX = Random.Range(0, width);
        var endPosX = Random.Range(0, width);

        // Checks each cell for the wall flags and instantiates the wallPrefab where needed.
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {

                // cell represents an individual item in the array. It has type CellState and can have up to 4 wall flags.
                var cell = mazeGrid[i, j];

                // This is the cell position calculated from the bottom left to the top right.
                var position = new Vector3(-width / 2 + i * size, 0, -length / 2 + j * size);

                // Upper wall is always generated if the flag is found.
                if (cell.HasFlag(CellState.UP_WALL)) {
                    var topWall = Instantiate(horizontalWall, transform);
                    topWall.transform.position = position + new Vector3(0, 0, size / 2);
                    topWall.transform.localScale = new Vector3(size, topWall.transform.localScale.y, topWall.transform.localScale.z);
                    topWall.name = i + "," + j + "TopWall";
                }

                // Left wall is always generated if the flag is found.
                if (cell.HasFlag(CellState.LEFT_WALL)) {
                    var leftWall = Instantiate(verticalWall, transform);
                    leftWall.transform.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.transform.localScale = new Vector3(size, leftWall.transform.localScale.y, leftWall.transform.localScale.z);
                    leftWall.name = i + "," + j + "LeftWall";
                }

                // Right wall is only generated at the right side of the maze. In other cells, an adjacent left wall is sufficient.
                if (i == width - 1) {
                    if (cell.HasFlag(CellState.RIGHT_WALL)) {
                        var rightWall = Instantiate(verticalWall, transform);
                        rightWall.transform.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.transform.localScale = new Vector3(size, rightWall.transform.localScale.y, rightWall.transform.localScale.z);
                        rightWall.name = i + "," + j + "RightWall";
                    }
                }

                // Down wall is only generated at the bottom side of the maze. In other cells, an adjacent upper wall is sufficient.
                if (j == 0) {
                    if (cell.HasFlag(CellState.DOWN_WALL)) {
                        var downWall = Instantiate(horizontalWall, transform);
                        downWall.transform.position = position + new Vector3(0, 0, -size / 2);
                        downWall.transform.localScale = new Vector3(size, downWall.transform.localScale.y, downWall.transform.localScale.z);
                        downWall.name = i + "," + j + "downWall";
                    }
                }

                // Draws a green square at a random place on the first row as starting point.
                if (i == startPosX && j == 0) {
                    var startPos = Instantiate(startPlatform, transform);
                    startPos.transform.position = position;
                    startPos.transform.localScale = new Vector3(startPos.transform.localScale.x * size, startPos.transform.localScale.y * size, startPos.transform.localScale.z * size);
                }

                // Draws a red aquare a t a random place on the last row as a end point.
                if (i == endPosX && j == length - 1) {
                    var endPos = Instantiate(endPlatform, transform);
                    endPos.transform.position = position;
                    endPos.transform.localScale = new Vector3(endPos.transform.localScale.x * size, endPos.transform.localScale.y * size, endPos.transform.localScale.z * size);

                }
            }
        }
    }
}
