using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public int row;
    public int col;

    // Changed Y offset from 0.3f to 0.8f to lift the pieces out of the cubes
    public Vector3 SpawnPosition => transform.position + new Vector3(0, 0.8f, 0);
}