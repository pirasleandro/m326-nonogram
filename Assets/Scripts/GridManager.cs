using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridManager : MonoBehaviour {

    private static GridManager gridManager;

    [SerializeField] private int _size;
    [SerializeField] private float _scale = 1;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private NumHint _numHintPrefab;
    [SerializeField] private Transform _tileParent;
    [SerializeField] private Transform _numHintParent;
    [SerializeField] private Color _colorTheme = Color.white;
    public Color ColorTheme {
        get { return _colorTheme; }
        set {
            _colorTheme = value;
            UpdateColor();
        }
    }
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _puzzleComplete;
    [SerializeField] private int[][] puzzle = new int[][] {
        new int[] {1,1,1,1,1,1,1,0,0,0},
        new int[] {1,1,1,0,1,0,1,0,0,0},
        new int[] {1,1,1,1,1,0,0,0,0,1},
        new int[] {1,1,0,0,1,1,1,0,1,1},
        new int[] {1,1,0,1,1,1,0,0,1,1},
        new int[] {1,1,0,0,0,1,0,1,1,1},
        new int[] {1,0,0,0,0,0,1,1,1,1},
        new int[] {0,0,0,0,0,0,0,1,1,1},
        new int[] {0,0,0,0,0,0,0,0,1,1},
        new int[] {1,1,0,0,0,0,0,0,0,0},
    };
    private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
    private Dictionary<Vector2, NumHint> numHints = new Dictionary<Vector2, NumHint>();

    private GridManager() {

    }

    public static GridManager GetInstance() {
        return gridManager;
    }

    private void Start() {
        gridManager = this;
        if (puzzle.Length != _size || puzzle[0].Length != _size) throw new System.Exception("puzzle and _size not equal");
        _puzzleComplete.SetActive(false);
        GenerateGrid();
        GenerateNumHints();
        PositionCam();
    }

    public void CheckForWin() {
        int[][] attempt = new int[_size][];
        for (int x = 0; x < _size; x++) {
            attempt[x] = new int[_size];
            for (int y = 0; y < _size; y++) {
                attempt[x][y] = GetTile(x, y).State == Tile.TileState.COLORED ? 1 : 0;
            }
        }
        if (IsEqual(attempt, puzzle)) _puzzleComplete.SetActive(true);
    }

    private bool IsEqual(int[][] arr1, int[][] arr2) {
        bool output = true;
        for (int i = 0; i < arr1.Length; i++) {
            for (int j = 0; j < arr1[i].Length; j++) {
                if (arr1[i][j] != arr2[i][j]) output = false;
            }
        }
        return output;
    }

    private void GenerateGrid() {
        for (int x = 0; x < _size; x++) {
            for (int y = 0; y < _size; y++) {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector3(x*_scale, y*_scale), Quaternion.identity, _tileParent);
                spawnedTile.name = $"Tile {x}/{y}";
                spawnedTile.transform.localScale = new Vector3(_scale*3, _scale*3, _scale*3);
                tiles[new Vector2(x*_scale, y*_scale)] = spawnedTile;
            }
        }
    }

    private void GenerateNumHints() {
        for (int i = 0; i < _size; i++) {
            ArrayList colHints = ComputeHints(puzzle[i]);
            for (int j = 0; j < colHints.Count; j++) {
                SpawnNumHint(new Vector3(i*_scale, (_size+j)*_scale), $"HintNum col {i} ({j+1})", (int) colHints[j]);
            }
            if (colHints.Count == 0) SpawnNumHint(new Vector3(i*_scale, _size*_scale), $"HintNum col {i} (1)", 0);
            ArrayList rowHints = ComputeHints(GetRow(i));
            rowHints.Reverse();
            for (int j = 0; j < rowHints.Count; j++) {
                SpawnNumHint(new Vector3((-1-j)*_scale, i*_scale), $"HintNum row {i} ({j+1})", (int) rowHints[j]);
            }
            if (rowHints.Count == 0) SpawnNumHint(new Vector3(-1*_scale, i*_scale), $"HintNum row {i} (1)", 0);
        }
    }

    private ArrayList ComputeHints(int[] array) {
        ArrayList output = new ArrayList();
        foreach (string s in string.Join(null, array).Split('0')) {
            if (s.Length > 0) output.Add(s.Length);
        }
        return output;
    }

    private int[] GetRow(int i) {
        int[] output = new int[puzzle.Length];
        for (int j = 0; j < puzzle.Length; j++) {
            output[j] = puzzle[j][i];
        }
        return output;
    }

    private void PositionCam() {
        _cam.transform.position = new Vector3((_size*_scale)/2 -0.5f, (_size*_scale)/2 - 0.5f, -10);
    }

    private void SpawnNumHint(Vector3 vector3, string name, int value) {
        NumHint spawnedNumHint = Instantiate(_numHintPrefab, vector3, Quaternion.identity, _numHintParent);
        spawnedNumHint.name = name;
        spawnedNumHint.transform.localScale = new Vector3(_scale*3, _scale*3);
        spawnedNumHint.Value = value;
        numHints[vector3] = spawnedNumHint;
    }

    public Tile GetTile(int x, int y) {
        return GetTile(new Vector2(x, y));
    }

    public Tile GetTile(Vector2 pos) {
        pos.x *= _scale;
        pos.y *= _scale;
        return tiles[pos];
    }

    public void UpdateColor() {
        foreach (Tile tile in tiles.Values) {
            tile.UpdateColor();
        }
        foreach (NumHint numHint in numHints.Values) {
            numHint.UpdateColor();
        }
    }
}
