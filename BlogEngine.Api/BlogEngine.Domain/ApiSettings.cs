﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Domain
{
    public class ApiSettings
    {
        public Environment Environment { get; set; } = new Environment();
        public Values Values { get; set; } = new Values();
    }

    public class Values
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationTime { get; set; }
    }

    public class Environment
    {
        public string CurrentEnvironment { get; set; }

        public bool IsProduction()
        {
            return CurrentEnvironment == "Production";
        }

        public bool IsStaging()
        {
            return CurrentEnvironment == "Staging";
        }

        public bool Development()
        {
            return CurrentEnvironment == "Development";
        }
    }
}
