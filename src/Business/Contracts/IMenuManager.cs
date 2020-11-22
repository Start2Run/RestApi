using System;
using Common.Enums;

namespace Business.Contracts
{
    public interface IMenuManager : IDisposable
    {
        bool SelectOption(MenuOption menuOption);
    }
}