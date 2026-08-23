using UnityEngine;

public class Drage : MonoBehaviour
{
    // 이 코드는 외워야한다
    public LayerMask ground;

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