using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(ReventureModdingHelper.BuildInfo.Description)]
[assembly: AssemblyDescription(ReventureModdingHelper.BuildInfo.Description)]
[assembly: AssemblyCompany(ReventureModdingHelper.BuildInfo.Company)]
[assembly: AssemblyProduct(ReventureModdingHelper.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + ReventureModdingHelper.BuildInfo.Author)]
[assembly: AssemblyTrademark(ReventureModdingHelper.BuildInfo.Company)]
[assembly: AssemblyVersion(ReventureModdingHelper.BuildInfo.Version)]
[assembly: AssemblyFileVersion(ReventureModdingHelper.BuildInfo.Version)]
[assembly: MelonInfo(typeof(ReventureModdingHelper.RMH), ReventureModdingHelper.BuildInfo.Name, ReventureModdingHelper.BuildInfo.Version, ReventureModdingHelper.BuildInfo.Author, ReventureModdingHelper.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

[assembly: MelonGame("Pixelatto", "Reventure")]