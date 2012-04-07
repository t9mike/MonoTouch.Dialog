using System;

namespace MonoTouch.Dialog
{
	public interface IBackButtonCaption
	{
		/// <summary>
		/// Return "" or null to get the default, the previous view controller's Title.
		/// </summary>
		string BackButtonCaption { get; }
	}
}

