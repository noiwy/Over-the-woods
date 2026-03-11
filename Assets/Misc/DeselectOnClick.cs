using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectOnClick : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Запускаем корутину, которая подождет один кадр
        StartCoroutine(DeselectAtEndOfFrame());
    }

    IEnumerator DeselectAtEndOfFrame()
    {
        // Ждем конца кадра
        yield return new WaitForEndOfFrame();
        // Сбрасываем выделение
        EventSystem.current.SetSelectedGameObject(null);
    }

}
