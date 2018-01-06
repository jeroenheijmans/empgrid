using System;
using System.Security.Cryptography;
using System.Text;

namespace EmpGrid.Api.Models.Core
{
    public class EmpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string TagLine { get; set; }
        public PresenceModel[] Presences { get; set; }

        public string GravatarUrl => string.IsNullOrWhiteSpace(EmailAddress)
            ? ""
            : "https://www.gravatar.com/avatar/" + GetMd5Hash(EmailAddress.Trim().ToLowerInvariant());

        public override string ToString()
        {
            return $"EmpModel {Id} for Name '{Name}' with {Presences?.Length} Presences.";
        }

        private static string GetMd5Hash(string input)
        {
            using (var hasher = MD5.Create())
            {
                var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                var stringBuilder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
