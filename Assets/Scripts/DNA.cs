using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA{

    public float Fitness;
    public NeuralNetwork nNetwork;
    public bool Alive;
    public int id;


    public DNA(int[] topology,int nr = 0)
    {
        Fitness = 0;
        id = nr;
        Alive = true;
        nNetwork = new NeuralNetwork(topology);
    }

}
