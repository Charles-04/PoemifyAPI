namespace Poemify.Models.DTOs.Response
{
    public record Response<T>
    {
        public string Message { get; init; }
        public T Result { get; init; }
        public bool Success { get; init; }
    }
}
