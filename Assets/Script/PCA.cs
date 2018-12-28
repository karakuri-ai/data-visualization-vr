// 主成分分析を行う
// visualization.csから多次元データを受け取り、次元圧縮してvisualization.csに戻す。
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCA : MonoBehaviour {

    // 入力データの次元
    public int dimension = 100;


    public void PCAnalysis(double[,] multi_dim)
    {
        int data_count = multi_dim.GetLength(0);
        double[] sum = new double[data_count]; // 合計
        double[] ave = new double[data_count]; // 平均値

        // 平均を求める
        for(int i=0; i< dimension; i++)
        {
            for(int j=0; j< data_count; j++)
            {
                sum[i] += multi_dim[i, j];
            }
            ave[i] = sum[i] / data_count;
        }

        // 分散を求める

        // 共分散
        double[,] covariance = new double[dimension, dimension];
        for(int i=0; i<dimension; i++)
        {
            for(int j=0; j<dimension; j++)
            {
                for(int k=0; k<data_count; k++)
                {
                    covariance[i,j] += (multi_dim[i, k] - ave[i]) * (multi_dim[j, k] - ave[j]);
                }
                covariance[i, j] /= data_count;
            }
        }




        //// ヤコビ法。固有値ベクトルを求める。
        //// ref http://ou812.web.fc2.com/CsTips/AnalysisJacobi.html
        ///// <summary>正方行列の固有値、固有ベクトルを算出（ヤコビ法・対称行列のみ有効）</summary>
        ///// <param name="matrix">対称行列</param>
        ///// <param name="nMaxStep">最大評価ステップ数</param>
        ///// <param name="dSettleValue">収束値</param>
        ///// <param name="eigenValue">固有値リスト</param>
        ///// <returns>固有ベクトルのリスト</returns>
        ///// <remarks>
        ///// 非対称行列の場合は「べき乗法」という解法があるが、絶対値最大の固有値しか求まらない。
        ///// http://www.asahi-net.or.jp/~UC3K-YMD/Lesson/Section03/section03_09.html
        ///// </remarks>
        //public static List<Vector> GetEigenVectorsByJacobi(SquareMatrix matrix, int nMaxStep, double dSettleValue, out Vector eigenValue)
        //{
        //    eigenValue = null;
        //    SquareMatrix A1 = (SquareMatrix)matrix.Clone();
        //    SquareMatrix X1 = new SquareMatrix(matrix.Size);
        //    X1.SetUnitMatrix();

        //    SquareMatrix A2 = new SquareMatrix(matrix.Size);
        //    SquareMatrix X2 = new SquareMatrix(matrix.Size);
        //    int nStep = 0, p = 0, q = 0;

        //    while (nStep < nMaxStep)
        //    {
        //        // 最大要素の収束判定
        //        double dMaxElement = A1.GetMaxElement(out p, out q);
        //        if (dMaxElement < dSettleValue)
        //        {
        //            // 収束した
        //            break;
        //        }

        //        double t = 0.5 * (A1[p, p] - A1[q, q]);
        //        double v = Math.Abs(t) / Math.Sqrt(A1[p, q] * A1[p, q] + t * t);
        //        double sn = Math.Sqrt(0.5 * (1.0 - v));
        //        if (-A1[p, q] * t < 0.0)
        //        {
        //            sn = -sn;
        //        }
        //        double cs = Math.Sqrt(1.0 - sn * sn);

        //        // Akの計算
        //        for (int i = 0; i < matrix.Size; i++)
        //        {
        //            if (i == p)
        //            {
        //                for (int j = 0; j < matrix.Size; j++)
        //                {
        //                    if (j == p)
        //                        A2[p, p] = A1[p, p] * cs * cs + A1[q, q] * sn * sn -
        //                           2.0 * A1[p, q] * sn * cs;
        //                    else if (j == q)
        //                        A2[p, q] = 0.0;
        //                    else
        //                        A2[p, j] = A1[p, j] * cs - A1[q, j] * sn;
        //                }
        //            }
        //            else if (i == q)
        //            {
        //                for (int j = 0; j < matrix.Size; j++)
        //                {
        //                    if (j == q)
        //                        A2[q, q] = A1[p, p] * sn * sn + A1[q, q] * cs * cs +
        //                           2.0 * A1[p, q] * sn * cs;
        //                    else if (j == p)
        //                        A2[q, p] = 0.0;
        //                    else
        //                        A2[q, j] = A1[q, j] * cs + A1[p, j] * sn;
        //                }
        //            }
        //            else
        //            {
        //                for (int j = 0; j < matrix.Size; j++)
        //                {
        //                    if (j == p)
        //                        A2[i, p] = A1[i, p] * cs - A1[i, q] * sn;
        //                    else if (j == q)
        //                        A2[i, q] = A1[i, q] * cs + A1[i, p] * sn;
        //                    else
        //                        A2[i, j] = A1[i, j];
        //                }
        //            }
        //        }
        //        // Xkの計算
        //        for (int i = 0; i < matrix.Size; i++)
        //        {
        //            for (int j = 0; j < matrix.Size; j++)
        //            {
        //                if (j == p)
        //                    X2[i, p] = X1[i, p] * cs - X1[i, q] * sn;
        //                else if (j == q)
        //                    X2[i, q] = X1[i, q] * cs + X1[i, p] * sn;
        //                else
        //                    X2[i, j] = X1[i, j];
        //            }
        //        }

        //        A1 = (SquareMatrix)A2.Clone();
        //        X1 = (SquareMatrix)X2.Clone();

        //        // 次のステップへ
        //        nStep++;
        //    }

        //    if (nStep >= nMaxStep) return null;

        //    // 行列A1,X1に固有値と固有ベクトルが設定された。

        //    // 固有ベクトルリスト
        //    List<Vector> eigenVectors = new List<Vector>();

        //    // 固有値リスト
        //    eigenValue = new Vector(matrix.Size);
        //    for (int i = 0; i < matrix.Size; i++)
        //    {
        //        // 固有値
        //        eigenValue[i] = A1[i, i];

        //        // 固有ベクトル
        //        Vector eigenVector = new Vector(matrix.Size);
        //        for (int j = 0; j < matrix.Size; j++)
        //        {
        //            eigenVector[j] = X1[j, i];
        //        }
        //        eigenVectors.Add(eigenVector);
        //    }

        //    return eigenVectors;
        //}
    }



}
