using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    
    Vector2Int currentTilePosition = new Vector2Int(0,0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 20f; // kich co 1 o map
    GameObject[,] terrainTiles; //toa do o map

    [SerializeField] int terrainTileHorizontalCount; // so o map
    [SerializeField] int terrainTileVerticalCount;  // _________

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidht = 3;
    Transform playerTranformNow;
    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }
    private void Start()
    {
        UpdateTileOnScreen();

        playerTranformNow = GameManager.instance.playerTransform;

    }
    private void Update()
    {  

        playerTilePosition.x = (int)(playerTranformNow.position.x / tileSize);
        playerTilePosition.y = (int)(playerTranformNow.position.y / tileSize);
    

        playerTilePosition.x -= playerTranformNow.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTranformNow.position.y < 0 ? 1 : 0;

        

        if (currentTilePosition != playerTilePosition) // khi nhan vat di chuyen 
        {
            currentTilePosition = playerTilePosition;
            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);

           

            UpdateTileOnScreen();
            
        }
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
        
    private void UpdateTileOnScreen()
    {                                                                                               
        for(int pov_x = -(fieldOfVisionWidht/2); pov_x <= fieldOfVisionWidht/2; pov_x++)
        {
            for(int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionHeight/2; pov_y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

               

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y]; //roll mapp

                Vector3 newPosition = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
                if(newPosition != tile.transform.position)
                {
                    tile.transform.position = newPosition;
                    terrainTiles[tileToUpdate_x, tileToUpdate_y].GetComponent<TerrainTile>().Spawn(); // tao object spawning
                }

            }
        }
               
    }    

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if(horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;

            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if(currentValue >=0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

       
        return (int)currentValue;
    }
}
