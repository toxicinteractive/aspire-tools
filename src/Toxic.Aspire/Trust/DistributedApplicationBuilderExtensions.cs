using Microsoft.Extensions.Configuration;

namespace Toxic.Aspire.Trust;

public static class DistributedApplicationBuilderExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        /// <summary>
        /// Trusts CA certificates in container and javascript app resources for easier HTTPS development scenarios in run mode (not publish mode).
        /// Add custom certificate file paths or specify a value in CUSTOM_HOST_CA.
        /// </summary>
        /// <param name="addHostCertificates">If true will also add all the trusted certificates on the host machine to the resource.</param>
        /// <param name="certFileNames">Specify optional custom certificate file paths (absolute).</param>
        public IDistributedApplicationBuilder WithHostCertificates(bool addHostCertificates = true, params string[] certFileNames)
        {
            IResourceBuilder<CertificateAuthorityCollection>? certs = null;

            var envCaFile = builder.Configuration.GetValue<string>("CUSTOM_HOST_CA");
            var certsToAdd = new List<string>();

            if (certFileNames.Length > 0)
            {
                certsToAdd.AddRange(certFileNames);
            }

            if (!string.IsNullOrWhiteSpace(envCaFile))
            {
                certsToAdd.Add(envCaFile);
            }

            if (certsToAdd.Count > 0)
            {
                certs = builder.AddCertificateAuthorityCollection("host-ca");

                foreach (var cert in certsToAdd)
                {
                    certs.WithCertificatesFromFile(cert);
                }
            }

            // projects (when running in a devcontainer)
            foreach (var project in builder.Resources.OfType<ProjectResource>())
            {
                builder.ConfigureResourceCerts(project, certs, addHostCertificates);
            }

            // containers
            foreach (var container in builder.Resources.OfType<ContainerResource>())
            {
                builder.ConfigureResourceCerts(container, certs, addHostCertificates);
            }

            // executables (javascript apps for example)
            foreach (var app in builder.Resources.OfType<ExecutableResource>())
            {
                builder.ConfigureResourceCerts(app, certs, addHostCertificates);
            }

            return builder;
        }

        private void ConfigureResourceCerts<TResource>(
            TResource resource,
            IResourceBuilder<CertificateAuthorityCollection>? certs,
            bool addHostCertificates) where TResource : IResourceWithArgs, IResourceWithEnvironment
        {
            var resourceBuilder = builder.CreateResourceBuilder(resource);
            var isConfigured = false;

            if (certs != null)
            {
                resourceBuilder.WithCertificateAuthorityCollection(certs);
                isConfigured = true;
            }

            if (addHostCertificates)
            {
                resourceBuilder.WithCertificateTrustScope(CertificateTrustScope.System);
                isConfigured = true;
            }

            if (isConfigured)
            {
                resourceBuilder.WithDeveloperCertificateTrust(false);
            }
        }
    }
}
