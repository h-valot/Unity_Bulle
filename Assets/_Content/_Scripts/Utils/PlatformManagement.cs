using System;
using System.Runtime.InteropServices;
// DO NOT DELETE THOSE USINGS

public static class PlatformManagement
{
#if (UNITY_WEBGL && !UNITY_EDITOR)
		[DllImport("__Internal")] private static extern bool IsMobile();
#endif

	public static bool s_isMobile;

	public static void Initialize()
	{
		bool output = false;

#if (UNITY_WEBGL && !UNITY_EDITOR)
        output = IsMobile();
#endif

		s_isMobile = output;
	}
}