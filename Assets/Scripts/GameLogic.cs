using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public Galaxy g;
    public GameObject galaxy;

	// Use this for initialization
	void Start () {
        g = new Galaxy();
        Debug.Log(g.DebugGalaxy());

        SolarSystem[] systems = g.GetSystems();
        foreach (SolarSystem s in systems)
        {
            Vector3 pos = new Vector3(s.x, s.y, 0f);

            Instantiate(galaxy, pos, Quaternion.identity);
            foreach (SolarSystem neighbour in s.neighbours)
            {
                Debug.DrawLine(pos, new Vector3(neighbour.x, neighbour.y, 0f), Color.green, 100000000000f);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

    }
}
