using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

/// <summary>
/// FetchDataで取ってきた主成分分析後の位置データを受け取って、プロットする。
/// Main.Visualizerにアタッチされている。
/// </summary>
public class visualization : MonoBehaviour {

    // csvファイルを使う時
    //TextAsset csvFile;
    //List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    List<Vector3> data;
    GameObject FetchButton;

    public GameObject obj;
    public GameObject camera;

    public void OnClick()
    {
        // VisualizeButtonを押すと可視化処理が始まる
        // inspectatorの操作ができない

        FetchButton = GameObject.Find("Canvas/FetchButton");
        data = FetchButton.GetComponent<FetchData>().data;
        Debug.Log(data[0]);



        //// csvファイルを読みこむ時
        //// TODO: csvファイルを使いたいときに動かせるようにしたい(条件分岐か何かで)
        //csvFile = Resources.Load("data1") as TextAsset; // Resouces下のCSV読み込み。拡張子は無視
        ////print(csvFile.text);
        //StringReader reader = new StringReader(csvFile.text);

        //// ,で分割しつつ一行ずつ読み込み
        //// リストに追加
        //while (reader.Peek() != -1)
        //{
        //    string line = reader.ReadLine(); // 一行ずつ読み込み
        //    csvDatas.Add(line.Split(',')); // ,区切りでリストに追加
        //}
        ////Debug.Log(script.x_initial[0]);

        //// 可視化処理
        //for (int i = 0; i < csvDatas.Count(); i++)
        //{
        //    //Instantiate(obj, new Vector3((float)script.x_initial[i], (float)script.y_initial[i], (float)script.z_initial[i]), Quaternion.identity);
        //    GameObject o = Instantiate(obj, new Vector3(float.Parse(csvDatas[i][0]), float.Parse(csvDatas[i][0]), float.Parse(csvDatas[i][0])), Quaternion.identity);
        //    o.transform.parent = camera.transform;
        //}


        //可視化処理
        for (int i = 0; i < data.Count(); i++)
        {
            GameObject o = Instantiate(obj, data[i], Quaternion.identity);
            o.transform.parent = camera.transform;
        }
    }
 }