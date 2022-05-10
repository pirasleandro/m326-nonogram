using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _defaultSprite, _coloredSprite, _markedSprite;
    [SerializeField] private AudioClip _colorSound, _markSound;
    [SerializeField] private Selectable _selectable;
    private GridManager gridManager = GridManager.GetInstance();
    private AudioManager audioManager = AudioManager.GetInstance();

    public enum TileState {
        BLANK,
        COLORED,
        MARKED
    }

    private TileState state = TileState.BLANK;
    public TileState State {
        get { return state; }
        set {
            state = value;
            UpdateSprite();
        }
    }

    private static TileState firstSelected = TileState.COLORED;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            ToggleColor();
            firstSelected = state;
        }
        if (Input.GetMouseButtonDown(1)) {
            ToggleMark();
            firstSelected = state;
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
            gridManager.CheckForWin();
        }
    }

    private void OnMouseEnter() {
        if (Input.GetMouseButton(0) && state != firstSelected) {
            ToggleColor();
        }
        if (Input.GetMouseButton(1) && state != firstSelected) {
            ToggleMark();
        }
    }

    private void ToggleColor() {
        switch (state) {
            case TileState.BLANK: State = TileState.COLORED; break;
            case TileState.COLORED: State = TileState.BLANK; break;
            case TileState.MARKED: State = TileState.COLORED; break;
        }
        audioManager.Play(_colorSound);
    }

    private void ToggleMark() {
        switch (state) {
            case TileState.BLANK: // fallthrough
            case TileState.COLORED: State = TileState.MARKED; break;
            case TileState.MARKED: State = TileState.BLANK; break;
        }
        audioManager.Play(_markSound);
    }

    private void UpdateSprite() {
        switch (state) {
            case TileState.BLANK: _spriteRenderer.sprite = _defaultSprite; break;
            case TileState.COLORED: _spriteRenderer.sprite = _coloredSprite; break;
            case TileState.MARKED: _spriteRenderer.sprite = _markedSprite; break;
        }
    }

    public void UpdateColor() {
        _selectable.UpdateColor(gridManager.ColorTheme);
    }
}
