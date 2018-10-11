using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentification.Services
{
    public static class IdentityCertificateExtensions
    {
        public static IIdentityServerBuilder AddCertificate(this IIdentityServerBuilder builder, bool isDev = false, string certificateKey = null)
        {
            if (isDev)
            {
                builder.AddDeveloperSigningCredential(filename: "tempkey.rsa");
            }

            else
            {
                var cert = LoadCertificate(certificateKey);
                if (cert == null)
                    throw new KeyNotFoundException($"Certificate could not be found - certificateKey : {certificateKey}");

                builder.AddSigningCredential(cert);
            }

            return builder;
        }

        public static IIdentityServerBuilder AddOperationalStore(this IIdentityServerBuilder identityServerBuilder, string defaultSchema, string identityConnectionString)
        {
            // this adds the config data from DB (clients, resources)
            return identityServerBuilder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = (DbContextOptionsBuilder obj) =>
                    obj.UseSqlServer(identityConnectionString,
                                         sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
                                     
                options.DefaultSchema = defaultSchema;

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 30;
            });
        }

        private static X509Certificate2 LoadCertificate(string certificateKey)
        {
            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certCollection = certStore
                    .Certificates
                    .Find(X509FindType.FindByThumbprint,
                    certificateKey,
                    false);

                if (certCollection.Count > 0)
                {
                    return certCollection[0];
                }
            }

            return null;
        }
    }
}
