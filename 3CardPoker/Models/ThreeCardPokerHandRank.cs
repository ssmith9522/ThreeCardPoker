namespace _3CardPoker.Models
{
	public class ThreeCardPokerHandRank
	{
		/*
			ThreeCardPoker Ranking Logic:
		  
			The hand ranking is achieved by accumulating an integer value for the various scoring options in the hand that will yield an outcome consistent with the rules of the game.

			The integer values below don't have significant meaning other than the values where chosen in a way that gaurentees higher scoring options always outweighs the accumulation of the lower scoring options.

			The ranking calculation is started by traversing the card ranks in the hand in descending order and adding the values based on the following:
				rank of card 1 (highest rank when in descending order)  * 200 = value between 200 & 2600
				rank of card 2 * 20 = value between 20 & 260
				rank of card 1 * 2 = value between 2 & 26
				MAX VALUE BASED SOLELY ON CARD RANKING:  2886
				NOTE:  Multiplying by 200 and 20 instead of 100 and 10 so the (second ranking) high card of KING can't be beat by a combination of a QUEEN + JACK  (120 + 11) = 131 which would be higher than KING = 130.

				Additional Scoring:
				For Two of a Kind,  Add 30000  (Must be a value greating than 2886 from high card ranking) Also add the rank of the pair * 3000 to settle ties with a pair.
				For a flush, Add 100000 (Must be a value greater than high card ranking + two of a kind,  must also be high enough,  that when added to the value of a straight,  exceeds the value for three of a kind)
				For a streight Add 250000 (Must be a value greater than high card ranking + two of a kind + flush,  must also be high enough,  that when added to the value of a flush,  exceeds the value for three of a kind)
				For three of a kind,  Add 300000

				The sum of all these scoring options yield a hand ranking where the highest rank should be the winner of the game.
		*/

		public const int ThreeOfAKind = 300000;
		public const int Straight = 250000;
		public const int Flush = 100000;
		public const int TwoOfAKind = 30000;
		public const int PairRankMultiplier = 3000;

		private int ranking = 0;

		public int Ranking {
			get
			{
				return ranking;
			}
			set
			{
				ranking = value;
			}
		}
	}
}
