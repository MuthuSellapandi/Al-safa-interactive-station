using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform snapPoint;

    //..........................................................................Functions starts...................................................................................

    #region IBeginDragHandler implementation

    public void OnBeginDrag (PointerEventData eventData)
	{

		itemBeingDragged = gameObject;
		startPosition = transform.position;
		snapPoint = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		//transform.position = eventData.position;
        transform.position = new Vector2(Mathf.Round(eventData.position.x),Mathf.Round(eventData.position.y ));
    }

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		//if(transform.parent == startParent){
		//	transform.position = startPosition;
		//}
	}

    #endregion

}
