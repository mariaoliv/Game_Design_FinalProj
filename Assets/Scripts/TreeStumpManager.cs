using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TreeStumpManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile treeStumpTile;
    public Tile[] treeTile;

    public Tilemap houseTilemap;
    public Tilemap waterTilemap;

    public GameObject[] sprites; // eggs, nest, enemies, quest points, portals, ..,

    //public Tile[] treeTiles; // possible tree tiles -- so we have diff types of trees

    [Range(0, 1)]
    public float treePercentage = 0.2f; // Change this to set the initial percentage of trees

    bool questCompleted = false;

    public QuestManager qm;

    void Start()
    {
        treePercentage = PlayerPrefs.GetFloat("Tree", 0.05f); // 0.05f if no quests are completed yet

        Debug.Log("tree percentage: " +  treePercentage);

        ResizeTilemap(150, 100);
        InitializeTilemap();
    }



    bool positionIsEmpty(Vector3Int position)
    {
        return (tilemap.GetTile(position) == null && waterTilemap.GetTile(position) == null && houseTilemap.GetTile(position) == null);
    }

    bool positionFarFromSprites(Vector3Int position, int distance)
    {
        foreach (var sprite in sprites)
        {
            float dist = Vector3.Distance(position, sprite.transform.position);
            if (dist <= distance)
            {
                return false;
            }
        }
        return true;
    }

    bool positionFarFromOtherTilemap(Vector3Int position, Tilemap otherTilemap, int distance)
    {
        BoundsInt bounds = otherTilemap.cellBounds;

        foreach (Vector3Int otherPosition in bounds.allPositionsWithin)
        {
            float dist = Vector3.Distance(position, otherPosition);
            if (dist <= distance)
            {
                return false;
            }
        }
        return true;
    }

    void InitializeTilemap()
    {
        BoundsInt bounds = tilemap.cellBounds;

        /*
        Debug.Log("Get tile: " + forestDetailsTilemap.GetTile(new Vector3Int(0, 0, 1)));

        if (forestDetailsTilemap.GetTile(new Vector3Int(0, 0, 1)) == null)
        {
            Debug.Log("nully nully null null null");
        } */

        Vector3Int offset = waterTilemap.origin - tilemap.origin;



        for (int i = 0; i <= bounds.xMax; i+=5)
        {
            for (int j = 0; j <= bounds.yMax; j+=5)
            {
                Vector3Int position = new Vector3Int(i, j, 0);
                
                position += offset;


                //position += house_offset;

                // Introduce randomness to the position
 
                position.x += Random.Range(-2, 2);
                position.y += Random.Range(-2, 2);

                float rand = Random.value;

                if (positionIsEmpty(tilemap.WorldToCell(position))
                    && positionFarFromSprites(position, 5) && rand <= 0.7f)
                {
                    float randomValue = Random.value;

                    if (randomValue <= treePercentage)
                    {
                        // generate random tree tile index
                        int index = Random.Range(0, (treeTile.Length - 1));

                        tilemap.SetTile(position, treeTile[index]);
                    }
                    else
                    {
                        tilemap.SetTile(position, treeStumpTile);
                    }
                }

            }
        }

        /*
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (houseTilemap.GetTile(pos) != null) 
            {
                print("house pos: " + pos);
                tilemap.SetTile(pos, null);
            }
        } */

        /*
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            float randomValue = Random.value;

            if (randomValue <= treePercentage)
            {
                tilemap.SetTile(position, treeTile);
            }
            else
            {
                tilemap.SetTile(position, treeStumpTile);
            }
        } */
    }

    void ResizeTilemap(int width, int height)
    {
        // Resize the tilemap grid
        //tilemap.size = new Vector3Int(width, height, 0);
        tilemap.size = waterTilemap.size;

        tilemap.ResizeBounds();

    }

    public void CompleteQuest()
    {
        Debug.Log("Quest completed ... changing trees to stumps");
        StartCoroutine(ChangeTilesAfterQuest());
    }

    IEnumerator ChangeTilesAfterQuest()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay based on your game's needs

        Debug.Log(" *** num quests finished= " + qm.GetNumQuestsFinished());

        float tree_perc = (qm.GetNumQuestsFinished() / qm.GetQuestMapLength());

        PlayerPrefs.SetFloat("Tree", tree_perc);

        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            float randomValue = Random.value;

            if (tilemap.GetTile(position) == treeStumpTile && randomValue <= tree_perc)
            {

                // generate random tree tile index
                int index = Random.Range(0, (treeTile.Length - 1));

                tilemap.SetTile(position, treeTile[index]);
            }
        }
    }
}

