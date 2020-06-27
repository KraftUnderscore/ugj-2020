using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levels;

    private int[,] levelGrid;//-1 empty, 0 road, 1 p1 start, 2 p1 end, 3 p2 start, 4 p2 end
    private Vector2Int player1Pos;
    private Vector2Int player2Pos;

    [HideInInspector]
    public Vector2 player1StartPos;
    [HideInInspector]
    public Vector2 player2StartPos;

    private void Awake()
    {
        LoadLevel(levels[0]);
    }

    public void SelectLevel(int level)
    {
        LoadLevel(levels[level]);
    }

    private void LoadLevel(GameObject level)
    {
        player1StartPos = level.transform.GetChild(1).position;
        player2StartPos = level.transform.GetChild(2).position;

        Tilemap tilemap = level.transform.GetChild(0).GetComponent<Tilemap>();
        tilemap.CompressBounds();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        levelGrid = new int[bounds.size.x, bounds.size.y];
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    switch (tile.name)
                    {
                        case "startUp": //player2 start
                            levelGrid[x, y] = 3;
                            player2Pos = new Vector2Int(x, y);
                            break;
                        case "startDown":   //player1 start
                            levelGrid[x, y] = 1;
                            player1Pos = new Vector2Int(x, y);
                            break;
                        case "tile_105":    //player2 end
                            levelGrid[x, y] = 4;
                            break;
                        case "tile_105v2":  //player1 end
                            levelGrid[x, y] = 2;
                            break;
                        default:
                            levelGrid[x, y] = 0;
                            break;
                    }
                }
                else
                {
                    levelGrid[x, y] = -1;
                }
            }
        }
        level.SetActive(true);
    }

    public bool[] Move(int direction)   //0 up, 1 right, 2 down, 3 left
    {
        bool canPlayer1 = false;
        bool canPlayer2 = false;
        if(IsGameOver(direction))
        {
            //call game over from game manager
            Debug.Log("PRZEGRANA!!!");
            canPlayer1 = true;
        }
        else
        {
            switch (direction)
            {
                case 0:
                    if (player2Pos.y - 1 >= 0)
                        if (levelGrid[player2Pos.x, player2Pos.y - 1] != -1)
                        {
                            canPlayer2 = true;
                            player2Pos.Set(player2Pos.x, player2Pos.y - 1);
                        }
                    if (player1Pos.y + 1 < levelGrid.GetLength(1))
                        if (levelGrid[player1Pos.x, player1Pos.y + 1] != -1)
                        {
                            canPlayer1 = true;
                            player1Pos.Set(player1Pos.x, player1Pos.y + 1);
                        }
                    break;
                case 1:
                    if (player1Pos.x + 1 < levelGrid.GetLength(0))
                        if (levelGrid[player1Pos.x + 1, player1Pos.y] != -1)
                        {
                            canPlayer1 = true;
                            player1Pos.Set(player1Pos.x + 1, player1Pos.y);
                        }
                    if (player2Pos.x - 1 >= 0)
                        if (levelGrid[player2Pos.x - 1, player2Pos.y] != -1)
                        {
                            canPlayer2 = true;
                            player2Pos.Set(player2Pos.x - 1, player2Pos.y);
                        }
                    break;
                case 2:
                    if (player2Pos.y + 1 < levelGrid.GetLength(1))
                        if (levelGrid[player2Pos.x, player2Pos.y + 1] != -1)
                        {
                            canPlayer2 = true;
                            player2Pos.Set(player2Pos.x, player2Pos.y + 1);
                        }
                    if (player1Pos.y - 1 >= 0)
                        if (levelGrid[player1Pos.x, player1Pos.y - 1] != -1)
                        {
                            canPlayer1 = true;
                            player1Pos.Set(player1Pos.x, player1Pos.y - 1);

                        }
                        break;
                case 3:
                    if (player1Pos.x - 1 >= 0)
                        if (levelGrid[player1Pos.x - 1, player1Pos.y] != -1)
                        {
                            canPlayer1 = true;
                            player1Pos.Set(player1Pos.x - 1, player1Pos.y);
                        }
                    if (player2Pos.x + 1 < levelGrid.GetLength(0))
                        if (levelGrid[player2Pos.x + 1, player2Pos.y] != -1)
                        {
                            canPlayer2 = true;
                            player2Pos.Set(player2Pos.x + 1, player2Pos.y);
                        }
                    break;
            }
        }
        if (IsVictory())
        {
            //tell game manager
            Debug.Log("WYGRANA ELO");
        }
        bool[] output = { canPlayer1, canPlayer2 };
        return output;
    }

    private bool IsVictory()
    {
        return levelGrid[player1Pos.x, player1Pos.y] == 2 && levelGrid[player2Pos.x, player2Pos.y] == 4;
    }

    private bool IsGameOver(int direction)
    {
        if (player1Pos.x == player2Pos.x && player1Pos.y == player2Pos.y) return true;
        switch(direction)
        {
            case 0:
                if (player1Pos.y - player2Pos.y == -1 && player1Pos.x == player2Pos.x) return true;
                break;
            case 1:
                if (player1Pos.x - player2Pos.x == -1 && player1Pos.y == player2Pos.y) return true;
                break;
            case 2:
                if (player1Pos.y - player2Pos.y == 1 && player1Pos.x == player2Pos.x) return true;
                break;
            case 3:
                if (player1Pos.x - player2Pos.x == 1 && player1Pos.y == player2Pos.y) return true;
                break;
        }

        return false;
    }
}
