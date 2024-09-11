using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    public int width = 21;
    public GameObject wallPrefab; 
    public GameObject floorPrefab;
    public int tileSize = 2;
    private System.Random random;
    void Start()
    {
        random = new System.Random();
        DrawMaze();
    }

    void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < width; z++)
            {
                Vector3 floorPosition = new Vector3(x * tileSize, -0.5f, z * tileSize);
                Instantiate(floorPrefab, floorPosition, Quaternion.identity, transform);
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < width; z++)
            {
                if (x == 0 || x == width - 1 || z == 0 || z == width - 1)
                {
                    Vector3 wallPosition = new Vector3(x * tileSize, 0, z * tileSize);
                    Quaternion wallRotation = Quaternion.identity;

                    if (x == 0 && z == 0)
                    {
                        Instantiate(wallPrefab, wallPosition + new Vector3(-tileSize / 2.0f, 0, 0), Quaternion.identity, transform);
                        Instantiate(wallPrefab, wallPosition + new Vector3(0, 0, -tileSize / 2.0f), Quaternion.Euler(0, 90, 0), transform);
                    }
                    else if (x == 0 && z == width - 1)
                    {
                        Instantiate(wallPrefab, wallPosition + new Vector3(-tileSize / 2.0f, 0, 0), Quaternion.identity, transform);
                        Instantiate(wallPrefab, wallPosition + new Vector3(0, 0, tileSize / 2.0f), Quaternion.Euler(0, 90, 0), transform);
                    }
                    else if (x == width - 1 && z == 0)
                    {
                        Instantiate(wallPrefab, wallPosition + new Vector3(tileSize / 2.0f, 0, 0), Quaternion.identity, transform);
                        Instantiate(wallPrefab, wallPosition + new Vector3(0, 0, -tileSize / 2.0f), Quaternion.Euler(0, 90, 0), transform);
                    }
                    else if (x == width - 1 && z == width - 1)
                    {
                        Instantiate(wallPrefab, wallPosition + new Vector3(tileSize / 2.0f, 0, 0), Quaternion.identity, transform);
                        Instantiate(wallPrefab, wallPosition + new Vector3(0, 0, tileSize / 2.0f), Quaternion.Euler(0, 90, 0), transform);
                    }
                    else
                    {
                        if (x == 0)
                        {
                            wallPosition.x -= tileSize / 2.0f;
                            wallRotation = Quaternion.identity;
                        }
                        else if (x == width - 1)
                        {
                            wallPosition.x += tileSize / 2.0f;
                            wallRotation = Quaternion.identity;
                        }
                        else if (z == 0)
                        {
                            wallPosition.z -= tileSize / 2.0f;
                            wallRotation = Quaternion.Euler(0, 90, 0);
                        }
                        else if (z == width - 1)
                        {
                            wallPosition.z += tileSize / 2.0f;
                            wallRotation = Quaternion.Euler(0, 90, 0);
                        }

                        Instantiate(wallPrefab, wallPosition, wallRotation, transform);
                    }
                }
            }
        }

        for (int x = 0; x < width; x++ )
        {
            int door = random.Next(0, 10);
            for (int z = 0; z < width; z++)
            {
                if (z != door)
                {
                    Vector3 wallPosition = new Vector3(x * tileSize, 0, z * tileSize);
                    Instantiate(wallPrefab, wallPosition, Quaternion.identity, transform);
                }

            }
        }
    }

}
