using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork  {

    public NeuralLayer[] Layers;
    public int[] topology;
    public int WeightNumber;
    int LayerSize=0;
    public int Help = 0;

    public NeuralNetwork(int[] topology)
    {
        this.topology = topology;

        for (int i = 0; i < topology.Length; i++)
        {
            if(topology[i] > LayerSize) LayerSize = topology[i];
        }

        WeightNumber = 0;
        for (int i = 0; i < topology.Length-1; i++)
        {
            WeightNumber += (topology[i]+1) * topology[i + 1];
        }

        Layers = new NeuralLayer[topology.Length];
        for(int i = 0; i < topology.Length-1; i++)
        {
            Layers[i] = new NeuralLayer(topology[i],topology[i+1]);
        }
    }
    


    //Zwraca wartosci obliczone przez siec
    public float[] ProccesInput(float[] values)
    {
        float[] outputs = new float[LayerSize];
        outputs = values;
        for(int i = 0; i < topology.Length-1; i++)
        {
            outputs = Layers[i].ProcessLayer(outputs);
        }
        return outputs;
    }

    //Przypisuje losowe wagi w sieci
    public void RandomWeights(float min, float max)
    {
        for (int i = 0; i < topology.Length-1; i++)
        {
            Layers[i].SetRandomWeights(min,max);
        }
    }

}
