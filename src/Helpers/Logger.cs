﻿using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace OpenInNotepadPlusPlus.Helpers
{
	public static class Logger
	{
		private static IVsOutputWindowPane _pane;
		private static IServiceProvider _provider;
		private static string _name;

		public static void Initialize(IServiceProvider provider, string name)
		{
			_provider = provider;
			_name = name;
		}

		public static void Log(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			try
			{
				if (!EnsurePane())
				{
					return;
				}
				_pane.OutputString(DateTime.Now + ": " + message + Environment.NewLine);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
		}

		public static void Log(Exception ex)
		{
			if (ex == null)
			{
				return;
			}
			Log(ex.ToString());
		}

		private static bool EnsurePane()
		{
			if (_pane == null)
			{
				var guid = Guid.NewGuid();
				var output = (IVsOutputWindow)_provider.GetService(typeof(SVsOutputWindow));
				output.CreatePane(ref guid, _name, 1, 1);
				output.GetPane(ref guid, out _pane);
			}

			return _pane != null;
		}
	}
}
