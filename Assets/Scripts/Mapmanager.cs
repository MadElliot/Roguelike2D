using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapmanager : MonoBehaviour {

    public GameObject[] FloorArray;
    public GameObject[] OutwallArray;
    public GameObject[] WallArray;
    public GameObject[] FoodArray;
    public GameObject[] Enemy;
    public GameObject Exit;

    public int minWallCount = 2;
    public int maxWallCount = 8;

    private Transform mapsHolder;
    private List<Vector2> positionList=new List<Vector2>();

    private int cols = 10;      //列
    private int rows = 10;      //行


	void Start() {

        InitMap();
       
	}


    //初始化地图
    private void InitMap()
    {

        mapsHolder = new GameObject("Walls and Floors").transform;
        //生成底板和外围墙
        for (int x = 0; x <cols ; x++)
        {
            for (int y = 0; y <rows; y++)
            {
                if (x==0||y==0||x==cols-1||y==rows-1)
                {
                   // int index = Random.Range(0,OutwallArray.Length);
                   // GameObject outwall = GameObject.Instantiate(OutwallArray[index], new Vector2(x, y), Quaternion.identity) as GameObject;
                   //outwall.transform.SetParent(mapsHolder);

                    GameObject outwall = OutwallArray[Random.Range(0, OutwallArray.Length)];
                    GameObject instance = Instantiate(outwall, new Vector2(x, y), Quaternion.identity);
                    instance.transform.SetParent(mapsHolder);
                }
                else
                {
                    int index = Random.Range(0, FloorArray.Length);
                    GameObject floor = GameObject.Instantiate(FloorArray[index], new Vector2(x, y), Quaternion.identity) as GameObject;
                    floor.transform.SetParent(mapsHolder);
                }                
            }
        }

        positionList.Clear();
        //获取地图中间区域
        for (int x = 2 ; x <cols-2; x++)
        {
            for (int y = 2; y < rows-2; y++)
            {
                positionList.Add(new Vector2(x, y));
            }
        }
        //生成障碍物
        int WallCount = Random.Range(minWallCount, maxWallCount + 1);
        InitPrefab(WallArray, WallCount);
        
        //生成食物2--level*2
        int foodCount = Random.Range(2, GetComponent<GameManager>().level*2+1);
        InitPrefab(FoodArray, foodCount);
        //生成敌人
        int enemyCount = GetComponent<GameManager>().level / 2;
        InitPrefab(Enemy, enemyCount);
        //生成出口
        Instantiate(Exit, new Vector2(cols - 2, rows - 2), Quaternion.identity);
       
    }
    private void InitPrefab(GameObject[] prefabs,int count) {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = RandomPosition();
            GameObject Prefab = RandomPrefab(prefabs);
           GameObject go = Instantiate(Prefab, pos, Quaternion.identity) as GameObject;
           go.transform.SetParent(mapsHolder);
        }
    
    }
    private Vector2 RandomPosition() {
        int positionIndex = Random.Range(0, positionList.Count);
        Vector2 Pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);//位置已占，将其移除
        return Pos;
    }
    private GameObject RandomPrefab(GameObject[] prefab) {
        int prefabIndex = Random.Range(0, prefab.Length);
        return prefab[prefabIndex];
    }
}
