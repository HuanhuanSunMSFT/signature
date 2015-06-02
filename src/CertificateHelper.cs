﻿using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CodeTitans.Signature
{
    /// <summary>
    /// Helper class providing info about certificates.
    /// </summary>
    static class CertificateHelper
    {
        /// <summary>
        /// Loads all certificates that belong to current user.
        /// </summary>
        public static IEnumerable<X509Certificate2> LoadUserCertificates(string subjectName)
        {
            var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                certStore.Open(OpenFlags.ReadOnly);

                var result = new List<X509Certificate2>();
                var certificates = string.IsNullOrEmpty(subjectName) ? certStore.Certificates : certStore.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);
                foreach (var cert in certificates)
                {
                    result.Add(cert);
                }
                return result;
            }
            finally
            {
                certStore.Close();
            }
        }

        /// <summary>
        /// Gets the list of timestamp servers.
        /// </summary>
        public static IEnumerable<string> LoadTimestampServers()
        {
            return new[]
            {
                "http://time.certum.pl",
                "http://timestamp.verisign.com/scripts/timstamp.dll",
                "http://timestamp.comodoca.com/authenticode",
                "https://timestamp.geotrust.com/",
                "http://ca.signfiles.com/TSAServer.aspx",
                "http://mindprod.com/jgloss/timestamp.html"
            };
        }

        /// <summary>
        /// Gets the list of names for available hash algorithms.
        /// </summary>
        public static IEnumerable<NamedHashAlgorithm> LoadHashAlgorithms()
        {
            return new[]
            {
                new NamedHashAlgorithm("SHA1", "http://www.w3.org/2000/09/xmldsig#sha1"),
                new NamedHashAlgorithm("SHA256", "http://www.w3.org/2001/04/xmlenc#sha256"),
                new NamedHashAlgorithm("SHA512", "http://www.w3.org/2001/04/xmlenc#sha512")
            };
        }
    }
}
