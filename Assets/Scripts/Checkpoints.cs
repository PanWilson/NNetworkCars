using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

    public Transform[] Points;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //Zwraca punkt o danym indeksie
    public Transform GivePoint(int i)
    {
        if(i<Points.Length&&i>0)return Points[i];
        else if(i>= Points.Length)return Points[Points.Length-1];
        else return Points[0];
    }

    //Zwraca indeks najblizszego punktu
    public int NearestPoint(Transform pos)
    {
        int pointnr = 0;
        float distance=0;
        distance = Vector3.Distance(pos.position, Points[0].position);
        for (int i = 1;i<Points.Length;i++)
        {
            if(Vector3.Distance(pos.position, Points[i].position) < distance)
            {
                distance = Vector3.Distance(pos.position, Points[i].position);
                pointnr = i;
            }
        }
        return pointnr;
    }
}
