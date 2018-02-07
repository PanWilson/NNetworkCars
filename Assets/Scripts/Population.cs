using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population{

	public int PopulationNumber=0;
    public List<DNA> DNAs;
    public int ParentsNumber = 2;
    int[] GTopology;
    float MutationProb;
    public int Generation;
    public int Alive;

    public Population(int populationNumber, float mutationProb, int[] topology)
    {
        this.PopulationNumber = populationNumber;
        this.MutationProb = mutationProb;
        this.Generation = 1;
        this.Alive = populationNumber;
        GTopology = topology;
        DNAs = new List<DNA>();
    }


    //Inicjalizuje populacje
    public void CreatePopulation()
    {
        if (PopulationNumber != 0)
        {
            for (int i = 0; i < PopulationNumber; i++)
            {
                DNAs.Add(new DNA(GTopology, i));
            }
        }
    }

    //Segreguje DNA według dopasowania
    public void Segregate()
    {
        DNAs.Sort((a,b)=>(-1*a.Fitness.CompareTo(b.Fitness)));
    }

    //Tworzy nowa generacje
    public void NewGeneration()
    {
        Generation++;
        Alive = PopulationNumber;
        List<DNA> newDNAs= new List<DNA>();
        for (int i = 0; i < PopulationNumber; i++)
        {
            newDNAs.Add(CreateChild(PickParents()));
        }

        DNAs = newDNAs;
    }

    //Wybiera "rodzicow" dla nowego DNA  
    NeuralNetwork[] PickParents()
    {
        int r1;
        float r2;
        NeuralNetwork[] Parents = new NeuralNetwork[ParentsNumber];
        for (int i=0; i < ParentsNumber; i++)
        {
            do
            {
                r1 = Random.Range(0, PopulationNumber);
                r2 = Random.value;
            } while (DNAs[r1].Fitness <= (r2 * DNAs[0].Fitness));
            Parents[i] = DNAs[r1].nNetwork;
        }

        return Parents;
    }

    //Zwraca DNA utworzone z wybranych rodzicow 
    DNA CreateChild(NeuralNetwork[] parents)
    {
        DNA child = new DNA(GTopology);
        for(int i = 0;i<parents[0].Layers.Length-1; i++)
        {
            float[,] hWeights = new float[GTopology[i]+1,GTopology[i+1]];

            for (int k = 0; k < GTopology[i]+1; k++)
            {
                for (int j = 0; j < GTopology[i+1] ; j++)
                {
                    bool toe = (Random.Range(0,2) == 0);

                    if (toe)hWeights[k,j] = parents[0].Layers[i].Weights[k, j];
                    else hWeights[k ,j] = parents[1].Layers[i].Weights[k, j];

                    if (Random.value < MutationProb) hWeights[k,j] *= Random.Range(-1.0f,1.0f);
                }
            }
            child.nNetwork.Layers[i].SetWeights(hWeights);
        }

        return child;
    }

    public void SetFitnessById(int id,float fitt)
    {
        Debug.Log(fitt);
        Debug.Log(DNAs.Find(x => x.id == id).Fitness);
        DNAs.Find(x => x.id == id).Fitness =fitt;
    }


}
