using System;
using Kwetter.Services.FollowService.Application.Common.Models;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces
{
    public interface IDeleteFollowEvent
    {
        Object SendDeleteFollowEvent(FollowEvent followEvent);
    }
}