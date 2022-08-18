using KIT.Minio.Settings.Interfaces;

namespace KIT.Minio.Commands.SaveFileWithSharing.Models
{
    /// <summary>
    ///     Minio sharing files settings fake
    /// </summary>
    public class MinioSharingFilesSettingsFake : IMinioSharingFilesSettings
    {
        /// <summary>
        ///     Test exparation time value
        /// </summary>
        private const int exparationInSeconds = 600;

        public MinioSharingFilesSettingsFake()
        {
            ExpirationInSeconds = exparationInSeconds;
        }

        /// <summary>
        ///     Test exparation time
        /// </summary>
        public int ExpirationInSeconds { get; set; }
    }
}
