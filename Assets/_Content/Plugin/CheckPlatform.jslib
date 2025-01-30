// .jslib is an file extention of unity
// it permit javascript functions to be called in c#

mergeInto(LibraryManager.library, 
{	
	IsMobile: function()
	{
		return Module.SystemInfo.mobile;
	}
});