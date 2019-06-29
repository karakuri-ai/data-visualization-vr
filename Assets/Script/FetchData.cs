using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using MiniJSON;
using System;

/// <summary>
/// GETリクエストを送信して、AWSから可視化するデータを取ってくる。
/// Canvas.FetchButtonにアタッチされている。
/// </summary>
/// 
/// レスポンス:
///     {
///         "statusCode": int;
///         "pca_data": Dictionary<string, int[]>;
///     }
/// 
/// Dictionaryの具体例:
///     {
///         "x": [2.5, 0.1, 0.9, 15.2, 3.3, 3.7]
///         "y": [0.6, 5.1, 8.3, 2.0, 1.1, 6.2]
///         "z": [0.7, 1.2, 4.7, 3.1, 5.2, 2.6]
///     }
public class FetchData : MonoBehaviour {


    public string text;
    //public List<float[]> Datas = new List<float[]>();
    public IList x_initial, y_initial, z_initial;

    public GameObject obj;
    public GameObject camera;
    public GameObject f;

    //public Data visual_data = new Data();
    //public Data visual_data;
    //public Data visual_data = new Data();
    public List<Vector3> data = new List<Vector3>();

    public void OnClick()
    {
        StartCoroutine(GetText());
    }

    // https://blog.applibot.co.jp/2016/07/20/unity-webrequest/
    // 使用する際はurlを変更する。
    IEnumerator GetText()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://u9o2yq1n5j.execute-api.ap-northeast-1.amazonaws.com/sample");
        // 下記でも可
        // UnityWebRequest request = new UnityWebRequest("http://example.com");
        // methodプロパティにメソッドを渡すことで任意のメソッドを利用できるようになった
        // request.method = UnityWebRequest.kHttpVerbGET;

        // リクエスト送信
        yield return request.SendWebRequest();

        // 通信エラーチェック
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                //UTF8文字列として取得する
                text = request.downloadHandler.text;
                Debug.Log(text);
                BodyJson bodyJson = JsonUtility.FromJson<BodyJson>(text);
                List<float> x = bodyJson.pca_data.x;
                List<float> y = bodyJson.pca_data.y;
                List<float> z = bodyJson.pca_data.z;
                for (int i = 0; i < x.Count; i++)
                {
                    data.Add(new Vector3((float)x[i], (float)y[i], (float)z[i]));
                }
                Debug.Log(data[0]);
            }
        }
    }

    // AWS から取得するデータの形式を規定するクラス
    [System.Serializable]
    class BodyJson
    {
        public int statusCode;
        public PosJson pca_data;
    }

    [System.Serializable]
    class PosJson
    {
        public List<float> x;
        public List<float> y;
        public List<float> z;
    }
}
