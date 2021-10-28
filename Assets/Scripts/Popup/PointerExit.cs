using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerExit : MonoBehaviour, IPointerExitHandler {

	public PopupController controller;

	public void OnPointerExit(PointerEventData eventData) {
		controller.OnPointerExit(eventData);
	}
}
