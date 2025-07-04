namespace Poliedro.Psr.Domain.Dto;

public record ErrorReportDto(
    string ErrorReport, 
    string ErrorReportAdmitted, 
    string Error,
    string BatteryPercentag,
    string Status,
    string DeviceNumber,
    DateTime FirstSeen
    );
