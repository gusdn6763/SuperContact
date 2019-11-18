using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PagingManager : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    ScrollRect cachedScrollRect;
    Coroutine moveCoroutine;


    public ScrollRect CachedScrollRect
    {
        get
        {
            if(cachedScrollRect ==null)
            {
                cachedScrollRect = GetComponent<ScrollRect>();
            }
            return cachedScrollRect;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GridLayoutGroup gridLayoutGroup = CachedScrollRect.content.GetComponent<GridLayoutGroup>();

        CachedScrollRect.StopMovement();

        float pagewidth = -(gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x);

        int pageIndex  = Mathf.RoundToInt(CachedScrollRect.content.anchoredPosition.x / pagewidth);

        float targexX = pageIndex * pagewidth;

         

        if(pageIndex >= 0 && pageIndex <=gridLayoutGroup.transform.childCount -1)
        {
            moveCoroutine = StartCoroutine(MoveCell(new Vector2(targexX, 0), 0.2f));
        }
    }

    IEnumerator MoveCell(Vector2 targetPos,float duration)
    {
        Vector2 initPos = CachedScrollRect.content.anchoredPosition;

        float currentTime = 0;

        while(currentTime <duration)
        {
            Vector2 newPos = initPos + (targetPos - initPos);
            float newTime = currentTime / duration;

            Vector2 destPos = new Vector2(Mathf.Lerp(initPos.x,newPos.x,newTime), newPos.y);
            CachedScrollRect.content.anchoredPosition = destPos;
            currentTime += Time.deltaTime;
            yield return null;        
        }

    }
}
