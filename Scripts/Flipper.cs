using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IFlippable))]

public class Flipper : MonoBehaviour
{
    [SerializeField] private Image _image;

    private IFlippable _flippable;

    private void Awake()
    {
        _flippable = GetComponent<IFlippable>();
    }

    private void LateUpdate()
    {
        if (_flippable.Direction.x > 0)
        {
             _image.transform.localScale = new Vector3(Mathf.Abs(_image.transform.localScale.x), _image.transform.localScale.y, _image.transform.localScale.z);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_flippable.Direction.x < 0)
        {
            _image.transform.localScale = new Vector3(-Mathf.Abs(_image.transform.localScale.x), _image.transform.localScale.y, _image.transform.localScale.z);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}