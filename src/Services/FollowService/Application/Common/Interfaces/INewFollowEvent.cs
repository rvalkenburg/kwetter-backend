using System;
using Kwetter.Services.FollowService.Application.Common.Models;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces
{
    public interface INewFollowEvent
    {
        Object SendNewFollowEvent(FollowEvent followEvent);
    }
}