export interface ErrorResponseDto {
    StatusCodes: number;
    Message: string;
    Details: string;
}

export interface ErrorState {
    error: ErrorResponseDto | null;
}