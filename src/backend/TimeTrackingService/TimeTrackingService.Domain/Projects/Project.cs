using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain.Projects.ValueObjects;

namespace TimeTrackingService.Domain.Projects;

public sealed class Project
{
    private Project(
        ProjectId id,
        ProjectCode code,
        ProjectName name,
        ProjectBudget budget,
        ProjectPeriod period)
    {
        Id = id;
        Code = code;
        Name = name;
        Budget = budget;
        Period = period;
    }

    public ProjectId Id { get; private set; }

    public ProjectCode Code { get; private set; }

    public ProjectName Name { get; private set; }

    public ProjectBudget Budget { get; private set; }

    public ProjectPeriod Period { get; private set; }

    public static Result<Project, Error> Create(
        ProjectId id,
        ProjectCode code,
        ProjectName name,
        ProjectBudget budget,
        ProjectPeriod period)
    {
        return Result.Success<Project, Error>(new Project(id, code, name, budget, period));
    }
}
