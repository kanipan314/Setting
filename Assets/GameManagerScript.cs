using System.Collections.Generic;
using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject clearText;
    public GameObject wallPrefab;
    public GameObject particlePrefab;
    int[,] map;
    int[,] wallmap;
    GameObject[,] field;
    GameObject instance;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    //private enum Scean;
    //Scean scean
    //{

    //}

    /// <summary>
    /// 与えられた数字をマップ上で移動させる
    /// </summary>
    /// <param name="number">移動させる数字</param>
    /// <param name="moveFrom">元の位置</param>
    /// <param name="moveTo">移動先の位置</param>
    /// <returns>移動可能な時 true</returns>
    bool MoveNumber(Vector2Int movefrom, Vector2Int moveto)
    {
        if (moveto.y < 0 || moveto.y >= field.GetLength(0))
            return false;

        if (moveto.x < 0 || moveto.x >= field.GetLength(1))
            return false;

        if (field[moveto.y,moveto.x]?.tag == "wall")
        {
            return false;
        }

        if (field[moveto.y, moveto.x]?.tag == "Box")
        {
            var offset = moveto - movefrom;  // 箱の行先を決めるための差分
            bool result = MoveNumber(moveto, moveto + offset);

            if (!result)
                return false;
        }   // 行先に箱がある時



        field[movefrom.y, movefrom.x].transform.position =
            new Vector3(moveto.x, -1 * moveto.y, 0);    // シーン上のオブジェクトを動かす
        ////MoveScript
        Vector3 moveToPostion = new Vector3(moveto.x, -1 * moveto.y, 0);
        var obj = field[movefrom.y, movefrom.x];

        //if (obj == null)
        //{
        //    Debug.LogWarning($"obj が null です。{}");
        //    return false;
        //}

        var move = obj.GetComponent<Move>();
        move.Moveto(moveToPostion);

        // field のデータを動かす
        field[moveto.y, moveto.x] = field[movefrom.y, movefrom.x];
        field[movefrom.y, movefrom.x] = null;


        return true;
    }

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                GameObject obj = field[y, x];

                if (obj != null && obj.tag == "Player")
                {
                    return new Vector2Int(x, y);
                }   // プレイヤーを見つけた
            }
        }

        return new Vector2Int(-1, -1);  // 見つからなかった
    }

    bool isCleard()
    {
        //Vector2Int型の可変長配列の作成

        List<Vector2Int> goals = new List<Vector2Int>();

        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0;x < map.GetLength(1); x++)
            {
                if (map[y,x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        //  要素数はgoals.Countで取得
        for(int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y,goals[i].x];

            if(f == null || f.tag != "Box")
            {
                //一つでも箱がなかったら条件未達成
                return false;
            }
        }

        //条件未達成でなければ条件達成
        return true;
    }

    void ResetScean()
    {
        // オブジェクトを初期状態に戻す
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

    }


    //void PrintArray()
    //{
    //    string debugText = "";

    //    for (int y = 0; y < map.GetLength(0); y++)
    //    {
    //        for (int x = 0; x < map.GetLength(1); x++)
    //        {
    //            debugText += map[y, x].ToString() + ",";
    //        }

    //        debugText += "\n";
    //    }

    //    Debug.Log(debugText);
    //}

    void Start()
    {

        Screen.SetResolution(1280,720,false);

        map = new int[,]
        {
           {0, 0, 0, 0, 0, 0, 0, 0, 0},
           {0, 3, 1, 3, 0, 0, 0, 3, 0},
           {0, 2, 2, 0, 0, 3, 0, 0, 0},
           {0, 2, 3, 2, 0, 0, 0, 2, 0},
           {0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        wallmap = new int[,]
        {
           {4,4,4,4,4,4,4,4,4,4,4},
           {4,0,0,0,0,0,0,0,0,0,4},
           {4,0,0,0,0,0,0,0,0,0,4},
           {4,0,0,0,0,0,0,0,0,0,4},
           {4,0,0,0,0,0,0,0,0,0,4},
           {4,0,0,0,0,0,0,0,0,0,4},
           {4,4,4,4,4,4,4,4,4,4,4}
        };

        //PrintArray();

        field = new GameObject[
            map.GetLength(0),
            map.GetLength(1)
        ];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    instance =
                        Instantiate(playerPrefab, new Vector3(x, -1 * y, 0), Quaternion.identity);
                    field[y, x] = instance;

                }   // プレイヤーを見つけた
                else if (map[y, x] == 2)
                {
                    instance =
                        Instantiate(boxPrefab, new Vector3(x, -1 * y, 0), Quaternion.identity);
                    field[y, x] = instance;
                }   // 箱を見つけた
                if (map[y, x] == 3)
                {
                    instance = Instantiate(goalPrefab, new Vector3(x, -1 * y, 0), Quaternion.identity);
                    field[y, x] = instance;
                }

            }
        }

        for (int y = 0; y < wallmap.GetLength(0); y++)
        {
            for (int x = 0; x < wallmap.GetLength(1); x++)
            {
                if (wallmap[y, x] == 4)
                {
                    instance = Instantiate(wallPrefab, new Vector3(x - 1 , -1 * y + 1, 0), Quaternion.identity);
                }
                
            }
        }

        // 初期状態を記録
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();


            for (int i = 0; i < 20; i++)
            {

                Instantiate(particlePrefab, new Vector3(playerIndex.x,-1 * playerIndex.y, 0), Quaternion.identity);
            }
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
            

            if (isCleard())
            {
                clearText.SetActive(true);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            for (int i = 0; i < 20; i++)
            {

                Instantiate(particlePrefab, new Vector3(playerIndex.x, -1 * playerIndex.y, 0), Quaternion.identity);
            }

            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));

            if (isCleard())
            {
                clearText.SetActive(true);

            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            for (int i = 0; i < 20; i++)
            {

                Instantiate(particlePrefab, new Vector3(playerIndex.x, -1 * playerIndex.y, 0), Quaternion.identity);
            }

            MoveNumber(playerIndex, playerIndex + new Vector2Int(0,-1));

            if (isCleard())
            {
                clearText.SetActive(true);

            }

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            for (int i = 0; i < 20; i++)
            {

                Instantiate(particlePrefab, new Vector3(playerIndex.x, -1 * playerIndex.y, 0), Quaternion.identity);
            }

            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));

            if (isCleard())
            {
              clearText.SetActive(true);
            }

        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ResetScean();
        }

    }
}
