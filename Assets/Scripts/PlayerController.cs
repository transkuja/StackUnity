using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Vector3 cameraOffset;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject canvas;

    float errorAllowedForPerfects = 1.0f; // en %

    float errorAllowed = 3.0f; // en %

    int combo = 0;
    int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            canvas.GetComponentInChildren<Text>().text = score.ToString();
        }
    }

    private void Start()
    {
        combo = 0;
        Score = 0;
        cameraOffset = transform.position;    
    }

    void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            OnClickProcess();
        }
	}


    void OnClickProcess()
    {
        // Pose le bloc
        Destroy(gameManager.currentBlock.GetComponent<MoveBlock>());


        // S'il est dessus
        if (CheckEdges())
        {
            Score++;
            gameManager.transform.position += gameManager.defaultScale.y * Vector3.up;
            transform.position += gameManager.defaultScale.y * Vector3.up;
            gameManager.SpawnNewBlock();
        }
        else
        {
            gameManager.GameOver();
        }
    }

    bool CheckPerfectEdges()
    {
        if (Mathf.Abs(gameManager.currentBlock.transform.position.x - gameManager.tower.GetChild(gameManager.tower.childCount - 2).position.x) < errorAllowedForPerfects
            && Mathf.Abs(gameManager.currentBlock.transform.position.z - gameManager.tower.GetChild(gameManager.tower.childCount - 2).position.z) < errorAllowedForPerfects)
        {
            Debug.Log("Perfect!");
            // s'il est parfaitement dessus ==> combo ==> play ok sound ==> recalage
            combo++;
            if (combo >= 8)
            {
                if (gameManager.currentScale.x < gameManager.currentScale.z)
                    gameManager.currentScale.x *= 1.2f;
                else
                    gameManager.currentScale.z *= 1.2f;

                gameManager.currentBlock.transform.localScale = gameManager.currentScale;
            }
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.coinFX, combo);
            gameManager.currentBlock.transform.position = gameManager.tower.GetChild(gameManager.tower.childCount - 2).position + Vector3.up * 0.5f;

            return true;
        }
        return false;
    }

    bool CheckEdges()
    {
        if (gameManager.lastSpawner == 1 && Mathf.Abs(gameManager.currentBlock.GetComponent<Collider>().bounds.extents.x - gameManager.tower.GetChild(gameManager.tower.childCount - 2).GetComponent<Collider>().bounds.extents.x) < errorAllowed)
        {
            Debug.Log("edge1");
            if (!CheckPerfectEdges())           
                CutTheRope(true);
            return true;
        }
        else if (gameManager.lastSpawner == 0 && Mathf.Abs(gameManager.currentBlock.GetComponent<Collider>().bounds.extents.z - gameManager.tower.GetChild(gameManager.tower.childCount - 2).GetComponent<Collider>().bounds.extents.z) < errorAllowed)
        {
            Debug.Log("edge2");
            if (!CheckPerfectEdges())
                CutTheRope(false);
            return true;
        }
        // Lose
        return false;

    }
    
    // Easter egg
    void CutTheRope(bool _onX)
    {
        float diff;
        combo = 0;
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.failFX, combo);

        if (_onX)
        {
            diff = gameManager.currentBlock.transform.position.x - gameManager.tower.GetChild(gameManager.tower.childCount - 2).transform.position.x;
            if (diff > 0)
            {
                Debug.Log(1);
                gameManager.currentBlock.transform.localScale -= Vector3.right * diff;
                gameManager.currentBlock.transform.position = new Vector3(diff / 2, gameManager.currentBlock.transform.position.y, gameManager.currentBlock.transform.position.z);

                gameManager.currentScale.x = gameManager.currentBlock.transform.localScale.x;


                GameObject fallingPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallingPart.transform.localScale = new Vector3(diff, gameManager.currentScale.y, gameManager.currentScale.z);
                fallingPart.transform.position = new Vector3((diff + gameManager.currentScale.x)/2, gameManager.currentBlock.transform.position.y, gameManager.currentBlock.transform.position.z); ;
                fallingPart.AddComponent<Rigidbody>();
            }
            else
            {
                Debug.Log(2);
                diff = -diff;
                gameManager.currentBlock.transform.localScale -= Vector3.right * diff;
                gameManager.currentBlock.transform.position = new Vector3(-diff / 2, gameManager.currentBlock.transform.position.y, gameManager.currentBlock.transform.position.z);

                gameManager.currentScale.x = gameManager.currentBlock.transform.localScale.x;


                GameObject fallingPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallingPart.transform.localScale = new Vector3(diff, gameManager.currentScale.y, gameManager.currentScale.z);
                fallingPart.transform.position = new Vector3(-(diff + gameManager.currentScale.x) / 2, gameManager.currentBlock.transform.position.y, gameManager.currentBlock.transform.position.z); ;
                fallingPart.AddComponent<Rigidbody>();
            }
        }
        else
        {
            diff = gameManager.currentBlock.transform.position.z - gameManager.tower.GetChild(gameManager.tower.childCount - 2).transform.position.z;
            if (diff > 0)
            {
                Debug.Log(3);
                gameManager.currentBlock.transform.localScale -= Vector3.forward * diff;
                gameManager.currentBlock.transform.position = new Vector3(gameManager.currentBlock.transform.position.x, gameManager.currentBlock.transform.position.y, diff / 2);

                gameManager.currentScale.z = gameManager.currentBlock.transform.localScale.z;


                GameObject fallingPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallingPart.transform.localScale = new Vector3(gameManager.currentScale.x, gameManager.currentScale.y, diff);
                fallingPart.transform.position = new Vector3(gameManager.currentBlock.transform.position.x, gameManager.currentBlock.transform.position.y, (diff + gameManager.currentScale.z) / 2); ;
                fallingPart.AddComponent<Rigidbody>();
            }
            else
            {
                Debug.Log(4);
                diff = -diff;
                gameManager.currentBlock.transform.localScale -= Vector3.forward * diff;
                gameManager.currentBlock.transform.position = new Vector3(gameManager.currentBlock.transform.position.x, gameManager.currentBlock.transform.position.y, -diff / 2);

                gameManager.currentScale.x = gameManager.currentBlock.transform.localScale.x;


                GameObject fallingPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallingPart.transform.localScale = new Vector3(gameManager.currentScale.x, gameManager.currentScale.y, diff);
                fallingPart.transform.position = new Vector3(gameManager.currentBlock.transform.position.x, gameManager.currentBlock.transform.position.y, -(diff + gameManager.currentScale.z) / 2); ;
                fallingPart.AddComponent<Rigidbody>();
            }
        }

        gameManager.transform.position = gameManager.currentBlock.transform.position;

    }
}
