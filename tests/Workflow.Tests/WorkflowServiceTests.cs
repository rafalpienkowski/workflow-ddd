using System;
using FluentAssertions;
using Workflow.Application.Services;
using Workflow.Domain.Configuration;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Tests.Extensions;
using Xunit;

namespace Workflow.Tests
{
    public class WorkflowServiceTests
    {
        private readonly ConfigurationInMemoryRepository _repository = new ConfigurationInMemoryRepository();
        private const string someData = "Some data";
        private const string draftAuthor = "Raf";
        private DateTime goLive = DateTime.UtcNow.AddDays(12);
        private const string plannedAuthor = "Tom";
        private const string liveAuthor = "Alice";
        private const string archiveAuthor = "Bob";

        [Fact]
        public void SunnyDayFlowTest()
        {            
            var sut = new WorkflowService(_repository);
            
            // Draft
            var draft = sut.CreateDraft(someData, draftAuthor);

            AssertDraft(draft.Id);

            // Planned        
            sut.Schedule(draft.Id.AsGuid(), plannedAuthor, goLive);

            var plannedId = PlannedId.FromDraftId(draft.Id);
            AssertPlanned(plannedId);

            // Live
            sut.GoLive(plannedId.AsGuid(), liveAuthor);

            var liveId = LiveId.FromPlannedId(plannedId);
            AssertLive(liveId);

            // Archive
            sut.Archive(liveId.AsGuid(), archiveAuthor);

            var archiveId = ArchiveId.FromLiveId(liveId);
            AssertArchive(archiveId);

            // End flow assertion
            AssertRawData(archiveId);
        }

        private void AssertDraft(DraftId draftId)
        {
            var draftFromRepository = _repository.GetDraft(draftId);
            draftFromRepository.Data.AsString().Should().Be(someData);
            draftFromRepository.Author.AsString().Should().Be(draftAuthor);
            draftFromRepository.CreationDate.ShouldBe(DateTime.UtcNow);
        }

        private void AssertPlanned(PlannedId plannedId)
        {
            var planned = _repository.GetPlanned(plannedId);
            planned.Data.AsString().Should().Be(someData);
            planned.Author.AsString().Should().Be(plannedAuthor);
            planned.CreationDate.ShouldBe(DateTime.UtcNow);
            planned.WhenGoLive.ShouldBe(goLive);
        }

        private void AssertLive(LiveId liveId)
        {
            var live = _repository.GetLive(liveId);
            live.Data.AsString().Should().Be(someData);
            live.Author.AsString().Should().Be(liveAuthor);
            live.CreationDate.ShouldBe(DateTime.UtcNow);
        }

        private void AssertArchive(ArchiveId archiveId)
        {
            var archived = _repository.GetArchived(archiveId);
            archived.Data.AsString().Should().Be(someData);
            archived.Author.AsString().Should().Be(archiveAuthor);
            archived.CreationDate.ShouldBe(DateTime.UtcNow);
        }

        private void AssertRawData(ArchiveId archiveId)
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