using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomTest : MonoBehaviour {

    int[] r = new int[1000];
    // Use this for initialization
    void Start () {


        for (int i = 0; i < 100000; i++)
        {
            int r1;
            float r2;
            do
            {
                r1 = Random.Range(0, 1000);
                r2 = Random.value;
            } while (r1 <= r2 * 1000);
            r[r1]++;
        }
	}

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 1000; i++)
        {
            Gizmos.DrawCube(new Vector3(i,r[i]*0.5f,0), new Vector3(1f, r[i], 1f));
        }
    }
}
