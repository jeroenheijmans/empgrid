﻿using EmpGrid.Domain.Core;

namespace EmpGrid.Api.Models.Core
{
    public class PresenceModel
    {
        public string Url { get; set; }
        public string MediumId { get; set; }

        public override string ToString()
        {
            return $"PresenceModel for '{MediumId}', URL: {Url}";
        }
    }
}
