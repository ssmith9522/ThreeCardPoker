using System;

namespace _3CardPoker.Models
{
	public class Player
	{
		private Hand hand;
		private int playerID;

		public Player(string id)
		{
			int player_id;

			if (int.TryParse(id, out player_id))
			{
				this.playerID = player_id;
				hand = new Hand();
			} else
			{
				throw new Exception("PlayerID is required to be an integer");
			}
		}

		public string PlayerID
		{
			get
			{
				return playerID.ToString();
			}
		}

		public void TakeCard(string cardRepresentation)
		{
			hand.AddCard(cardRepresentation);
		}

		public Hand GetHand()
		{
			return hand;
		}
	}
}
