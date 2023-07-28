namespace EndProject.Application.DTOs;

public record TokenResponseDTO(string token, DateTime expireDate,DateTime refreshTokenExpration,string refreshToken);