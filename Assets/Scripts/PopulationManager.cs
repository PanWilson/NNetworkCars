using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopulationManager : MonoBehaviour {

    Population CurrentPopulation;
    public int PopulationNumber;
    public GameObject CarPrefab;
    public float mutation=0.01f;
    public float LifeTime = 10.0f;
    float CurrentTime=0.0f;
    public int[] topology;
    public Text GenerationCountTxt;
    public Text TimerTxt;
    public InputField GeneratiomInput;
    public InputField MutationInput;
    public InputField TimeInput;

    //Tick gry

    void Update () {

        CurrentTime -= Time.deltaTime;
        if (CurrentTime < 0)
        {
            KillAll();
        }
        if(CurrentTime>=0) SetTimeText((int)Mathf.Floor(CurrentTime));
    }

    //Rozpoczyna cala ewolucje
    public void StartEvolution()
    {
        if (CurrentPopulation == null)
        {
            //Sprawdzenie czy uzytkownik wprowadzil wlasciwe wartosci
            if (GeneratiomInput.text != "")
            {
                if (int.Parse(GeneratiomInput.text) > 0) PopulationNumber = int.Parse(GeneratiomInput.text);
            }
            GeneratiomInput.enabled = false;
            if (TimeInput.text != "")
            {
                if (float.Parse(TimeInput.text) > 0) LifeTime = float.Parse(TimeInput.text);
            }
            TimeInput.enabled = false;
            if (MutationInput.text != "")
            {
                if (MutationInput.text != "" & float.Parse(MutationInput.text) >= 0 && float.Parse(MutationInput.text) <= 1) mutation = float.Parse(MutationInput.text);
            }
            MutationInput.enabled = false;

            CurrentPopulation = new Population(PopulationNumber, mutation, topology);
            CurrentPopulation.CreatePopulation();
            CreateCars();
        }
    }


    //Rozpoczyna nowa generacje
    void StartGeneration()
    {
        CurrentPopulation.Segregate();
        CurrentPopulation.NewGeneration();
        CurrentPopulation.Alive = PopulationNumber;
        CreateCars();
    }


    //Tworzy odpowiednia ilosc aut i przypisuje im DNA
    void CreateCars()
    {
        for (int i = 0; i < PopulationNumber; i++)
        {
            GameObject Car = Instantiate(CarPrefab, transform.position, transform.rotation);
            Car.GetComponent<DNAInterpretation>().MyDNA = CurrentPopulation.DNAs[i];
            Car.GetComponent<DNAInterpretation>().Manager = gameObject.GetComponent<PopulationManager>();
        }
        SetGenerationText(CurrentPopulation.Generation);
        CurrentTime = LifeTime;
    }

    //Zlicza martwe auta
    public void CarDead()
    {
        if (--CurrentPopulation.Alive == 0) StartGeneration();
    }


    //Zabija wszystkie auta
    public void KillAll()
    {
        DNAInterpretation[] cars = FindObjectsOfType<DNAInterpretation>();
        foreach(DNAInterpretation c in cars)
        {
            c.KillCar();
        }
    }

    //wylacza gre
    public void doExitGame()
    {
        Application.Quit();
    }

    //Restartuje gre
    public void ResetGame()
    {
        SceneManager.LoadScene("BasicMap");
    }


    //Ustawia tekst generacji
    public void SetGenerationText(int x)
    {
        GenerationCountTxt.text = "Numer generacji: " + x;
    }

    //ustawia tekst czasu
    public void SetTimeText(int x)
    {
        TimerTxt.text = "Do końca generacji:" + x;
    }
}
