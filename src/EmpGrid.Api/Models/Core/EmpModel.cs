﻿using System;

namespace EmpGrid.Api.Models.Core
{
    public class EmpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string TagLine { get; set; }
        public PresenceModel[] Presences { get; set; }

        public override string ToString()
        {
            return $"EmpModel {Id} for Name '{Name}' with {Presences?.Length} Presences.";
        }
    }
}
