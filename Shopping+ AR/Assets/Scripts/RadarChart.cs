using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarChart : MonoBehaviour {

	[SerializeField]
	private Image[] m_panels;
	[SerializeField]
	private float m_fullSize = 100f;
	[SerializeField]
	private float[] m_statusValues;

	void OnValidate() {
		if (m_statusValues.Length != m_panels.Length) {
			return;
		}

		for (int i = 0; i < 5; i++) {
			SetValue (i, m_statusValues[i]);
		}
	}

	public void SetValue (int index, float value) {
		m_statusValues[index] = value;

		Vector2 size = m_panels[index].rectTransform.sizeDelta;
		size.x = m_fullSize * value;
		m_panels[index].rectTransform.sizeDelta = size;

		int pre = (index + m_panels.Length - 1) % m_panels.Length;
		size = m_panels[pre].rectTransform.sizeDelta;
		size.y = m_fullSize * value;
		m_panels[pre].rectTransform.sizeDelta = size;
	}
}
