using System;
using Kwetter.Services.ProfileService.Application.Common.Models;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces
{
    public interface INewProfileEvent
    {
        Object SendNewProfileEvent(ProfileDto profile);
    }
}