﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MRS.Models.MRSMobileModels.ViewModels.Account;
using MRS.Services.Mobile.Data.Contracts;

namespace MRS.Spa.Web.Hubs
{
    public class UserLocationsHub : Hub
    {
        private readonly IUserService userService;

        public UserLocationsHub(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task SendUserLocations(string date)
        {
            var users = (await userService.GetAllUsersWithLocationsWithDateAsync<UserViewModel>(DateTime.Parse(date))).ToList();

            await this.Clients.Caller.SendAsync("SendUserLocations",
                users.ToList()
                );
        }
    }
}
