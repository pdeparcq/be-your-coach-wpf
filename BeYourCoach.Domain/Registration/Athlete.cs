﻿using System;
using Conditions.Guards;
using Deparcq.Common.Domain;

namespace BeYourCoach.Domain.Registration
{
    public class Athlete : EntityBase
    {
        public FullName FullName { get; private set; }

        public Athlete(FullName fullName)
        {
            Check.If(fullName).IsNotNull();

            FullName = fullName;
        }
    }
}
