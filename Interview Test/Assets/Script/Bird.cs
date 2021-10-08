using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool _isPressed = false;

    private Rigidbody2D _bird;
    private Rigidbody2D _hook;

    private GameHandler _gameHandler;

    private float _releaseTime = 0.1f;
    private float _destroyTime = 1.0f;
    private float _maxDragDistance = 2.0f;

    public bool IsThrown;

    void Start()
    {
        _bird = GetComponent<Rigidbody2D>();
        _hook = GameObject.Find("Hook").GetComponent<Rigidbody2D>();
        _gameHandler = GameObject.FindObjectOfType<GameHandler>();
    }

    void Update()
    {
        if (_isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, _hook.position) > _maxDragDistance)
            {
                _bird.position = _hook.position + (mousePos - _hook.position).normalized * _maxDragDistance;
            }
            else
            {
                _bird.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        _isPressed = true;
        _bird.isKinematic = true;
    }

    private void OnMouseUp()
    {
        _isPressed = false;
        _bird.isKinematic = false;
        IsThrown = true;

        StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {
        yield return new WaitForSeconds(_releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
        _gameHandler.DecreaseBirdCount();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(this.gameObject);
    }
}
