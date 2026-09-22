using NLog;
using NzbDrone.Core.Parser.Model;

namespace NzbDrone.Core.DecisionEngine.Specifications
{
    public class MultiSeasonSpecification : IDownloadDecisionEngineSpecification
    {
        private readonly Logger _logger;

        public MultiSeasonSpecification(Logger logger)
        {
            _logger = logger;
        }

        public SpecificationPriority Priority => SpecificationPriority.Default;
        public RejectionType Type => RejectionType.Permanent;

        public virtual DownloadSpecDecision IsSatisfiedBy(RemoteEpisode subject, ReleaseDecisionInformation information)
        {
            var isCompleteSeries = subject.ParsedEpisodeInfo.IsMultiSeason || subject.ParsedEpisodeInfo.IsCompleteSeries;

            if (isCompleteSeries && information?.SearchCriteria?.InteractiveSearch != true)
            {
                _logger.Debug("Multi-season/complete-series release {0} rejected for automatic search", subject.Release.Title);
                return DownloadSpecDecision.Reject(DownloadRejectionReason.MultiSeason, "Multi-season releases are only supported through interactive complete-series search");
            }

            return DownloadSpecDecision.Accept();
        }
    }
}
