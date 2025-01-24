using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;

public class DataManager : MonoBehaviour
{
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_LoadData m_rseLoad;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SaveData m_rseSave;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_ClearData m_rseClear;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_PersistentData m_rsoContentSave;
	
	private string m_path;

	private void OnEnable()
	{
		m_rseLoad.Action += Load;
		m_rseSave.Action += Save;
		m_rseClear.Action += Clear;
	}

	private void OnDisable()
	{
		m_rseLoad.Action -= Load;
		m_rseSave.Action -= Save;
		m_rseClear.Action -= Clear;
	}

	private void Start()
	{
		m_path = Application.persistentDataPath + "persistent.data";

		if (File.Exists(m_path)) Load();
		else Save();
	}

	private void Save()
	{
		string data = JsonUtility.ToJson(m_rsoContentSave.Value);
		File.WriteAllText(m_path, data);
	}

	private void Load()
	{
		string data = File.ReadAllText(m_path);
		m_rsoContentSave.Value = JsonUtility.FromJson<PersistentData>(data);
	}

	private void Clear()
	{
		m_rsoContentSave.Value = new();
		Save();
	}
}   