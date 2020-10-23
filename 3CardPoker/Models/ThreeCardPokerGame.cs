using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3CardPoker.Models
{
	public class ThreeCardPokerGame : CardGame
	{
		public const int cardsPerHand = 3;
		public const int pair = 2;
		public const int threeOfAKind = 3;
		public const bool verbose = false;

		override public string GetWinner()
		{
			StringBuilder winningPlayers = new StringBuilder("");

			if(players.Count == 0)
			{
				/* No players in the game,  throw an exception */
				throw new Exception("Nobody is playing this game!");
			}
			if(players.Count == 1)
			{
				/* If there is only one player,  no need to evaluate cards */
				winningPlayers.Append(players.Keys.First());
			} else
			{
				/* Let's calculate the hand rankings */
				Dictionary<string, ThreeCardPokerHandRank> handRankings = CalculateHandRankings();

				/* Let's evaluate our winners from the hand rankings */
				int winningRank = 0;
				foreach(string key in handRankings.Keys)
				{
					ThreeCardPokerHandRank handRanking = handRankings[key];
					if(winningRank==0)
					{
						/* We've sorted the ranks descending, so the first one should be our winner */
						winningPlayers.Append(key);
						winningRank = handRanking.Ranking;
					} else
					{
						/* Now we're just looking for ties */
						if(winningRank == handRanking.Ranking)
						{
							/* We have a tie */
							winningPlayers.Append(" " + key);
						}
					}
				}
			}

			return winningPlayers.ToString();
		}

		private Dictionary<string, ThreeCardPokerHandRank> CalculateHandRankings()
		{
			Dictionary<string, ThreeCardPokerHandRank> handRankings = new Dictionary<string, ThreeCardPokerHandRank>();

			/* Loop through each player and calculate their hand */
			foreach(Player player in players.Values)
			{
				if(verbose)
				{
					Console.WriteLine("Evaluating Hand for Player: " + player.PlayerID);
				}

				/* Calculate Player Hand & Save to Rankings */
				handRankings.Add(player.PlayerID, CalculateHandRanking(player.GetHand()));
			}

			/* Order Rankings so Winners are first */
			handRankings = handRankings.OrderByDescending(o => o.Value.Ranking).ToDictionary(x => x.Key, y => y.Value);

			return handRankings;
		}

		private static ThreeCardPokerHandRank CalculateHandRanking(Hand hand)
		{
			/* This may not be the most eloquent logic,  but it does evaluate all the scoring with a single iteration through the data. */

			ThreeCardPokerHandRank handRanking = new ThreeCardPokerHandRank();
			int lastRank = 0;
			char lastSuit = ' ';

			int longestRankRun = 0;
			int longestSuitRun = 0;
			int longestKind = 1;
			int logestKindRank = 0;
			bool hasAce = false;

			/* Order the hand so we can evaluate the cards in their rank order descending */
			List<Card> cards = hand.GetCards().OrderByDescending(o => o.GetRank()).ToList();

			/* Iterate through the cards and evaluate potential scoring */
			for(int idx=0; idx<cards.Count; idx++)
			{
				int cardRank	= cards[idx].GetRank();
				char cardSuit	= cards[idx].GetSuit();

				if (verbose)
				{
					Console.WriteLine("Card " + idx.ToString() + ": " + cardRank + cardSuit + "      Score: (" + (cardRank * (2 * ((int)Math.Pow(10, (cardsPerHand - idx) - 1)))).ToString() + ")");
				}


				/* check for an Ace,  this may come into play later when checking for a straight */
				if (cardRank == Card.ranking_AceHigh)
				{
					hasAce = true;
				}

				/* Update Scoring by High Card */
				handRanking.Ranking += cardRank * (2 * ((int)Math.Pow(10, (cardsPerHand - idx)-1)));

				/* Evaluate Same Kind */
				if(cardRank == lastRank)
				{
					longestKind++;

					/* If we end up with a pair,  we need to capture the rank of the pair to settle any ties between other players with a pair. */
					if(longestKind > 1)
					{
						logestKindRank = lastRank;
					}
				}

				/* Evaluate for a Straight */
				if(cardRank == lastRank-1)
				{
					/* Increment the run counter */
					longestRankRun++;

					if ((cardRank == Card.ranking_Two) && (lastRank == Card.ranking_Three) && hasAce)
					{
						/* The above logic is a special case for a straight with Ace being low card.  
						 * Because have the ranking for Ace is at 14 (For Ace High),  we have an Ace low straight when the current card is two,  
						 * the previouis card was a three and the first card was an Ace based on evaluation sort order */

						/* Give the counter a bonus increment to account for the Ace */
						longestRankRun++;
					}
				} else {
					/* Reset Run Counter */
					longestRankRun = 1;
				}
				lastRank = cardRank;

				/* Evaluate for a Flush */
				if(cardSuit == lastSuit)
				{
					/* Increment the suit counter */
					longestSuitRun++;
				} else
				{
					/* Reset the suit counter */
					longestSuitRun = 1;
				}
				lastSuit = cardSuit;
			}

			/* Check to see if we have a pair or three of a kind */
			if(longestKind == threeOfAKind)
			{
				handRanking.Ranking += ThreeCardPokerHandRank.ThreeOfAKind;
			} else if(longestKind == pair)
			{
				handRanking.Ranking += ThreeCardPokerHandRank.TwoOfAKind;
				handRanking.Ranking += logestKindRank * ThreeCardPokerHandRank.PairRankMultiplier;
			}

			/* Check to see if we have a Straight */
			if (longestRankRun == cardsPerHand)
			{
				handRanking.Ranking += ThreeCardPokerHandRank.Straight;
			}

			/* Check to see if we have a Flush */
			if (longestSuitRun == cardsPerHand)
			{
				handRanking.Ranking += ThreeCardPokerHandRank.Flush;
			}

			if(verbose)
			{
				Console.WriteLine("Score for Hand: " + handRanking.Ranking.ToString());
			}

			return handRanking;
		}
	}
}
