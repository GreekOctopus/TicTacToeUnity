using UnityEngine;
using UnityEngine.InputSystem; // Imports the New Input System namespace
using TicTacToe.Core;
using System.Collections;

public class UnityGameController : MonoBehaviour
{
    [Header("3D Prefabs")]
    public GameObject prefabX;
    public GameObject prefabO;

    [Header("Settings")]
    public GameMode gameMode = GameMode.HumanVsAi;
    public Difficulty difficulty = Difficulty.Hard;
    public CellState aiPlayer = CellState.O;

    private Game game;
    private GameObject[,] spawnedPieces = new GameObject[3, 3];

    void Start()
    {
        game = new Game(gameMode, difficulty, aiPlayer);
        Debug.Log("Tic Tac Toe Game Logic Initialized.");

        if (gameMode == GameMode.HumanVsAi && aiPlayer == CellState.X)
        {
            game.TriggerAiMove();
            UpdateVisualBoard();
        }
    }

    void Update()
    {
        if (game.IsGameOver) return;

        // New Input System check for left mouse button click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Camera.main == null)
            {
                Debug.LogError("Error: No Camera found with the tag 'MainCamera'! Please tag your camera in the Inspector.");
                return;
            }

            // Get mouse position and cast a 3D ray into the scene
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Raycast hit GameObject: '{hit.collider.gameObject.name}'");

                BoardCell cell = hit.collider.GetComponent<BoardCell>();
                if (cell != null)
                {
                    Debug.Log($"Clicked BoardCell: Row {cell.row}, Col {cell.col}. Attempting logic move...");
                    bool moveSuccessful = game.MakeMove(cell.row, cell.col);

                    if (moveSuccessful)
                    {
                        Debug.Log("Move accepted by engine.");
                        UpdateVisualBoard();
                    }
                    else
                    {
                        Debug.LogWarning("Move rejected by engine (cell occupied or game over).");
                    }
                }
                else
                {
                    Debug.Log("Clicked an object, but it does not have a 'BoardCell' script attached to it.");
                }
            }
        }
    }

    private void UpdateVisualBoard()
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                CellState state = game.Board.GetCell(r, c);

                if (state != CellState.Empty && spawnedPieces[r, c] == null)
                {
                    BoardCell physicalCell = FindPhysicalCell(r, c);
                    if (physicalCell != null)
                    {
                        GameObject prefabToSpawn = (state == CellState.X) ? prefabX : prefabO;

                        GameObject spawned = Instantiate(prefabToSpawn, physicalCell.SpawnPosition, Quaternion.identity);
                        StartCoroutine(ScaleUpAnimation(spawned.transform, Vector3.one * 0.8f, 0.25f));

                        spawnedPieces[r, c] = spawned;
                        Debug.Log($"Visual piece spawned at logic cell: Row {r}, Col {c}");
                    }
                }
            }
        }

        if (game.IsGameOver)
        {
            AnnounceGameFinished();
        }
    }

    private BoardCell FindPhysicalCell(int row, int col)
    {
#if UNITY_2023_1_OR_NEWER
        foreach (var cell in FindObjectsByType<BoardCell>(FindObjectsSortMode.None))
#else
        foreach (var cell in FindObjectsOfType<BoardCell>())
#endif
        {
            if (cell.row == row && cell.col == col)
                return cell;
        }
        return null;
    }

    private void AnnounceGameFinished()
    {
        if (game.Winner == CellState.Empty)
        {
            Debug.Log("Game Over: It's a Draw!");
        }
        else
        {
            Debug.Log($"Game Over: Winner is {game.Winner}!");
        }
    }

    private IEnumerator ScaleUpAnimation(Transform target, Vector3 targetScale, float duration)
    {
        float time = 0;
        target.localScale = Vector3.zero;

        while (time < duration)
        {
            float t = time / duration;
            float scaleProgress = Mathf.Sin(t * Mathf.PI * 0.5f);

            target.localScale = Vector3.Lerp(Vector3.zero, targetScale, scaleProgress);
            time += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale;
    }
}