﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IProgramLanguageService
    {
        List<string> GetLanguages();
        public Guid GetID();
    }
}
