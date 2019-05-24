using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSON;
using System;

/// <summary>
/// GETリクエストを送信して、AWSから可視化するデータを取ってくる。
/// Main.FetchDataにアタッチされている。
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
    public List<float[]> Datas = new List<float[]>();
    public IList x_initial, y_initial, z_initial;

    public GameObject obj;
    public GameObject camera;

    public void OnClick()
    {
        StartCoroutine(GetText());

        //text = "{\"pca_data\": {\"y\": [33, 4]}}";
        Debug.Log(text);
        
        // MiniJSON
        var json = Json.Deserialize(text) as Dictionary<string, object>;
        //Debug.Log(json["statusCode"]);
        //string pca_data = json["pca_data"].ToString();
        //var x = Json.Deserialize(pca_data) as Dictionary<string, object>;
        //Debug.Log(x);
        var pca_data = (Dictionary<string, object>)json["pca_data"];
        //var data = Json.Deserialize(pca_data) as Dictionary<string, object>;
        //Debug.Log(pca_data);
        //Debug.Log(x[0]);
        x_initial = (IList)pca_data["x"];
        y_initial = (IList)pca_data["y"];
        z_initial = (IList)pca_data["z"];
        Debug.Log(x_initial[1]);

        // 可視化処理
        for (int i = 0; i < x_initial.Count; i++)
        {
            //GameObject o = Instantiate(obj, new Vector3(float.Parse((string)x_initial[i]), float.Parse((string)y_initial[i]), float.Parse((string)z_initial[i])), Quaternion.identity); // うまくいかない
            GameObject o = Instantiate(obj, new Vector3((float)(double)x_initial[i], (float)(double)y_initial[i], (float)(double)z_initial[i]), Quaternion.identity);
            o.transform.parent = camera.transform;
        }

        //    List<float> z = (List<float>)pca_data["z"];
        //    Debug.Log(x[0]);
        //    // TODO:Datasの形にする必要もない？そのままx[], y[], z[]を渡してあげればいい？
        //    // x[]をpublicにする方法はない？
        //    for(int i = 0; i < x.Count; i++)
        //    {
        //        Datas[i][0] = x[i];
        //        Datas[i][1] = y[i];
        //        Datas[i][2] = z[i];
        //    }
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
                // UTF8文字列として取得する
                text = request.downloadHandler.text;
                //Debug.Log(text);

            }
        }
    }
}
