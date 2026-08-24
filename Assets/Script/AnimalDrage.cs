using UnityEngine;

public class AnimalDrage : MonoBehaviour
{
    public LayerMask ground;
    public GameObject StateUI;


    private void OnMouseDown()
    {
        StateUI.SetActive(true);
    }

    private void OnMouseUp()
    {
        StateUI.SetActive(false);
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