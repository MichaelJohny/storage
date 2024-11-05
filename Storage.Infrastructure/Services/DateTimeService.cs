using Storage.Application.Common.Interfaces;

namespace Storage.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;

}