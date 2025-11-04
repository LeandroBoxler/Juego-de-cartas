using System.Collections.Generic;
namespace MiJuego.Domain.UseCases;
public class OperationResult<T>
{
    private string v;

    public T Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public List<string> Errors { get; private set; }

    public OperationResult(T value) { Value = value; IsSuccess = true; Errors = new List<string>(); }
    public OperationResult(List<string> errors) { IsSuccess = false; Errors = errors; }

    public OperationResult(string v)
    {
        this.v = v;
    }
}
