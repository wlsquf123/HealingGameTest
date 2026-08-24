using UnityEngine;

public class Drage : MonoBehaviour
{
    public LayerMask ground;

    private void OnMouseDown()
    {
        if (CompareTag("Animal"))
        {
            Debug.Log("표시");
        }
    }

    private void OnMouseUp()
    {
        if (CompareTag("Animal"))
        {
            Debug.Log("꺼짐");
        }
    }

    private void OnMouseDrag()
    {
        if (Time.timeScale == 0) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
        {
            transform.position = hit.point + Vector3.up;
        }
    }
}