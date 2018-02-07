using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAInterpretation : MonoBehaviour
{


    public DNA MyDNA = new DNA(new int[1] { 0 });
    public float RayDistance = 10;
    public LayerMask RayMask;
    public PopulationManager Manager;
    public Checkpoints ChPoints;

    // Use this for initialization
    void Start()
    {
        ChPoints = GameObject.Find("CheckPoints").GetComponent<Checkpoints>();
    }


    //Aktualizuje dopasowanie auta
    void Update()
    {
        if (MyDNA.Alive)
        {
            MyDNA.Fitness = FitnessFunc();  
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(MyDNA==null) MyDNA = new DNA(new int[1] { 0 });


        if (MyDNA.Alive)
        {
            float[] output = new float[2];
            output = MyDNA.nNetwork.ProccesInput(CheckRoad());
            GetComponent<Drive>().SetInput(output[0], output[1]);
        }
    }


    //Obsluguje wszystkie czujniki
    float[] CheckRoad()
    {
        float[] result = new float[5];
        int[] angle = new int[5]{ 0, 45, -45, 75, -75 };
        for(int  i =0;i < 5; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Quaternion.AngleAxis(angle[i], Vector3.forward) * transform.up,RayDistance,RayMask);
            if (hit.collider != null)
            {
                result[i] = (RayDistance-hit.distance) / RayDistance;
                Debug.DrawRay(transform.position,(Quaternion.AngleAxis(angle[i], Vector3.forward)  *transform.up)*hit.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position,(Quaternion.AngleAxis(angle[i], Vector3.forward) * transform.up)*RayDistance, Color.blue);
                result[i] = 0.0f;
            }
        }
        return result;
    }


    //Niszczy auto przy kolizji
    void OnCollisionEnter2D(Collision2D coll)
    {
        KillCar();
    }

    //Pozbywanie sie auta
    public void KillCar()
    {
        if (MyDNA.Alive)
        {
            MyDNA.Alive = false;
            GetComponent<Drive>().SetInput(0, 0);
            Manager.CarDead();
            Destroy(gameObject);
        }
    }

    //Oblicza dopasowanie auta
    float FitnessFunc()
    {
        float fitness = 0, dist2=0.1f, dist3=0,dist4=0,dist5=0;

        int currentpoint = 0;
        currentpoint = ChPoints.NearestPoint(gameObject.transform);


        if (currentpoint > 0) {
            dist2 = Vector3.Distance(gameObject.transform.position, ChPoints.Points[currentpoint - 1].position);
            dist3 = Vector3.Distance(ChPoints.Points[currentpoint].position, ChPoints.Points[currentpoint - 1].position);
        }
        if (currentpoint < ChPoints.Points.Length-1)
        {
            dist4 = Vector3.Distance(gameObject.transform.position, ChPoints.Points[currentpoint + 1].position);
            dist5 = Vector3.Distance(ChPoints.Points[currentpoint].position, ChPoints.Points[currentpoint + 1].position);
        }

        return fitness += currentpoint-(dist3/dist2)+(dist5/dist4);
    }

}
