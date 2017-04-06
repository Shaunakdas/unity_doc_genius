using UnityEngine;
using System.Collections;
using TexDrawLib;

public class TexLengthCounter : TEXPerCharacterBase {

	public float m_Count;
	public float m_ApproxCount;

	// This is applied for each character (and commands)
	protected override string Subtitute(string match, float m_Factor)
	{

		if (match.Length == 1)
			m_Count++;
		// In case if you wondering:
		// Debug.Log(match);
		return match;
	}

	protected override void OnBeforeSubtitution (float count)
	{
		// This is just an appoximate. For example a command will counted as 1. if you need it.
		m_ApproxCount = count;
		//count is total
		m_Count = 0;
	}

}
