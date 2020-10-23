using System;

namespace _3CardPoker.Models
{
	public class Card
	{
		private int rank;
		private char suit;

		public const int ranking_King = 13;
		public const int ranking_Queen = 12;
		public const int ranking_Jack = 11;
		public const int ranking_Ten = 10;
		public const int ranking_Ace = 1;
		public const int ranking_AceHigh = 14;
		public const int ranking_Two = 2;
		public const int ranking_Three = 3;

		public Card(string cardRepresentation)
		{
			parseCard(cardRepresentation);
		}

		public int GetRank()
		{
			return rank;
		}

		public char GetSuit()
		{
			return suit;
		}

		private void parseCard(string cardRepresentation)
		{
			if ((cardRepresentation == null) || (cardRepresentation.Length != 2))
			{
				throw new Exception("Invalid Card");
			}

			char tmpRank = cardRepresentation[0];
			char tmpSuit = cardRepresentation[1];

			/* Validate Rank */
			if(!(Int32.TryParse(tmpRank.ToString(), out rank)))
			{
				switch(tmpRank.ToString())
				{
					case "A":
						rank = ranking_AceHigh;
						break;
					case "T":
						rank = ranking_Ten;
						break;
					case "J":
						rank = ranking_Jack;
						break;
					case "Q":
						rank = ranking_Queen;
						break;
					case "K":
						rank = ranking_King;
						break;
					default:
						throw new Exception("Invalid Rank");
				}
			} else
			{
				if(tmpRank == '1')
				{
					throw new Exception("Aces should be entered as an 'A' instead of 1");
				}
			}

			/* Validate Suit */
			if((tmpSuit == 'h') || (tmpSuit == 'd') || (tmpSuit == 's') || (tmpSuit == 'c'))
			{
				this.suit = tmpSuit;
			} else
			{
				throw new Exception("Invalid Suit");
			}

			/* One more check on rank,  the only condition that should get to this point is if rank==0 */
			if((rank < 2) || (rank > 14))
			{
				throw new Exception("Invalid Rank");
			}
		}
	}
}
