using UnityEngine;
using System.Collections.Generic;

public class FlappyLevel : MonoBehaviour
{
    public float speed = 1.2f;
    [SerializeField] float endXPos = -9;
    private FlappyGM flappyGM;

    [Header("Ground")]
    [SerializeField] Transform groundPrefab;
    [SerializeField] float groundYPos = -3.5f;
    private float groundDist = 9;
    private List<Transform> grounds;

    [Header("Hands")]
    [SerializeField] FlappyHand handPrefab;
    public float handGap; // Min 2.6, Max 9
    public float handDist;
    private List<FlappyHand> hands;


    private void Start()
    {
        hands = new List<FlappyHand>();
        grounds = new List<Transform>();
        flappyGM = FlappyGM.instance;

        InitializeHands();
        InitializeGround();
    }

    private void Update()
    {
        if (flappyGM.state == FlappyGM.State.Playing)
        {
            MoveHands();
            MoveGround();
        }
    }


    private void InitializeHands()
    {
        for (int i = 0; i < 4; i++)
        {
            SpwanHands(i * handDist);
        }
    }

    private void SpwanHands(float dist)
    {
        FlappyHand flappyHand = Instantiate(handPrefab, Vector3.zero, Quaternion.identity);
        flappyHand.transform.SetParent(this.transform);
        float gap = Mathf.Clamp(Random.Range(handGap - 3, handGap), 2.8f, 9);
        flappyHand.Set(gap, dist);
        hands.Add(flappyHand);
    }

    private void MoveHands()
    {
        for (int i = 0; i < hands.Count; i++)
        {
            hands[i].transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;

            if (hands[i].transform.position.x <= 0 && !hands[i].isScoreAdded)
            {
                flappyGM.AddScore();
                hands[i].isScoreAdded = true;
            }
            if (hands[i].transform.position.x < endXPos)
            {
                float gap = Mathf.Clamp(Random.Range(handGap * 0.6f, handGap), 3, 9);
                hands[i].Set(gap, handDist);
            }
        }
    }



    private void InitializeGround()
    {
        for (int i = 0; i < 3; i++)
        {
            SpwanGround(i * groundDist);
        }
    }

    private void SpwanGround(float dist)
    {
        Transform ground = Instantiate(groundPrefab, Vector3.zero, Quaternion.identity);
        ground.transform.SetParent(this.transform);
        ground.localPosition = new Vector3(dist, groundYPos, 0);
        grounds.Add(ground);
    }

