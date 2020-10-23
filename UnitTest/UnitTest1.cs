using Microsoft.VisualStudio.TestTools.UnitTesting;

using _3CardPoker.Models;

namespace UnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void Example1()
		{
			ThreeCardPokerGame game = new ThreeCardPokerGame();

			Player player1 = new Player("0");
			player1.TakeCard("2c");
			player1.TakeCard("As");
			player1.TakeCard("4d");

			game.AddPlayer(player1);

			Player player2 = new Player("1");
			player2.TakeCard("Kd");
			player2.TakeCard("5h");
			player2.TakeCard("6c");

			game.AddPlayer(player2);

			Player player3 = new Player("2");
			player3.TakeCard("Jc");
			player3.TakeCard("Jd");
			player3.TakeCard("9s");

			game.AddPlayer(player3);

			Assert.AreEqual(game.GetWinner(), "2");
		}

		[TestMethod]
		public void Example2_TieGame()
		{
			ThreeCardPokerGame game = new ThreeCardPokerGame();

			Player player1 = new Player("0");
			player1.TakeCard("Qc");
			player1.TakeCard("Kc");
			player1.TakeCard("4s");

			game.AddPlayer(player1);

			Player player2 = new Player("1");
			player2.TakeCard("Ah");
			player2.TakeCard("2c");
			player2.TakeCard("Js");

			game.AddPlayer(player2);

			Player player3 = new Player("2");
			player3.TakeCard("3h");
			player3.TakeCard("9h");
			player3.TakeCard("Th");

			game.AddPlayer(player3);

			Player player4 = new Player("3");
			player4.TakeCard("Tc");
			player4.TakeCard("9c");
			player4.TakeCard("3c");

			game.AddPlayer(player4);

			Assert.AreEqual(game.GetWinner(), "2 3");
		}

		[TestMethod]
		public void AceLowStraight()
		{
			ThreeCardPokerGame game = new ThreeCardPokerGame();

			Player player1 = new Player("0");
			player1.TakeCard("8h");
			player1.TakeCard("Qh");
			player1.TakeCard("Kh");

			game.AddPlayer(player1);

			Player player2 = new Player("1");
			player2.TakeCard("3h");
			player2.TakeCard("As");
			player2.TakeCard("2d");

			game.AddPlayer(player2);

			Player player3 = new Player("2");
			player3.TakeCard("9d");
			player3.TakeCard("9s");
			player3.TakeCard("Kd");

			game.AddPlayer(player3);

			Assert.AreEqual(game.GetWinner(), "1");
		}

		[TestMethod]
		public void AceHighStraight()
		{
			ThreeCardPokerGame game = new ThreeCardPokerGame();

			Player player1 = new Player("0");
			player1.TakeCard("Ac");
			player1.TakeCard("Qh");
			player1.TakeCard("Kh");

			game.AddPlayer(player1);

			Player player2 = new Player("1");
			player2.TakeCard("3h");
			player2.TakeCard("As");
			player2.TakeCard("2d");

			game.AddPlayer(player2);

			Player player3 = new Player("2");
			player3.TakeCard("9d");
			player3.TakeCard("9s");
			player3.TakeCard("Kd");

			game.AddPlayer(player3);

			Assert.AreEqual(game.GetWinner(), "0");
		}

		[TestMethod]
		public void StraightFlush()
		{
			ThreeCardPokerGame game = new ThreeCardPokerGame();

			Player player1 = new Player("0");
			player1.TakeCard("Ac");
			player1.TakeCard("Qh");
			player1.TakeCard("Kh");

			game.AddPlayer(player1);

			Player player2 = new Player("1");
			player2.TakeCard("2h");
			player2.TakeCard("4h");
			player2.TakeCard("3h");

			game.AddPlayer(player2);

			Player player3 = new Player("2");
			player3.TakeCard("9d");
			player3.TakeCard("9s");
			player3.TakeCard("Kd");

			game.AddPlayer(player3);

			Assert.AreEqual(game.GetWinner(), "1");
		}

		[TestMethod]
		public void Pairs()
		{
			ThreeCardPokerGame game = new ThreeCardPokerGame();

			Player player1 = new Player("0");
			player1.TakeCard("Ac");
			player1.TakeCard("Kd");
			player1.TakeCard("Kh");

			game.AddPlayer(player1);

			Player player2 = new Player("1");
			player2.TakeCard("2h");
			player2.TakeCard("6c");
			player2.TakeCard("3h");

			game.AddPlayer(player2);

			Player player3 = new Player("2");
			player3.TakeCard("9d");
			player3.TakeCard("Ks");
			player3.TakeCard("Kc");

			game.AddPlayer(player3);

			Assert.AreEqual(game.GetWinner(), "0");
		}

		[TestMethod]
		public void InputValidationPlayer()
		{
			Assert.ThrowsException<System.Exception>(() => new Player("A"));
		}

		[TestMethod]
		public void InputValidationAceFormat()
		{
			Assert.ThrowsException<System.Exception>(() => new Card("1d"));
		}
	}
}
