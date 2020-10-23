using System.Collections.Generic;

namespace _3CardPoker.Models
{
	public class Hand
	{
		private List<Card> cards;

		public Hand()
		{
			cards = new List<Card>();
		}

		public void AddCard(string cardRepresentation)
		{
			cards.Add(new Card(cardRepresentation));
		}

		public List<Card> GetCards()
		{
			return cards;
		}
	}
}
