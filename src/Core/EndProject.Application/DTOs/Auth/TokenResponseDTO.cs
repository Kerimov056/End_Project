namespace EndProject.Application.DTOs.Auth;

public record TokenResponseDTO(string token,
                               DateTime expireDate,
                               DateTime refreshTokenExpration,
                               string refreshToken,
                               string UserName,
                               string AppUserId);