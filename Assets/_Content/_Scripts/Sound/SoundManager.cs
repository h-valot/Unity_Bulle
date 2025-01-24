using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

public class AudioManager : MonoBehaviour
{
    [Header("Tweakable values")]
	[InfoBox("Number of GameObject create on start for the sound", InfoMessageType.None)]
    [SerializeField] private int m_startingAudioObjectsCount = 30;

	[FoldoutGroup("Internal references")][SerializeField] AudioMixerGroup m_musicMixerGroup;
	[FoldoutGroup("Internal references")][SerializeField] AudioMixerGroup m_soundMixerGroup;
	[FoldoutGroup("Internal references")][SerializeField] private Transform m_musicParent;
	[FoldoutGroup("Internal references")][SerializeField] private Transform m_sfxParent;

	[FoldoutGroup("Scriptable")] [SerializeField] private RSE_PlaySoundAt m_rsePlaySoundAt;

	private Queue<AudioSource> m_sockets = new Queue<AudioSource>();

	private void OnEnable()
    {
        m_rsePlaySoundAt.Action += PlaySoundAt;
    }
	
    private void OnDisable()
    {
        m_rsePlaySoundAt.Action -= PlaySoundAt;
    }

    private void Start()
    {
		for (int i = 0; i < m_startingAudioObjectsCount; i++)
        {
            m_sockets.Enqueue(CreateSocket(i % 2 == 0 ? SoundType.SFX : SoundType.MUSIC));
        }
    }

    private void PlaySoundAt(SSO_Sound sound, Vector3 position)
    {
        AudioSource socket;
        if (m_sockets.Count <= 0) socket = CreateSocket(sound.Type);
        else socket = m_sockets.Dequeue();

        socket.transform.position = position;
		socket.pitch = sound.Pitch;
        socket.volume = sound.Volume;
        socket.clip = sound.Clip;
        socket.Play();
        StartCoroutine(AddSocket(socket));
    }

    private IEnumerator AddSocket(AudioSource socket)
    {
        yield return new WaitForSeconds(socket.clip.length);
        m_sockets.Enqueue(socket);
    }

    private AudioSource CreateSocket(SoundType type)
    {
        AudioSource socket = new GameObject("Socket").AddComponent<AudioSource>();
        socket.transform.SetParent(type == SoundType.SFX ? m_sfxParent : m_musicParent);
        socket.outputAudioMixerGroup = m_soundMixerGroup;
        m_sockets.Enqueue(socket);
        return socket;
    }
}