using MediatR;

namespace NetWealth.Data.Models.Command
{
    public class CommandBase<T> : IRequest<T> where T : class
    {

    }
}