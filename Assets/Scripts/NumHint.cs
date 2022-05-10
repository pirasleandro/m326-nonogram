using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumHint : MonoBehaviour {

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _firstDigitRenderer;
    [SerializeField] private SpriteRenderer _secondDigitRenderer;
    [SerializeField] private SpriteRenderer _markedOverlayRenderer;
    [SerializeField] private Sprite _blankSprite;
    [SerializeField] private Sprite[] _singleDigitSprites;
    [SerializeField] private Sprite[] _digitSprites;
    [SerializeField] private Selectable _selectable;
    private GridManager gridManager = GridManager.GetInstance();

    private int value = 0;
    public int Value {
        get { return value; }
        set { this.value = value;}
    }

    private bool marked = false;
    public bool Marked {
        get { return marked; }
        set { this.marked = value; }
    }

    private void Start() {
        SetSprite(value);
    }

    public void SetSprite(int num) {
        if (num < 10) {
            _spriteRenderer.sprite = _singleDigitSprites[num];
        } else {
            _firstDigitRenderer.sprite = _digitSprites[num/10];
            _secondDigitRenderer.sprite = _digitSprites[num%10]; // TODO
        }
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            marked = !marked;
            _markedOverlayRenderer.enabled = !_markedOverlayRenderer.enabled;
        }
    }

    public void UpdateColor() {
        _selectable.UpdateColor(gridManager.ColorTheme);
    }
}
