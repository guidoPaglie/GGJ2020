using System.Net.Mail;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestA : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Rigidbody2D _rigidbody;
    public Collider2D _collider2D;

    private Vector3 delta;
    private Vector3 lastPos;

    private bool _clicked = false;

    void FixedUpdate()
    {
        if (!_clicked) return;

        delta = Input.mousePosition - lastPos;

        var mousePositionX = Input.mousePosition.x;
        var mousePositionY = Input.mousePosition.y;

        var mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, 10));
        var direction = (mousePosWorld - transform.position) * delta.magnitude;
        _rigidbody.AddForce(direction);

        if (delta.magnitude <= 0.0f)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        
        lastPos = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastPos = eventData.position;

        _clicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.inertia = 0;

        _clicked = false;
    }
}