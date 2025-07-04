namespace Poliedro.Psr.Application.Dto;

public record DeviceDto(
Guid Guid,
string DeviceType,
string Provider,
QrDetailsDto Qr,
StateDevice State,
ReaderDto Reader,
PaginatedReaderHistoryDto ReaderHistories);

