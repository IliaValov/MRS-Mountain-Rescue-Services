using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using MRS.Models.MRSMobileModels.ViewModels.Account;
using MRS.Services.Mobile.Data;
using MRS.Services.Mobile.Data.Contracts;

namespace MRS.Spa.Web.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    public class UserLocationsHub : Hub
    {
        private IUserService userService;
        private DateTime dateTime = DateTime.UtcNow;
        private int usersCount = 0;
        private int locationsCount = 0;


        public UserLocationsHub(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task SendUserLocations(string date)
        {
            this.dateTime = DateTime.Parse(date);

            var users = (await this.userService.GetAllUsersWithLocationsWithDateAsync<UserViewModel>(dateTime)).ToList();

            int currentUsersCount = users.Count;
            int currentLocationsCount = users.Select(x => x.Locations.Count).Sum();

            if (this.usersCount != currentUsersCount || this.locationsCount != currentLocationsCount)
            {
                await this.Clients.Caller.SendAsync("SendUserLocations",
                                                   users.ToList()
                                                   );

                this.usersCount = currentUsersCount;
                this.locationsCount = currentLocationsCount;
            }

        }
    }
}
