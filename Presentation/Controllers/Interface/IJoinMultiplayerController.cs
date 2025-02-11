﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Presentation.Controllers.Interface
{
    public interface IJoinMultiplayerController
    { 
        Task ValidateForm(string username, string joinCode);
    }
}
