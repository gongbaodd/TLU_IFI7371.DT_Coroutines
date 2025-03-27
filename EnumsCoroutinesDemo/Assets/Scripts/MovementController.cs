using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class MovementController : MonoBehaviour
{
    [SerializeField] private UIDocument uIDocument;
    [SerializeField] private float maxMoveLength = 20f;

    [SerializeField] private float moveSpeed = .1f;
    private Direction? currentDirection = null;

    private Direction? nextDirection = null;

    private Vector3 startPosition;

    void OnEnable()
    {
        startPosition = transform.position;

        var up = uIDocument.rootVisualElement.Q<Button>("up");
        var down = uIDocument.rootVisualElement.Q<Button>("down");
        var left = uIDocument.rootVisualElement.Q<Button>("left");
        var right = uIDocument.rootVisualElement.Q<Button>("right");

        up.clicked += () => nextDirection = Direction.Up;
        down.clicked += () => nextDirection = Direction.Down;
        left.clicked += () => nextDirection = Direction.Left;
        right.clicked += () => nextDirection = Direction.Right;
    }

    void Update()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        if (nextDirection == null)
        {
            yield break;
        }

        if (startPosition == transform.position)
        {
            currentDirection = nextDirection;
        }

        MoveStep(currentDirection.Value);

        if (Vector3.Distance(startPosition, transform.position) >= maxMoveLength)
        {
            ReverseDirection();
        }

        yield return null;
    }

    void MoveStep(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                transform.position += Vector3.up * moveSpeed;
                break;
            case Direction.Down:
                transform.position += Vector3.down * moveSpeed;
                break;
            case Direction.Left:
                transform.position += Vector3.left * moveSpeed;
                break;
            case Direction.Right:
                transform.position += Vector3.right * moveSpeed;
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }

    void ReverseDirection()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                currentDirection = Direction.Down;
                break;
            case Direction.Down:
                currentDirection = Direction.Up;
                break;
            case Direction.Left:
                currentDirection = Direction.Right;
                break;
            case Direction.Right:
                currentDirection = Direction.Left;
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }
}
