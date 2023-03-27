using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SnakeControl : MonoBehaviour
{
    // Snake state
    private enum SnakeState
    {
        Alive,
        Dead
    }
    private SnakeState state;
    // Possible snake movement directions:
    private enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }
    private Direction snakeDirection;
    private Vector2Int snakePosition;
    // Time interval between frames/state update
    private float moveTimer;
    private float moveTimerMax;
    public float maxSpeed = 4.0f;
    // Calling LevelGrid script for apple(s) spawning
    private LevelGrid levelGrid;
    // Grid boundaries [to be passed as a parameter to spawn function]
    public Collider2D gridArea;
    // Snake lenght/number of body parts [size of the list]
    private int snakeSize;
    // List of positions (head + body)
    private List<SnakeMovePosition> snakeMovePositionList;
    // List of snake body parts (body only)
    private List<SnakeBodyPart> snakeBodyPartList;
    // Screen size and boundaries for continuos borders
    int screenWidth, screenHeight;
    private float screenTop, screenBottom, screenLeft, screenRight;
    // To handle snake death & game over screen
    public static event Action onSnakeDeath;

    // Called once to init variables & references
    private void Awake()
    {
        state = SnakeState.Alive;

        snakePosition = new Vector2Int(0, 0);
        moveTimerMax = 1f / maxSpeed;
        moveTimer = moveTimerMax;
        snakeDirection = Direction.Right;

        levelGrid = new LevelGrid();

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeSize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>();

        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Start()
    {
        // Get screen boundaries (depending on Camera object)
        screenTop = (int)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y;
        screenBottom = (int)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        screenLeft = (int)Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).x;
        screenRight = (int)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    // Called once per frame
    private void Update()
    {
        switch (state)
        {
            case SnakeState.Alive:
                // If the snake is alive, you can play
                HandleInput();
                HandleGridMovement();
                break;
            case SnakeState.Dead:
                // if the snake is dead, you can't
                break;
        }
    }

    // Getting user input (keyboard)
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (snakeDirection != Direction.Down)
            {
                snakeDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (snakeDirection != Direction.Up)
            {
                snakeDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (snakeDirection != Direction.Right)
            {
                snakeDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (snakeDirection != Direction.Left)
            {
                snakeDirection = Direction.Right;
            }
        }
    }

    // Moving the snake according to the input obtained
    private void HandleGridMovement()
    {
        // Delta time: interval between two frames (current to next) 
        // -> time interval between two state updates
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveTimerMax)
        {
            moveTimer -= moveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            // Init the class calling a constructor w/ arguments
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, snakePosition, snakeDirection);
            // Add to the list the snake (head) position at index = 0
            snakeMovePositionList.Insert(0, snakeMovePosition);

            // Moving on the grid
            Vector2Int snakeDirectionVector;
            switch (snakeDirection)
            {
                default:
                case Direction.Right: snakeDirectionVector = new Vector2Int(+1, 0); break;
                case Direction.Left: snakeDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: snakeDirectionVector = new Vector2Int(0, +1); break;
                case Direction.Down: snakeDirectionVector = new Vector2Int(0, -1); break;
            }
            // Position list dimension = snake size (num. of parts)
            if (snakeMovePositionList.Count >= snakeSize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }
            // Give the snake the right direction
            snakePosition += snakeDirectionVector;
            // If it exceedes the screen borders:
            snakePosition = ValidatedPosition(snakePosition);
            
            transform.position = new Vector3(snakePosition.x, snakePosition.y);
            //Debug.Log("DEBUG: position changed");
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(snakeDirectionVector) - 90);
            //Debug.Log("DEBUG: righ angle assigned");

            UpdateSnakeBodyParts();
        }
    }
    // -> utility function:
    // n.1
    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            // Add new positions each frame
            if (snakeBodyPartList.Count == snakeMovePositionList.Count)
            {
                snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
            }
            //Debug.Log("DEBUG: New position added");
        }
    }
    // n.2
    private float GetAngleFromVector(Vector2Int dir)
    {
        // Get the angle n where tan(n) = y/x and convert the value from radiant to degree
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // If the value is negative, add 360 (full angle rotation)
        if (n < 0) n += 360;
        return n;
    }

    // Return the list of positions of the snake (head + body)
    public List<Vector2Int> GetPositionList()
    {
        List<Vector2Int> positionList = new List<Vector2Int>();
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            positionList.Add(snakeMovePosition.GetGridPosition());
        }
        return positionList;
    }

    // Detect collision with food object 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the snake object collided with the food object
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("DEBUG: collision triggered");
            // then destroy the apple object
            UnityEngine.Object.Destroy(other.gameObject);
            // + increase score
            GameHandler.IncreaseScore();
            // + increase snake body size
            snakeSize++;
            // creating new body parts accordingly
            CreateSnakeBodyPart();
            Debug.Log("DEBUG: new snake size: " + snakeSize);
            // and make another food object spawn at a random position
            levelGrid.SpawnFood(gridArea);
        } else if (other.gameObject.CompareTag("Death"))
        {
            Debug.Log("Death");
            state = SnakeState.Dead;
            onSnakeDeath?.Invoke();
        }
    }
    // -> utility function:
    private void CreateSnakeBodyPart()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
        //Debug.Log("DEBUG: New body part created");
    }

    // BODY PART CLASS
    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        // constructor:
        public SnakeBodyPart(int bodyIndex)
        {
            // Create new body object
            GameObject snakeBodyObject = new GameObject("Body", typeof(SpriteRenderer));
            snakeBodyObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakebodySprite;
            // Add a tag to check for collision
            snakeBodyObject.tag = "Death";
            // Add Box Collider 2D component
            BoxCollider2D bodyCollider = snakeBodyObject.AddComponent<BoxCollider2D>();
            bodyCollider.size = new Vector2(0.4f, 0.4f);
            bodyCollider.isTrigger = true;
            // Sorting layers (one under the other)
            snakeBodyObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyObject.transform;
            // TODO: to be fixed
            transform.position = new Vector3(10, 10);
        }

        // class method:
        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            // Assign actual position 
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x,
                                             snakeMovePosition.GetGridPosition().y);

            // Assign actual angle (rotation)
            float angle;
            // TODO: to be cleaned up 
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up: // Currently going Up
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0;
                            break;
                        case Direction.Left: // Previously was going Left
                            angle = 0 + 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Right: // Previously was going Right
                            angle = 0 - 45;
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                    }
                    break;
                case Direction.Down: // Currently going Down
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180;
                            break;
                        case Direction.Left: // Previously was going Left
                            angle = 180 - 45;
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                        case Direction.Right: // Previously was going Right
                            angle = 180 + 45;
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
                case Direction.Left: // Currently going to the Left
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = +90;
                            break;
                        case Direction.Down: // Previously was going Down
                            angle = 180 - 45;
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                        case Direction.Up: // Previously was going Up
                            angle = 45;
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
                case Direction.Right: // Currently going to the Right
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        case Direction.Down: // Previously was going Down
                            angle = 180 + 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Up: // Previously was going Up
                            angle = -45;
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                    }
                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    // MOVE POSITION CLASS
    private class SnakeMovePosition
    {

        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int snakePosition;
        private Direction direction;

        // constructor:
        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int snakePosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.snakePosition = snakePosition;
            this.direction = direction;
        }

        // class methods:
        public Vector2Int GetGridPosition()
        {
            return snakePosition;
        }
        public Direction GetDirection()
        {
            return direction;
        }
        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }
    }

    // Implement continous screen
    public Vector2Int ValidatedPosition(Vector2Int snakePosition)
    {
        // TODO: find an alternative solution for integer vectors (grid)
        if (snakePosition.x < screenLeft)
        {
            snakePosition.x = (int)screenRight;
        }
        if (snakePosition.x > screenRight )
        {
            snakePosition.x = (int)screenLeft;
        }
        if (snakePosition.y < screenBottom - 2)
        {
            snakePosition.y = (int)screenTop + 2;
        }
        if (snakePosition.y > screenTop + 2)
        {
            snakePosition.y = (int)screenBottom - 2;
        }

        return snakePosition;
    }
}

