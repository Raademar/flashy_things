using System.Collections.Generic;
using flashy_things.Models;

namespace flashy_things.Repositories
{
    public interface ICartRepository

    {
    Cart Get(int id);

    bool SubmitOrder(Order order);

    }
}