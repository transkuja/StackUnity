using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Vector3 currentScale;
    public Vector3 defaultScale = new Vector3(8,0.5f,8);
    public GameObject currentBlock;
    public Transform[] spawners;
    public int lastSpawner = 0;
    public Transform tower;
    public GameObject gameOverPanel;
    public bool isInGameOver = false;
    public Text highScoreTxt;
    public Color lastColor;

    void InitGame()
    {
        Camera.main.orthographicSize = 5;
        lastSpawner = 0;
        currentScale = defaultScale;
        isInGameOver = false;
        lastColor = Color.white;
        tower.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", new Color(transform.position.y / 5.0f, 0, transform.position.y / 5.0f));
        SpawnNewBlock();
    }

	void Start () {
        InitGame();
    }

    public void SpawnNewBlock()
    {
        currentBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);

        currentBlock.transform.position = spawners[lastSpawner].position;

        currentBlock.transform.localScale = currentScale;
        currentBlock.AddComponent<MoveBlock>().Init(lastSpawner == 0);

        currentBlock.transform.SetParent(tower);
        ChangeBlockColor();

        lastSpawner++;
        lastSpawner %= spawners.Length;
    }

    void ChangeBlockColor()
    {
        float sinValue = transform.position.y / 5.0f;
        float cosValue = transform.position.y / 5.0f;

        lastColor = new Color(sinValue, 0, cosValue);
        currentBlock.GetComponent<Renderer>().material.SetColor("_Color", lastColor);
    }

    public void GameOver(int _score)
    {
        gameOverPanel.SetActive(true);
        Destroy(currentBlock);
        Camera.main.orthographicSize = 30;
        int highscore = PlayerPrefs.GetInt("score");
        if (highscore > _score)
            highScoreTxt.text = "Highscore: " + highscore.ToString();
        else
        {
            highScoreTxt.text = "Highscore: " + _score.ToString();
            PlayerPrefs.SetInt("score", _score);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
