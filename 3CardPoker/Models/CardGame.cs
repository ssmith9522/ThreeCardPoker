using System.Collections.Generic;

namespace _3CardPoker.Models
{
	public abstract class CardGame
	{
		protected Dictionary<string, Player> players;

		public CardGame()
		{
			players = new Dictionary<string, Player>();
		}

		public void AddPlayer(Player player)
		{
			players.Add(player.PlayerID, player);
		}

		public bool HasPlayer(string playerID)
		{
			return players.ContainsKey(playerID);
		}
		public abstract string GetWinner();
	}
}
