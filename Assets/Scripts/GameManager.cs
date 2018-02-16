using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Vector3 currentScale;
    public Vector3 defaultScale = new Vector3(8,0.5f,8);
    public GameObject currentBlock;
    public Transform[] spawners;
    public int lastSpawner = 0;
    public Transform tower;

    void InitGame()
    {
        lastSpawner = 0;
        currentScale = defaultScale;
        SpawnNewBlock();
    }

	void Start () {
        InitGame();
    }
	
	void Update () {
		
	}

    public void SpawnNewBlock()
    {
        currentBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);

        currentBlock.transform.position = spawners[lastSpawner].position;

        currentBlock.transform.localScale = currentScale;
        currentBlock.AddComponent<MoveBlock>().Init(lastSpawner == 0);

        currentBlock.transform.SetParent(tower);

        lastSpawner++;
        lastSpawner %= spawners.Length;
    }

    public void GameOver()
    {

    }
}
