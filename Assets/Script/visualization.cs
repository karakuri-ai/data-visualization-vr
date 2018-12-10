using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class visualization : MonoBehaviour {

    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    public GameObject obj;

    void Start () {
        csvFile = Resources.Load("data1") as TextAsset; // Resouces下のCSV読み込み。拡張子は無視
        //print(csvFile.text);
        StringReader reader = new StringReader(csvFile.text);

        // ,で分割しつつ一行ずつ読み込み
        // リストに追加
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // ,区切りでリストに追加
        }

        for (int i=0; i<csvDatas.Count(); i++)
        {
            Instantiate(obj, new Vector3(float.Parse(csvDatas[i][0]), float.Parse(csvDatas[i][1]), float.Parse(csvDatas[i][2])), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
