using System;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using NzbDrone.Core.DecisionEngine.Specifications;
using NzbDrone.Core.IndexerSearch.Definitions;
using NzbDrone.Core.Parser.Model;
using NzbDrone.Core.Test.Framework;
using NzbDrone.Core.Tv;

namespace NzbDrone.Core.Test.DecisionEngineTests
{
    [TestFixture]
    public class MultiSeasonSpecificationFixture : CoreTest<MultiSeasonSpecification>
    {
        private RemoteEpisode _remoteEpisode;

        [SetUp]
        public void Setup()
        {
            var series = Builder<Series>.CreateNew().With(s => s.Id = 1234).Build();
            _remoteEpisode = new RemoteEpisode
            {
                ParsedEpisodeInfo = new ParsedEpisodeInfo
                {
                    FullSeason = true,
                    SeasonNumbers = new[] { 1, 2, 3, 4, 5 }
                },
                Episodes = Builder<Episode>.CreateListOfSize(3)
                                           .All()
                                           .With(s => s.SeriesId = series.Id)
                                           .BuildList(),
                Series = series,
                Release = new ReleaseInfo
                {
                    Title = "Series.Title.S01-05.720p.BluRay.X264-RlsGrp"
                }
            };
        }

        [Test]
        public void should_return_true_if_is_not_a_multi_season_release()
        {
            _remoteEpisode.ParsedEpisodeInfo.SeasonNumbers = new[] { 1 };
            _remoteEpisode.Episodes.Last().AirDateUtc = DateTime.UtcNow.AddDays(+2);
            Subject.IsSatisfiedBy(_remoteEpisode, new()).Accepted.Should().BeTrue();
        }

        [Test]
        public void should_return_false_if_is_a_multi_season_release_from_automatic_search()
        {
            Subject.IsSatisfiedBy(_remoteEpisode, new()).Accepted.Should().BeFalse();
        }

        [Test]
        public void should_return_true_if_is_a_multi_season_release_from_interactive_search()
        {
            var information = new ReleaseDecisionInformation
            {
                SearchCriteria = new SeasonSearchCriteria { InteractiveSearch = true }
            };

            Subject.IsSatisfiedBy(_remoteEpisode, information).Accepted.Should().BeTrue();
        }

        [Test]
        public void should_return_true_if_is_complete_series_keyword_from_interactive_search()
        {
            _remoteEpisode.ParsedEpisodeInfo.SeasonNumbers = Array.Empty<int>();
            _remoteEpisode.ParsedEpisodeInfo.IsCompleteSeries = true;

            var information = new ReleaseDecisionInformation
            {
                SearchCriteria = new SeasonSearchCriteria { InteractiveSearch = true }
            };

            Subject.IsSatisfiedBy(_remoteEpisode, information).Accepted.Should().BeTrue();
        }
    }
}
