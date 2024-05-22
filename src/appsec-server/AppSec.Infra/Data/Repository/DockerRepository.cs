using Amazon.Runtime.Internal.Util;
using AppSec.Domain.Interfaces.IRepository;
using Docker.DotNet;
using Docker.DotNet.Models;
using ICSharpCode.SharpZipLib.Tar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppSec.Infra.Data.Repository
{
    public class DockerRepository : IContainerRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<DockerRepository> logger;
        public DockerRepository(ILogger<DockerRepository> logger, IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Obsolete]
        public async Task Build(string dockerfilecontet, string target, Dictionary<string, string> buildArgs, string image, string tag, string project_dir, string dockerhost = "host.docker.internal:host-gateway", CancellationToken cancellationToken = default)
        {
            try
            {
                logger.LogDebug("Call DockerRepository.Build");
                if (string.IsNullOrEmpty(dockerfilecontet))
                {
                    throw new ArgumentNullException(nameof(dockerfilecontet));
                }
                if (string.IsNullOrEmpty(project_dir))
                {
                    throw new ArgumentNullException(nameof(project_dir));
                }
                try
                {
                    var file = System.IO.Path.Combine(project_dir, "Dockerfile.sonar");
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    File.WriteAllText(file, dockerfilecontet);
                }
                catch (Exception e)
                {
                    logger.LogError("Exception: " + e.Message);
                }
                finally
                {
                    logger.LogInformation("Executing finally block.");
                }

                var sonar_url = configuration.GetSection("sonar:url").Value ?? throw new Exception("not found sonar url");
                buildArgs.Add("SONARQUBE_URL", sonar_url);
                var dockerdaemon = configuration.GetSection("docker:tcp").Value ?? throw new Exception("not found docker daemon");
                using var client = new DockerClientConfiguration(new Uri(dockerdaemon)).CreateClient();
                using var tarball = CreateTarballForDockerfileDirectory(project_dir);
                using var imageBuildResponse = await client.Images.BuildImageFromDockerfileAsync(tarball, new ImageBuildParameters
                {
                    Dockerfile = "Dockerfile.sonar",
                    Tags = new List<string> { $"{image}:{tag}" },
                    Target = target,
                    NoCache = true,
                    BuildArgs = buildArgs,
                    Platform = "linux/amd64",
                    ExtraHosts = new List<string> { dockerhost },
                    Pull = "always",
                });
                using (System.IO.StreamReader sr = new System.IO.StreamReader(imageBuildResponse))
                {
                    logger.LogDebug("Read DockerResponse");
                    string line;
                    while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                    {
                        logger.LogInformation(line);
                    }
                }

            }
            catch (Exception e)
            {
                logger.LogError($"{nameof(DockerRepository)}{e.Message}");
                throw e;
            }
        }

        public void DeleteContainer()
        {
            // Delete container
        }

        public void StartContainer()
        {
            // Start container
        }

        public void StopContainer()
        {
            // Stop container
        }
        private static Stream CreateTarballForDockerfileDirectory(string directory)
        {
            var tarball = new MemoryStream();
            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            using var archive = new TarOutputStream(tarball)
            {
                //Prevent the TarOutputStream from closing the underlying memory stream when done
                IsStreamOwner = false
            };

            foreach (var file in files)
            {
                //Replacing slashes as KyleGobel suggested and removing leading /
                string tarName = file.Substring(directory.Length).Replace('\\', '/').TrimStart('/');

                //Let's create the entry header
                var entry = TarEntry.CreateTarEntry(tarName);
                using var fileStream = File.OpenRead(file);
                entry.Size = fileStream.Length;
                archive.PutNextEntry(entry);

                //Now write the bytes of data
                byte[] localBuffer = new byte[32 * 1024];
                while (true)
                {
                    int numRead = fileStream.Read(localBuffer, 0, localBuffer.Length);
                    if (numRead <= 0)
                        break;

                    archive.Write(localBuffer, 0, numRead);
                }

                //Nothing more to do with this entry
                archive.CloseEntry();
            }
            archive.Close();

            //Reset the stream and return it, so it can be used by the caller
            tarball.Position = 0;
            return tarball;
        }
    }
}
