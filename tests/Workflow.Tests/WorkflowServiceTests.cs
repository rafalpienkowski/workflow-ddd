using System;
using FluentAssertions;
using Workflow.Application.Services;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Domain.Framework;
using Workflow.Tests.Extensions;
using Xunit;

namespace Workflow.Tests
{
    public class WorkflowServiceTests
    {
        private readonly ConfigurationInMemoryRepository _repository = new ConfigurationInMemoryRepository();
        private const string someData = "Some data";
        private const string draftAuthor = "Tim";
        private DateTime goLive = DateTime.UtcNow.AddDays(12);
        private const string plannedAuthor = "Tom";
        private const string liveAuthor = "Tam";
        private const string archiveAuthor = "Tob";

        [Fact]
        public void SunnyDayFlowTest()
        {            
            var sut = new WorkflowService(_repository);
            
            // Draft
            var draft = sut.CreateDraft(someData, draftAuthor).Value;
            var configurationId = draft.Id;

            AssertDraft(draft.Id);

            // Planned
            sut.Schedule(draft.Id.AsGuid(), plannedAuthor, goLive);

            AssertPlanned(configurationId);

            // Live
            sut.GoLive(configurationId.AsGuid(), liveAuthor);

            AssertLive(configurationId);

            // Archive
            sut.Archive(configurationId.AsGuid(), archiveAuthor);

            AssertArchive(configurationId);

            // End flow assertion
            AssertRawData(configurationId);
        }

        private void AssertDraft(ConfigurationId draftId)
        {
            var draftFromRepository = _repository.GetDraft(draftId);
            draftFromRepository.HasValue.Should().BeTrue();
            draftFromRepository.Value.Data.AsString().Should().Be(someData);
            draftFromRepository.Value.Author.AsString().Should().Be(draftAuthor);
            draftFromRepository.Value.CreationDate.ShouldBe(DateTime.UtcNow);
        }

        private void AssertPlanned(ConfigurationId plannedId)
        {
            var planned = _repository.GetPlanned(plannedId);

            planned.HasValue.Should().BeTrue();
            planned.Value.Data.AsString().Should().Be(someData);
            planned.Value.Author.AsString().Should().Be(plannedAuthor);
            planned.Value.CreationDate.ShouldBe(DateTime.UtcNow);
            planned.Value.WhenGoLive.ShouldBe(goLive);
        }

        private void AssertLive(ConfigurationId liveId)
        {
            var live = _repository.GetLive(liveId);

            live.HasValue.Should().BeTrue();
            live.Value.Data.AsString().Should().Be(someData);
            live.Value.Author.AsString().Should().Be(liveAuthor);
            live.Value.CreationDate.ShouldBe(DateTime.UtcNow);
        }

        private void AssertArchive(ConfigurationId archiveId)
        {
            var archived = _repository.GetArchived(archiveId);
            archived.HasValue.Should().BeTrue();
            archived.Value.Data.AsString().Should().Be(someData);
            archived.Value.Author.AsString().Should().Be(archiveAuthor);
            archived.Value.CreationDate.ShouldBe(DateTime.UtcNow);
        }

        private void AssertRawData(ConfigurationId archiveId)
        {
            var goLiveDate = Date.FromDateTime(goLive);
            var dataRow = _repository.Get(archiveId.AsGuid());
            dataRow.Status.Should().Be(ConfigStatus.Archived);
            dataRow.DraftAuthor.Should().Be(draftAuthor);
            dataRow.PlannedAuthor.Should().Be(plannedAuthor);
            dataRow.LiveAuthor.Should().Be(liveAuthor);
            dataRow.WhenGoLive.Should().Be(goLiveDate.AsDateTime());
            dataRow.ArchivedAuthor.Should().Be(archiveAuthor);
        }
    }
}