using System;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.ViewModels.Program
{
    public class ProgramGenderRequestViewModel : ApplicationRequest
    {
        public int GenderId { get; set; }
    }
}
