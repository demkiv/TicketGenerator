using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TicketGenerator.UI.Hubs
{
	public class TicketHub : Hub
	{
		public void DisableBoughtSeats(string eventId, string sectorId, string seatId)
		{
			Clients.All.changeBoughtSeatsColor(eventId, sectorId, seatId);
		}
	}
}