    private void MoveGround()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            grounds[i].transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            if (grounds[i].transform.position.x < endXPos)
            {
                grounds[i].gameObject.SetActive(false);
                grounds[i].localPosition = new Vector3((grounds.Count - 1) * groundDist, groundYPos, 0);
                grounds[i].gameObject.SetActive(true);
            }
        }
    }


    public void Reset()
    {
        for (int i = 0; i < hands.Count; i++)
        {
            float gap = Mathf.Clamp(Random.Range(handGap - 3, handGap), 2.8f, 9);
            hands[i].Set(gap, i * handDist);
        }
    }















    // private List<Transform> groundList;
    // private List<Pipe> pipeList;

    // private int pipePassedCount;
    // private int pipeSpwaned;
    // private float pipeSpwanTimer;
    // private float pipeSpwanTimerMax;
    // private float pipeSpwanXPosition = 4;
    // private float gapSize;

    // // private State state;

    // // [SerializeField] AudioSource scoreSound;

    // // public Transform pipeTr;
    // public Transform handPrefab;
    // public Transform groundPrefab;

    // public enum Difficulty
    // {
    //     Easy,
    //     Medium,
    //     Hard,
    //     Impossible,
    // }

    // private enum State
    // {
    //     WatingToStart,
    //     Playing,
    //     Dead,
    // }

    // private void Awake()
    // {
    //     // state = State.Playing;

    //     pipeSpwanTimerMax = 1.1f;
    //     pipeList = new List<Pipe>();
    //     groundList = new List<Transform>();
    //     SpwanInitialGround();
    //     SetDifficuilty(Difficulty.Easy);
    // }

    // private void Start()
    // {
    //     // Bird.instace.onDie += BirdDye;
    //     // Bird.instace.onStart += OnStart;
    // }

    // // public void BirdDye(object sender, System.EventArgs eventArgs)
    // // {
    // //     state = State.Dead;
    // // }

    // // public void OnStart(object sender, System.EventArgs eventArgs)
    // // {
    // //     state = State.Playing;
    // // }

    // private void Update()
    // {
    //     // if (state == State.Playing)
    //     // {
    //     //     PipeMovement();
    //     //     PipeSpwaning();
    //     //     Ground();
    //     // }

    //     PipeMovement();
    //     PipeSpwaning();
    //     Ground();
    // }

    // private void SpwanInitialGround()
    // {
    //     Transform groundTransform;
    //     groundTransform = Instantiate(groundPrefab, new Vector3(0, -3.5f, 0), Quaternion.identity);
    //     groundList.Add(groundTransform);
    //     groundTransform = Instantiate(groundPrefab, new Vector3(9, -3.5f, 0), Quaternion.identity);
    //     groundList.Add(groundTransform);
    //     groundTransform = Instantiate(groundPrefab, new Vector3(18, -3.5f, 0), Quaternion.identity);
    //     groundList.Add(groundTransform);
    // }

    // private void Ground()
    // {
    //     foreach (Transform groundTransform in groundList)
    //     {
    //         groundTransform.position += new Vector3(-1, 0, 0) * 1.2f * Time.deltaTime;
    //         if (groundTransform.position.x < -9)
    //         {
    //             float rightMousePosition = -10;
    //             for (int i = 0; i < groundList.Count; i++)
    //             {
    //                 if (groundList[i].position.x > rightMousePosition)
    //                 {
    //                     rightMousePosition = groundList[i].position.x;
    //                 }
    //             }

    //             float groundWeidhtHalf = 9f;
    //             groundTransform.position = new Vector3(rightMousePosition + groundWeidhtHalf, groundTransform.position.y, groundTransform.position.z);
    //         }
    //     }
    // }



    // private void PipeSpwaning()
    // {
    //     pipeSpwanTimer -= Time.deltaTime;
    //     if (pipeSpwanTimer <= 0)
    //     {
    //         pipeSpwanTimer += pipeSpwanTimerMax;
    //         float minHeight = gapSize * 0.5f - 4;
    //         float maxHeight = 10 - (gapSize * 0.5f) - 5;
    //         Debug.Log("Min Height : " + minHeight);
    //         Debug.Log("Max Height : " + maxHeight);
    //         float height = Random.Range(minHeight, maxHeight);
    //         PipeGap(height, gapSize, pipeSpwanXPosition);
    //     }
    // }

    // private void PipeMovement()
    // {
    //     for (int i = 0; i < pipeList.Count; i++)
    //     {
    //         Pipe pipe = pipeList[i];
    //         bool isPipeRightToBird = pipe.GetXPosition() > 0;
    //         pipe.Move();
    //         if (isPipeRightToBird && (pipe.GetXPosition() <= 0) && pipe.IsBottom())
    //         {
    //             pipePassedCount++;
    //             // if (scoreSound.isPlaying) scoreSound.Stop();
    //             // scoreSound.Play();
    //         }
    //         if (pipe.GetXPosition() < -3.75f)
    //         {
    //             pipe.DestroySelf();
    //             pipeList.Remove(pipe);
    //             i--;
    //         }
    //     }
    // }

    // private void SetDifficuilty(Difficulty difficulty)
    // {
    //     switch (difficulty)
    //     {
    //         case Difficulty.Easy:
    //             gapSize = 4f;
    //             pipeSpwanTimerMax = 4f;
    //             break;
    //         case Difficulty.Medium:
    //             gapSize = 3f;
    //             pipeSpwanTimerMax = 3f;
    //             break;
    //         case Difficulty.Hard:
    //             gapSize = 2.2f;
    //             pipeSpwanTimerMax = 2.2f;
    //             break;
    //         case Difficulty.Impossible:
    //             gapSize = 1.5f;
    //             pipeSpwanTimerMax = 1.5f;
    //             break;
    //     }
    // }

    // private Difficulty GetDifficulty()
    // {
    //     if (pipeSpwaned >= 21) return Difficulty.Impossible;
    //     if (pipeSpwaned >= 12) return Difficulty.Hard;
    //     if (pipeSpwaned >= 5) return Difficulty.Medium;
    //     else return Difficulty.Easy;
    // }

    // private void PipeGap(float gapY, float gapSize, float xPosition)
    // {
    //     CreatPipe(gapY - gapSize * 0.5f, xPosition, true);
    //     CreatPipe(5 * 2 - gapY - gapSize * 0.5f, xPosition, false);
    //     pipeSpwaned++;
    //     SetDifficuilty(GetDifficulty());
    // }

    // private void CreatPipe(float height, float xPosition, bool creatBottom)
    // {
    //     if (creatBottom)
    //     {
    //         Transform pipe = Instantiate(handPrefab, new Vector2(xPosition, -5f), Quaternion.Euler(0, 0, 180));
    //         // pipe.position = new Vector2(xPosition, -5f);
    //         // pipe.rotation = Quaternion.Euler(0, 0, 180);

    //         // SpriteRenderer pipeSpriteRenderer = pipe.GetComponent<SpriteRenderer>();
    //         // pipeSpriteRenderer.size = new Vector2(1.15f, height);
    //         // BoxCollider2D pipeBoxCollider2D = pipe.GetComponent<BoxCollider2D>();
    //         // pipeBoxCollider2D.offset = new Vector2(0, (height * 0.451928f));
    //         // pipeBoxCollider2D.size = new Vector2(0.7392125f, height * 0.903857f);

    //         Pipe pipe1 = new Pipe(pipe, creatBottom);
    //         pipeList.Add(pipe1);
    //     }
    //     else
    //     {
    //         Transform pipeUP = Instantiate(handPrefab, new Vector2(xPosition, 5f), Quaternion.identity);
    //         // pipeUP.position = new Vector2(xPosition, 5f);
    //         // pipeUP.rotation = Quaternion.Euler(0, 0, 0);

    //         // SpriteRenderer pipeSpriteRendererUP = pipeUP.GetComponent<SpriteRenderer>();
    //         // pipeSpriteRendererUP.size = new Vector2(1.2f, height);
    //         // BoxCollider2D pipeBoxCollider2DUP = pipeUP.GetComponent<BoxCollider2D>();
    //         // pipeBoxCollider2DUP.offset = new Vector2(0.1435127f, (height * 0.5f));
    //         // pipeBoxCollider2DUP.size = new Vector2(0.128284f, height);

    //         Pipe pipe2 = new Pipe(pipeUP, creatBottom);
    //         pipeList.Add(pipe2);
    //     }
    // }

    // public int GetPipePassed()
    // {
    //     return pipePassedCount;
    // }

    // private class Pipe
    // {
    //     private Transform pipeTransform;
    //     private bool isBottom;

    //     public Pipe(Transform pipeTransform, bool isBottom)
    //     {
    //         this.pipeTransform = pipeTransform;
    //         this.isBottom = isBottom;
    //     }

    //     public void Move()
    //     {
    //         pipeTransform.position += new Vector3(-1, 0, 0) * 1.2f * Time.deltaTime;
    //     }

    //     public float GetXPosition()
    //     {
    //         return pipeTransform.position.x;
    //     }

    //     public bool IsBottom()
    //     {
    //         return isBottom;
    //     }

    //     public void DestroySelf()
    //     {
    //         Destroy(pipeTransform.gameObject);
    //     }
    // }
}
