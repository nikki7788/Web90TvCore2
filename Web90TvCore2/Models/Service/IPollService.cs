using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.Service
{
    public interface IPollService
    {

        void ClosePoll(int id);
        void SetVote(int id);


    }
}
