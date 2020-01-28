using System;
using FluentAssertions;
using Workflow.Application.Services;
using Xunit;

namespace Workflow.Tests
{
    public class WorkflowServiceTests
    {
        [Fact]
        public void SunnyDayFlowTest()
        {
            var repository = new ConfigurationRepository();
            var sut = new WorkflowService(repository);
            const string someData = "Some data";

            var draft = sut.CreateDraft(someData, "Raf");
            
            // var draftFromRepository = repository.GetDraft(draft.Id);
            // draftFromRepository.Data.Should().Be(someData);
            // draftFromRepository.Author.Should().Be("Raf");
            // draftFromRepository.CreationDate.Should().BeCloseTo(DateTime.UtcNow);

            // var goLive = DateTime.UtcNow.AddDays(1);
            // var plannedId = sut.Schedule(draftId, "Tom", goLive);

            // var planned = repository.GetPlanned(plannedId);
            // planned.Data.Should().Be(someData);
            // planned.Author.Should().Be("Tom");
            // planned.CreationDate.Should().BeCloseTo(DateTime.UtcNow);
            // planned.WhenGoLive.Should().Be(goLive);
            
            // var goLiveId = sut.GoLive(plannedId, "Alice");

            // var live = repository.GetLive(goLiveId);
            // live.Data.Should().Be(someData);
            // live.Author.Should().Be("Alice");
            // live.CreationDate.Should().BeCloseTo(DateTime.UtcNow);

            // var archivedId = sut.Archive(goLiveId, "Bob");

            // var archived = repository.GetArchived(archivedId);
            // archived.Data.Should().Be(someData);
            // archived.Author.Should().Be("Bob");
            // archived.CreationDate.Should().BeCloseTo(DateTime.UtcNow);

            // var dataRow = repository.Get(archivedId);

            // dataRow.Status.Should().Be(ConfigStatus.Archived);
            // dataRow.DraftAuthor.Should().Be("Raf");
            // dataRow.PlannedAuthor.Should().Be("Tom");
            // dataRow.LiveAuthor.Should().Be("Alice");
            // dataRow.WhenGoLive.Should().Be(goLive);
            // dataRow.ArchivedAuthor.Should().Be("Bob");
        }
    }
}