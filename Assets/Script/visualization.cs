// 入力ファイルからデータをとってPCA.csに渡し、主成分分析後のデータを可視化する。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class visualization : MonoBehaviour {

    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    public GameObject obj;//TODOVRcamera;
    //public static int data_count;

    void Start() {

        //int dimension = 3; //TODO:GetComponent<PCA>().dimension;
        //Debug.Log("dimension = " + dimension);

        //csvFile = Resources.Load("data1") as TextAsset; // Resouces下のCSV読み込み。拡張子は無視
        ////print(csvFile.text);
        //StringReader reader = new StringReader(csvFile.text);

        //// ,で分割しつつ一行ずつ読み込み
        //// リストに追加
        //// StringReader.Peek : 読み取り対象の次の文字を返す。
        //// 使用できる文字がないorストリームがシークをサポートしていない場合-1となる。
        //while (reader.Peek() != -1)
        //{
        //    string line = reader.ReadLine(); // 一行ずつ読み込み
        //    csvDatas.Add(line.Split(',')); // ,区切りでリストに追加
        //}
       
        //data_count = csvDatas.Count;

        //float[][] multi_dim = new float[data_count][dimension];
        //for (int i=0; i<data_count; i++)
        //{
        //    for (int j=0; j<dimension; j++)
        //    {
        //        multi_dim[i][j] = float.Parse(csvDatas[i][j]);
        //    }

        //}

        //// 関数呼び出し主成分分析
        //GetComponent<PCA>().PCAmalysis(multi_dim[data_count][dimension])

        //Instantiate(obj, new Vector3(,, ), Quaternion.identity);

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

        for (int i = 0; i < csvDatas.Count(); i++)
        {
            Instantiate(obj, new Vector3(float.Parse(csvDatas[i][0]), float.Parse(csvDatas[i][1]), float.Parse(csvDatas[i][2])), Quaternion.identity);
        }
    }
 }