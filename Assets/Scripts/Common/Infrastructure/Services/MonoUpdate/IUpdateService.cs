using System;

namespace Common.Infrastructure.Services.MonoUpdate
{
    public interface IUpdateService
    {
        event Action OnUpdate;
        event Action OnFixedUpdate;
        event Action OnLateUpdate;
    }
}