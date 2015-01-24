using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJam2015
{
    public interface GameState
    {
        void ChangeState();

        void Initialize();
    }
}
