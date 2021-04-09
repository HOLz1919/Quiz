using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.Dictionaries
{
    public enum MatchStatus
    {
        WAITING = 1, // oczekujący
        ACTIVE = 2,  // w trakcie rozgrywki
        ENDED =3  // Zakończony

    }
}
