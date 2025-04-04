namespace api.Dtos.AccountDtos;

public record class NewUserResponseDto
(
    string Username,
    string Email,

    string Token
);

