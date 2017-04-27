using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellVisualizer : MonoBehaviour {

    public struct CellData
    {
        public int lv;
        public Vector3 pos;
        public float radius;
        public GameObject sphere;

        public CellData(int l, Vector3 p, float r, GameObject sp)
        {
            lv = l;
            pos = p;
            radius = r;
            sphere = sp;
        }
    }

    public int levelMax = 5;            // 階層上限
    public int numX = 4;
    public int numY = 4;
    public float startRadius = 10;          // 初期半径
    public float angleRandomRange = 90;     // ランダム角度の範囲（度）
    public float dividePer = 0.4f;          // 分岐する確率(0～1)
    public float radiusDecayRate = 0.5f;    // 半径の減衰率

    private List<CellData> cellList = new List<CellData>();

    // Use this for initialization
    void Start () {
        ResetCel();
    }
	
    void ResetCel()
    {
        for (int i = 0; i < cellList.Count; i++)
        {
            Destroy(cellList[i].sphere);
        }

        cellList.Clear();

        float startTime = Time.realtimeSinceStartup;

        Cell.CellDivision(levelMax, numX, numY, startRadius, angleRandomRange, dividePer, radiusDecayRate, AddDebugObject);

        Debug.Log("Time " + (Time.realtimeSinceStartup - startTime) + " cellList.Count " + cellList.Count);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCel();
        }
	}

    private void AddDebugObject(int lv, int lvMax, Vector3 pos, float radius)
    {
        GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        s.transform.position = pos;
        s.transform.localScale = Vector3.one * radius * 2f;
        var mat = s.GetComponent<Renderer>().material;
        if (mat != null)
        {
            mat.SetColor("_Color", Color.HSVToRGB((float)lv / lvMax, 1, 1));
            //mat.SetColor("_Color", Color.HSVToRGB(1, 1, 1f - (float)lv / lvMax));
        }

        cellList.Add(new CellData(lv, pos, radius, s));
    }
}
