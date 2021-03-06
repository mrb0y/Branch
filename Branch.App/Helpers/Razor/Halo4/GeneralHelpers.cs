﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Branch.Core.Game.Halo4.Enums;
using Branch.Core.Game.Halo4.Models._343.DataModels;

namespace Branch.App.Helpers.Razor.Halo4
{
	public class GeneralHelpers
	{
		public static int GetHighestCsr(CommonModels.CurrentSkillRank topSkillRank)
		{
			if (topSkillRank == null || topSkillRank.CompetitiveSkillRank == null)
				return 0;

			return (int) topSkillRank.CompetitiveSkillRank;
		}

		public static string GetCsrLevelUrl(int csrLevel, string size = "medium", string version = "v1")
		{
			return string.Format("https://assets.halowaypoint.com/games/h4/csr/{0}/{1}/{2}.png", version, size, csrLevel);
		}

		public static string GetRawAssetUrl(CommonModels.ImageUrl imageUrl, string size = "medium")
		{
			return string.Format("{0}{1}", GlobalStorage.H4Manager.RegisteredWebApp.Settings[imageUrl.BaseUrl],
				imageUrl.AssetUrl.Replace("{size}", size));
		}

		public static string GetRawAssetUrl(CommonModels.ImageUrl imageUrl, int size)
		{
			return string.Format("{0}{1}", GlobalStorage.H4Manager.RegisteredWebApp.Settings[imageUrl.BaseUrl],
				imageUrl.AssetUrl.Replace("{size}", size.ToString(CultureInfo.InvariantCulture)));
		}

		public static string GetPlayerModelUrl(string gamertag, string size = "large", string pose = "fullbody")
		{
			return GlobalStorage.H4Manager.GetPlayerModelUrl(gamertag, size, pose);
		}

		public static string RemoveGuestIdentifier(string gamertag)
		{
			return Regex.Replace(gamertag, @"\((\d)\)", "", RegexOptions.None);
		}

		public static Tuple<string, string> GetGameVictoryStatus(Result result, bool completed)
		{
			if (!completed)
				return new Tuple<string, string>("dnf", "DNF");

			switch (result)
			{
				case Result.Draw:
					return new Tuple<string, string>("dnf", "DNF");

				case Result.Lost:
					return new Tuple<string, string>("los", "Lost");

				case Result.Won:
					return new Tuple<string, string>("win", "Won");

				default:
					return new Tuple<string, string>("", "");
			}
		}

		public static CsrType GetPlaylistCsrOrientation(PlaylistModel playlist)
		{
			var playlistOrientation = GlobalStorage.H4Manager.GetPlaylistOrientation(playlist.Id);
			if (playlistOrientation == null)
				return CsrType.Unknown;

			return playlistOrientation.IsTeam ? CsrType.Team : CsrType.Individual;
		}
	}
}
