using System;
using UnityEngine;

namespace Scriptables
{
    public class RuntimeScriptableObject<T> : ScriptableObject
	{
		public event Action<T> OnChanged;
		
		private T m_value;
        public T Value
        {
            get => m_value;
            set
            {
                m_value = value;
                OnChanged?.Invoke(value);
            }
        }
    }
}
