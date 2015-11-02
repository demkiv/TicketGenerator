using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(TicketGenerator.UI.Startup))]
namespace TicketGenerator.UI
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}