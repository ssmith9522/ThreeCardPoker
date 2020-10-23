using System;
using _3CardPoker.Models;

namespace _3CardPoker
{
	class Program
	{
		static void Main(string[] args)
		{
			int playerCount = 0;
			const int maxPlayers = 23;
			const int minPlayers = 1;

			try
			{
				/* Create a new Game */
				ThreeCardPokerGame game = new ThreeCardPokerGame();

				/* Read the number of players */
				//Console.Write("Enter the number of players: ");
				string paramPlayerCount = Console.ReadLine();

				if (Int32.TryParse(paramPlayerCount, out playerCount))
				{
					/* Player count is a valid integer */

					/* Validate the number of players are withing acceptable range */
					if ((playerCount < minPlayers) || (playerCount > maxPlayers))
					{
						throw new Exception("Invalid number of players!  Must be between " + minPlayers.ToString() + " and " + maxPlayers.ToString());
					}
					else
					{
						/* Read Player Data */
						for (int counter = 0; counter < playerCount; counter++)
						{
							/* Get Player & Hand */

							//Console.Write("Enter the Player ID and Hand for Player " + (counter + 1).ToString() + ": "); 
							string player_hand = Console.ReadLine();

							/* Split input into component parts */
							string[] components = player_hand.Split(" ");

							/* Validate the number of components found */
							if (components.Length != ThreeCardPokerGame.cardsPerHand + 1)
							{
								throw new Exception("Invalid input,  expected an integer, followed by " + ThreeCardPokerGame.cardsPerHand.ToString() + " strings, delimited by a space.");
							}

							if (!game.HasPlayer(components[0]))
							{
								/* Let's create a new player */
								Player player = new Player(components[0]);
								game.AddPlayer(player);

								for (int idx = 1; idx <= ThreeCardPokerGame.cardsPerHand; idx++)
								{
									/* Add card to player's hand */
									player.TakeCard(components[idx]);
								}
							}
							else
							{
								throw new Exception("Player " + components[0] + " is already in the game!");
							}
						}

						/* All Players and their Hands have been entered,  let's determine the winner */
						Console.WriteLine(game.GetWinner());
					}
				}
				else
				{
					throw new Exception("Expected an integer representing the number of players!");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
		}
	}
}
