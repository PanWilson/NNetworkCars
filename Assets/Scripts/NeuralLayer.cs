using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralLayer {

    public int NeuronNumber;
    public int Connections;
    public float[,] Weights;
    public float[] NeuronSize;


    public NeuralLayer(int neuronNumber,int connections)
    {
        NeuronNumber = neuronNumber+1;
        Connections = connections;
        NeuronSize = new float[Connections];
        SetRandomWeights();
    }


    //Zapisuje podane wagi
    public void SetWeights(float[,] weights)
    {
        Weights = new float[NeuronNumber, Connections];
            for (int i = 0; i < NeuronNumber; i++) {
                for (int j = 0; j < Connections; j++)
                {
                    Weights[i,j] = weights[i,j];
                }
            }
    }


    //Zwraca wagi
    public float[,] GetWeights()
    {
        float[,] hWeights = new float[NeuronNumber , Connections];

            for (int i = 0; i < NeuronNumber; i++)
            {
                for (int j = 0; j < Connections; j++)
                {
                    hWeights[i ,j] = Weights[i, j];
                }
            }
        return hWeights;
    }


    //Zwraca wartosci obliczane przez warstwe
    public float[] ProcessLayer(float[] values)
    {
        float[] Output = new float[Connections];

        for(int i = 0;i < Connections; i++)
        {
            for (int j = 0; j <NeuronNumber; j++)
            {
                if (j==NeuronNumber-1) Output[i] += Weights[j, i];
                else Output[i] += values[j] * Weights[j, i];
            }
            Output[i] = Sigmoid(Output[i]*6)-0.5f;

        }
        NeuronSize = Output;
        return Output;
    }

    //Kopiuje warstwe
    public NeuralLayer CopyLayer()
    {
        NeuralLayer copy= new NeuralLayer(NeuronNumber,Connections);

        copy.Weights = Weights;

        return copy;
    }

    //Ustawia losowe wagi w warstwie
    public void SetRandomWeights(float min=-1.0f, float max=1.0f)
    {
        Weights = new float[NeuronNumber, Connections];
        for (int i = 0; i < NeuronNumber; i++)
        {
             for (int j = 0; j < Connections; j++)
             {
                Weights[i, j] = Random.Range(min,max);
                
            }
        }
    }

   
    float Sigmoid(float value)
    {
        if (value > 7) return 1.0f;
        else if (value < -7) return -1.0f;
        else return 1.0f/( 1.0f+Mathf.Exp(-value));

    }

    public string NToString()
    {
        string str = "";
        for(int i =0; i < NeuronNumber;i++)
        {
            str += i + "### ";
            for(int j = 0; j < Connections; j++)
            {
                str += Weights[i, j] + " ";
            }
        }
        return str;
    }

}
