﻿using CodingWithCalvin.OpenInNotepadPlusPlus.Shared.Commands;
using CodingWithCalvin.OpenInNotepadPlusPlus.Shared.Dialogs;
using CodingWithCalvin.OpenInNotepadPlusPlus.Shared.Helpers;

#if X64
using CodingWithCalvin.OpenInNotepadPlusPlus.Vsix.x64;
#elif X86
using CodingWithCalvin.OpenInNotepadPlusPlus.Vsix.x86;
#endif

using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;


namespace CodingWithCalvin.OpenInNotepadPlusPlus.Shared
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[InstalledProductRegistration("#110", "#112", "1.1.12", IconResourceID = 400)]
	[ProvideOptionPage(typeof(SettingsDialogPage), VsixConstants.Name, "General",101,111, true, new string[0], ProvidesLocalizedCategoryName = false)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[Guid(PackageGuids.guidPackageString)]
	public sealed class OpenInNotepadPlusPlusPackage : AsyncPackage
	{
		protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await JoinableTaskFactory.SwitchToMainThreadAsync();

			var settings = (SettingsDialogPage)this.GetDialogPage(typeof(SettingsDialogPage));
			
			Logger.Initialize(this, VsixConstants.Name);
			OpenExecutableCommand.Initialize(this, settings);
		}
	}
}
